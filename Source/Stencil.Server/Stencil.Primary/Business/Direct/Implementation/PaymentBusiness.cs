using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stencil.Domain;
using Stencil.Data.Sql;
using Stencil.Primary.Synchronization;
using Stencil.Primary.Workers;
using Stencil.Primary.Workers.Models;

namespace Stencil.Primary.Business.Direct.Implementation
{
    partial class PaymentBusiness
    {
        partial void AfterInsertPersisted(StencilContext db, dbPayment payment)
        {
            Payment domainPayment = payment.ToDomainModel();

            if (domainPayment.save_paymentdetails)
            {
                Order domainOrder = this.API.Direct.Orders.GetById(domainPayment.order_id);
                // persist these payment details for later use
                PaymentDetailPersistanceWorker.EnqueueRequest(IFoundation, new PaymentDetailRequest()
                {
                    card_number = domainPayment.card_number,
                    expire_date = domainPayment.expire_date,
                    card_type = domainPayment.card_type,
                    cvv = domainPayment.cvv,
                    account_id = domainOrder.account_id
                });
            }
        }

        public List<Payment> GetPaymentsForProcessing()
        {
            return base.ExecuteFunction(nameof(GetPaymentsForProcessing), delegate ()
            {
                using(StencilContext db = base.CreateSQLContext())
                {
                    IQueryable<dbPayment> dbPayments = (from n in db.dbPayments
                                                  where n.payment_processed_successful == false && n.card_type == 0
                                                  select n);

                    return dbPayments.ToDomainModel();
                }
            });
        }
    }
}
