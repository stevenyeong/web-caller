using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inv.Core.Enum;

namespace Inv.Core.Database
{
    public class RequestLogDataHandler
    {
        public static void AddRequestLog(string reqUrl, string reqBody, string respData, RequestType requestType)
        {
            using (var db = new InvestmentEntities())
            {
                var rLog = db.RequestLogs.Create();
                rLog.RequestType = (int)requestType;
                rLog.ResponseData = respData;
                rLog.RequestBody = reqBody;
                rLog.Url = reqUrl;
                rLog.CreatedOn = DateTime.UtcNow;

                db.RequestLogs.Add(rLog);

                db.SaveChanges();
            }
        }

        public static IList<RequestLog> GetRequestLog(RequestType requestType)
        {
            using (var db = new InvestmentEntities())
            {
                return db.RequestLogs.Where(x => x.ProcessedOn == null && x.RequestType == (int)requestType).ToList();
            }
        }
    }
}
