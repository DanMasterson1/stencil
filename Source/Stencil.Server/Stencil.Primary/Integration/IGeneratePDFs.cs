using Stencil.SDK.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stencil.Primary.Integration
{
    public interface IGeneratePDFs // this shouldnt be in common think about putting in primary
    {
        byte[] GenerateInvoicePDF(Order order, List<LineItem> lineItems);

        //byte[] GeneratePackSlipPDF(Order order, List<LineItem> lineItems);

    }
}
