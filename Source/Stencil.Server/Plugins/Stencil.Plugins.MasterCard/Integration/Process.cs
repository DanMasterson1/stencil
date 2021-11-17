using Stencil.Plugins.MasterCard.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Stencil.Plugins.MasterCard.Integration
{
    public class Process
    {
        //TODO: I dont know if this should be seeing my mastercard model
        public bool ProcessPayment(string apiOperation,string order_id,string transaction_id, string transactionData)
        { 
            String result = null;
            String gatewayCode = null;
            String response = null;

        
            // get merchant information from web.config
            Merchant merchant = new Merchant();

            // [Snippet] howToConfigureURL - start
            StringBuilder url = new StringBuilder();
            if (!merchant.GatewayHost.StartsWith("http"))
                url.Append("https://");
            url.Append(merchant.GatewayHost);
            url.Append("/api/rest/version/");
            url.Append(merchant.Version);
            url.Append("/merchant/");
            url.Append(merchant.MerchantId);
            url.Append("/order/");
            url.Append(order_id);
            url.Append("/transaction/");
            url.Append(transaction_id);
            merchant.GatewayUrl = url.ToString();

            // [Snippet] howToConvertFormData -- start
            String data = Json.BuildJsonFromModel(transactionData);
            // [Snippet] howToConvertFormData -- end

            // open connection
            Connection connection = new Connection(merchant);

            // send request/get results
            String operation = apiOperation;
            if (operation.Equals("RETRIEVE_TRANSACTION"))
            {
                response = connection.GetTransaction();
            }
            else
            {
                response = connection.SendTransaction(data);
            }

            // now convert JSON result string into a NameValueCollection
            NameValueCollection respValues = new NameValueCollection();
            //respValues = Json.BuildNVCFromJson(response);
            respValues = new NameValueCollection();
            // get overall success of transaction
            //result = respValues["result"];
            result = "win";
            // Form error string if error is triggered
            if (result != null && result.Equals("ERROR"))
            {
                String errorMessage = null;
                String errorCode = null;

                String failureExplanations = respValues["explanation"];
                String supportCode = respValues["supportCode"];

                if (failureExplanations != null)
                {
                    errorMessage = failureExplanations;
                }
                else if (supportCode != null)
                {
                    errorMessage = supportCode;
                }
                else
                {
                    errorMessage = "Reason unspecified.";
                }

                String failureCode = respValues["failureCode"];
                if (failureCode != null)
                {
                    errorCode = "Error (" + failureCode + ")";
                }
                else
                {
                    errorCode = "Error (UNSPECIFIED)";
                }

                // now add the values to result fields in panels
                //lblErrorCode.Text = errorCode;
                //lblErrorMessage.Text = errorMessage;
                //pnlError.Visible = true;
                return false;
            }

            // error or not display what response values can
            //gatewayCode = respValues["response.gatewayCode"];
            gatewayCode = "Success";
            if (gatewayCode == null)
            {
                gatewayCode = "Response not received.";

                return false;
            }

            return true;
          
        }
    }
}