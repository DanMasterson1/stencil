using Codeable.Foundation.Common;
using Codeable.Foundation.Common.Aspect;
using Codeable.Foundation.Common.Daemons;
using Codeable.Foundation.Core.Caching;
using Codeable.Foundation.Core.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Stencil.Common.Configuration;
using Stencil.Common;
using sdk = Stencil.SDK.Models;
using dm = Stencil.Domain;
using System.Data.Sql;
using Stencil.Primary;
using Stencil.Primary.Business.Direct.Implementation;
using Stencil.Plugins.InvoiceGenerator.Integration;
using Stencil.SDK;
using Stencil.SDK.Models;

namespace Stencil.Plugins.InvoiceGenerator.Daemons
{
    public class InvoicerDaemon : ChokeableClass, IDaemonTask
    {
        public InvoicerDaemon(IFoundation iFoundation, string agentName)
            : base(iFoundation)
        {
            this.AgentName = agentName;
            this.API = iFoundation.Resolve<StencilAPI>();
            this.Cache = new AspectCache("InvoicerDaemon", iFoundation, new ExpireStaticLifetimeManager("InvoicerDaemon.Life15", System.TimeSpan.FromMinutes(15), false));
        }

        public StencilAPI API { get; set; }
        public AspectCache Cache { get; set; }

        public const string DAEMON_NAME_FORMAT = "InvoicerDaemon{0}";

        #region IDaemonTask Members


        public const string DAEMON_NAME = "InvoicerDaemon";

        protected static bool _executing;

        public string AgentName { get; set; }

        public string DaemonName
        {
            get
            {
                return DAEMON_NAME;
            }
            protected set
            {
            }
        }

        public void Execute(Codeable.Foundation.Common.IFoundation iFoundation)
        {
            base.ExecuteMethod("Execute", delegate()
            {
                if (_executing) { return; } // safety

                try
                {
                    _executing = true;
                    this.InvoiceOrders();
                }
                finally
                {
                    _executing = false;
                }
            });
        }

        public DaemonSynchronizationPolicy SynchronizationPolicy
        {
            get { return DaemonSynchronizationPolicy.SingleAppDomain; }
        }

        #endregion

        protected void InvoiceOrders()
        {
            base.ExecuteMethod(nameof(InvoiceOrders), delegate()
            {

                PdfSharpCreator pdfCreator = new PdfSharpCreator(this.IFoundation);

                List<Guid> nonInvoicedOrders = this.API.Direct.Orders.GetNonInvoicedOrders(); 

                foreach (Guid order_id in nonInvoicedOrders)
                {
                    dm.Order domainOrder = this.API.Direct.Orders.GetById(order_id);
 
                    dm.Asset asset = new dm.Asset()
                    {
                        type = Domain.AssetType.Image,
                        available = true,
                        resize_required = false,
                        raw_url = "",
                        public_url = ""

                    };

                    asset = this.API.Direct.Assets.Insert(asset);

                    dm.Invoice invoice = new dm.Invoice()
                    {
                        asset_id = asset.asset_id,
                        order_id = domainOrder.order_id
                    };

                    invoice = this.API.Direct.Invoices.Insert(invoice);

                    if (invoice != null)
                    {

                        domainOrder.invoice_id = invoice.invoice_id;

                        this.API.Direct.Orders.Update(domainOrder);
                    }

                    sdk.Order shapedOrder = this.API.Index.Orders.GetById(order_id);
                    List<sdk.LineItem> lineItems = this.API.Index.LineItems.GetByOrderId(order_id, 0, 10).items;

                    byte[] file = pdfCreator.GenerateInvoicePDF(shapedOrder, lineItems);

                    File.WriteAllBytes(@"C:\Users\DanielMasterson\Desktop\demo.pdf", file);

                    // agitate close order
                }


            });
        }

       
       
    }
}
