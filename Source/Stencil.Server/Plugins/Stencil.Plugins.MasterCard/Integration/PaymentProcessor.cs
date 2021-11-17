using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Core.Caching;
using Codeable.Foundation.Core.Unity;
using Stencil.Common;
using Stencil.Common.Configuration;
using Stencil.Common.Synchronization;
using Stencil.Primary;
using Stencil.Primary.Daemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using sdk = Stencil.SDK.Models;
using System.IO;
using System.Diagnostics;
using Stencil.Common.Integration;
using Stencil.Primary.Integration;
using Stencil.Domain;
using Stencil.Plugins.MasterCard.Models;
using Newtonsoft.Json;

namespace Stencil.Plugins.MasterCard.Integration
{
    public class PaymentProcessor : ChokeableClass, IProcessPayments
    {
        public PaymentProcessor(IFoundation iFoundation)
         : base(iFoundation)
        {
            this.API = iFoundation.Resolve<StencilAPI>();

        }

        public StencilAPI API { get; set; }

        public void ProcessPayments()
        {
            Process process = new Process();

            List<Payment> payments = this.API.Direct.Payments.GetPaymentsForProcessing();

            foreach (Payment payment in payments)
            {

                Order order = this.API.Direct.Orders.GetById(payment.order_id);

                MasterCardTransaction masterCardTransaction = new MasterCardTransaction();

                sdk.Order shapedOrder = this.API.Index.Orders.GetById(order.order_id);

                masterCardTransaction.order_amount = (double)shapedOrder.order_total; // can I get this elsewhere
                masterCardTransaction.order_currency = "USD";
                masterCardTransaction.order_id = order.order_id;

                masterCardTransaction.source = new SourceOfFunds()
                {
                    type = "CARD"
                };

                if (order.payment_id != null)
                {
                    Payment referencePayment = this.API.Direct.Payments.GetById((Guid)order.payment_id);
                    if (!referencePayment.payment_processed_successful) // is it still eligable
                    {
                        masterCardTransaction.source.provided = new Card()
                        {
                            expiryMonth = referencePayment.expire_date.Month,
                            expiryYear = referencePayment.expire_date.Year,
                            number = referencePayment.card_number,
                            securityCode = referencePayment.cvv
                        };

                        string transactionJson = JsonConvert.SerializeObject(masterCardTransaction);

                        bool paymentSuccessful = process.ProcessPayment("PAY", order.order_id.ToString(), new Guid().ToString(),transactionJson);

                        // update if the payment had been processed successfully
                        referencePayment.payment_processed_successful = paymentSuccessful;
                        this.API.Direct.Payments.Update(referencePayment);


                        PaymentTransaction transaction = new PaymentTransaction()
                        {
                            payment_id = referencePayment.payment_id,
                            order_id = order.order_id,
                            transaction_outcome = (paymentSuccessful) ? TransactionOutcome.Success : TransactionOutcome.Failure
                        };

                        // insert the new transaction that just occured
                        this.API.Direct.PaymentTransactions.Insert(transaction);

                        // if the payment was successful mark the order as paid
                        if (paymentSuccessful)
                        {
                            order.order_paid = true;
                            this.API.Direct.Orders.Update(order);
                        }
                    }
                }

            }
        }
    }
}