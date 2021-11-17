using Codeable.Foundation.Common;
using Stencil.Domain;
using Stencil.Primary.Daemons;
using Stencil.Primary.Workers.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stencil.Primary.Workers
{
    class PaymentDetailPersistanceWorker : WorkerBase<PaymentDetailRequest>
    {
        public const string WORKER_NAME = nameof(PaymentDetailPersistanceWorker);

        public PaymentDetailPersistanceWorker(IFoundation iFoundation)
            : base(iFoundation, WORKER_NAME)
        {
            this.API = iFoundation.Resolve<StencilAPI>();
        }

        public StencilAPI API { get; set; }

        public static void EnqueueRequest(IFoundation foundation, PaymentDetailRequest request)
        {
            EnqueueRequest<PaymentDetailPersistanceWorker>(foundation, WORKER_NAME, request, (int)TimeSpan.FromMinutes(2).TotalMilliseconds); // updates every 2 mins
        }

        // Dont think I need to override the default implementation of Enqueue

        // my implementation of processing request (what the worker will actually do)

        protected override void ProcessRequest(PaymentDetailRequest request)
        {
            base.ExecuteMethod(nameof(ProcessRequest), delegate ()
            {
                // save the payment details with the account ID
                if(request.account_id != null)
                {

                    PaymentDetail paymentDetail = new PaymentDetail()
                    {
                        account_id = request.account_id,
                        card_number = request.card_number,
                        card_type = request.card_type,
                        expire_date = request.expire_date,
                        cvv = request.cvv
                    };
                    this.API.Direct.PaymentDetails.Insert(paymentDetail);

                }


            });
        }
    }
}
