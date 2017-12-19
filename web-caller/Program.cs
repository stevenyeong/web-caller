using Inv.Core.Database;
using Inv.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inv.Core.Enum;
using Newtonsoft.Json;
using Inv.Core;

namespace web_caller
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please select an action:");
            Console.WriteLine("1 : CompaniesList");
            Console.WriteLine("2 : CompanyInfo");

            //GetFinancialHistory();

            //string userInput =  Console.ReadLine();
            //if (!string.IsNullOrWhiteSpace(userInput))
            //{
            //    int iUserInput = int.Parse(userInput);
            //    switch (iUserInput)
            //    {
            //        case 1:
            //            RequsetCompaniesList();
            //            break;
            //    }
            //}

            Console.WriteLine("Completed!");

            Console.ReadLine();
        }

        private static void RequsetCompaniesList()
        {
            string url = "https://sgx-premium.wealthmsi.com/sgx/search";
            string jsonBody = "{\"criteria\":[]}";

            string webResponse = WebRequestHelper.RequestUrlPost(url, jsonBody);

            if (string.IsNullOrEmpty(webResponse))
            {
                Console.WriteLine("ERROR: RequsetCompaniesList !!!");
                return;
            }
            RequestLogDataHandler.AddRequestLog(url, jsonBody, webResponse, RequestType.CompaniesList);
        }

        private static void CompaniesListRequestLog()
        {
            var requestLogs = RequestLogDataHandler.GetRequestLog(RequestType.CompaniesList);

            if(requestLogs != null)
            {
                foreach(var rLog in requestLogs)
                {
                    Console.WriteLine(rLog.ID + ":" + rLog.ResponseData);
                }
            }
        }

        private static void ConvertCompaniesListRequestLog()
        {
            var requestLogs = RequestLogDataHandler.GetRequestLog(RequestType.CompaniesList);

            if (requestLogs != null)
            {
                var requestLog = requestLogs[0];
                var jsonData = JsonConvert.DeserializeObject<CompaniesJson>(requestLog.ResponseData);
                
                Console.WriteLine(jsonData.companies);

                if(jsonData.companies != null)
                {
                    foreach(var cmp in jsonData.companies)
                    {
                        StockCompanyDataHandler.AddStockCompany(cmp);
                    }
                }
            }

            Console.WriteLine("ConvertCompaniesListRequestLog completed.");
        }


        private static void GetFinancialHistory()
        {
            var stockCompanies = StockCompanyDataHandler.GetAllStockCompany();

            stockCompanies = stockCompanies.Where(x => x.LatsUpdatedOn == null).ToList();

            if (stockCompanies == null || stockCompanies.Count == 0)
            {
                Console.WriteLine("GetFinancialHistory: no stock company.");
                return;
            }
            var url = "https://sgx-premium.wealthmsi.com/sgx/company/financials";
            var reqBody = "{{\"id\":\"{0}\"}}";

            foreach (var cmp in stockCompanies)
            {
                var tmpReqBody = string.Format(reqBody, cmp.TickerCode);
                var response = WebRequestHelper.RequestUrlPost(url, tmpReqBody);

                if (!string.IsNullOrEmpty(response) && !response.ToLower().Contains("errorcode"))
                {
                    RequestLogDataHandler.AddRequestLog(url, tmpReqBody, response, RequestType.FinancialHistory);
                    cmp.LatsUpdatedOn = DateTime.UtcNow;
                    StockCompanyDataHandler.UpdateStockCompany(cmp);
                }
            }
            Console.WriteLine("GetFinancialHistory: done");
        }


        private class CompaniesJson
        {
            public int size { get; set; }
            public StockCompany[] companies { get; set; }
        }
    }
}
