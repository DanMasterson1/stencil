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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using Stencil.Common.Integration;
using Stencil.Primary.Integration;

namespace Stencil.Plugins.InvoiceGenerator.Integration
{
    public class PdfSharpCreator : ChokeableClass, IGeneratePDFs
    {
        public PdfSharpCreator(IFoundation iFoundation)
           : base(iFoundation)
        {
            this.API = iFoundation.Resolve<StencilAPI>();
           
        }

        public StencilAPI API { get; set; }

        public byte[] GenerateInvoicePDF(sdk.Order order, List<sdk.LineItem> lineItems)
        {

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont subHeaderfont = new XFont("Verdana", 10);
            XFont lineitemHeaderfont = new XFont("Verdana", 12);
            XFont lineitemFont = new XFont("Verdana", 8);

            // HEADER
            gfx.DrawString("Invoice", headerFont, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.TopCenter);
            gfx.DrawString($"Invoice Id: {order.invoice_id}", subHeaderfont, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormat.TopCenter);
            gfx.DrawString($"Order Id: {order.order_id}", subHeaderfont, XBrushes.Black, new XRect(0, 30, page.Width, page.Height), XStringFormat.TopCenter);
            gfx.DrawLine(new XPen(XColors.Black, 2), 0, 50, page.Width, 50);

            // ORDER INFO
            gfx.DrawString($"Ordered By: {order.account_name}", subHeaderfont, XBrushes.Black, new XRect(0, 70, page.Width, 0));
            gfx.DrawString($"Order Email: {order.account_email}", subHeaderfont, XBrushes.Black, new XRect(0, 80, page.Width, 0));

            gfx.DrawString($"Shipment Address: {order.shipment_address}", lineitemFont, XBrushes.Black, new XRect(page.Width / 2, 70, page.Width, 0));
            gfx.DrawString($"Payment Type: {order.payment_cardtype}", subHeaderfont, XBrushes.Black, new XRect(page.Width / 2, 80, page.Width, 0));

            gfx.DrawLine(new XPen(XColors.Black, 2), 0, 90, page.Width, 90);

            // LINE ITEM HEADER
            gfx.DrawString("Brand", lineitemHeaderfont, XBrushes.Black, new XRect(0, 105, page.Width, 0));
            gfx.DrawString("Product", lineitemHeaderfont, XBrushes.Black, new XRect(100, 105, page.Width, 0));
            gfx.DrawString("Promo", lineitemHeaderfont, XBrushes.Black, new XRect(350, 105, page.Width, 0));
            gfx.DrawString("Base", lineitemHeaderfont, XBrushes.Black, new XRect(400, 105, page.Width, 0));
            gfx.DrawString("Qty", lineitemHeaderfont, XBrushes.Black, new XRect(450, 105, page.Width, 0));
            gfx.DrawString("Lineprice", lineitemHeaderfont, XBrushes.Black, new XRect(500, 105, page.Width, 0));

            int lastLineItemY = 0;
            
            // LINE ITEMS
            for(int i = 0; i < lineItems.Count; i++)
            {
                sdk.Listing listing = this.API.Index.Listings.GetById(lineItems[i].listing_id);

                if(listing != null)
                {
                    
                    int y = 120 + (i * 20);
                    
                    gfx.DrawString(listing.brand_name, lineitemFont, XBrushes.Black, new XRect(0, y, page.Width, 0));
                    gfx.DrawString(listing.product_name, lineitemFont, XBrushes.Black, new XRect(100, y, page.Width, 0));
                    
                    if (listing.promotion_id != null)
                    {
                        
                        if(listing.promotion_description.Length > 12)
                        {
                            gfx.DrawString(listing.promotion_description.Substring(0, 12), lineitemFont, XBrushes.Black, new XRect(350, y, page.Width, 0));
                            gfx.DrawString(listing.promotion_description.Substring(12, listing.promotion_description.Length - 12), lineitemFont, XBrushes.Black, new XRect(350, y + 10, page.Width, 0));
                        }
                        else
                        {
                            gfx.DrawString(listing.promotion_description, lineitemFont, XBrushes.Black, new XRect(350, y, page.Width, 0));
                        }
                        
                    }

                    gfx.DrawString(listing.product_baseprice.ToString(), lineitemFont, XBrushes.Black, new XRect(400, y, page.Width, 0));
                    gfx.DrawString(lineItems[i].lineitem_quantity.ToString(), lineitemFont, XBrushes.Black, new XRect(450, y, page.Width, 0));
                    gfx.DrawString(lineItems[i].lineitem_total.ToString(), lineitemFont, XBrushes.Black, new XRect(500, y, page.Width, 0));

                    lastLineItemY = y;
                }

            }

            // ORDER TOTAL
            gfx.DrawLine(new XPen(XColors.Black, 2), 0, lastLineItemY + 20, page.Width, lastLineItemY + 20);
            
            gfx.DrawString($"Order Total: {order.order_total}", lineitemHeaderfont, XBrushes.Black, new XRect(page.Width - (page.Width/4), lastLineItemY + 40, page.Width, 0));

            byte[] fileContents = null;
            using(MemoryStream ms = new MemoryStream())
            {
                document.Save(ms, false);
                fileContents = ms.ToArray();
            }

            return fileContents;
          
        }

       // public byte[] GeneratePackSlipPDF(sdk.Order order, List<sdk.LineItem> lineItems)


    }
}