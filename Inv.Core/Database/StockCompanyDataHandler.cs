using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Core.Database
{
    public class StockCompanyDataHandler
    {
        public static void AddStockCompany(StockCompany newStockComp)
        {
            using(var db = new InvestmentEntities())
            {
                var isExist = db.StockCompanies.Any(x => x.TickerCode == newStockComp.TickerCode);

                if (!isExist)
                {
                    var newStockCmp = db.StockCompanies.Create();
                    newStockCmp.TickerCode = newStockComp.TickerCode;
                    newStockCmp.CompanyName = newStockComp.CompanyName;
                    newStockCmp.Industry = newStockComp.Industry;
                    newStockCmp.IndustryGroup = newStockComp.IndustryGroup;
                    newStockCmp.CreatedOn = DateTime.UtcNow;

                    db.StockCompanies.Add(newStockCmp);
                    db.SaveChanges();
                }
            }

        }


        public static void UpdateStockCompany(StockCompany updStockComp)
        {
            using (var db = new InvestmentEntities())
            {
                var stockCmp = db.StockCompanies.FirstOrDefault(x => x.TickerCode == updStockComp.TickerCode);

                if(stockCmp == null)
                {
                    return;
                }

                if(updStockComp.LatsUpdatedOn != null)
                {
                    stockCmp.LatsUpdatedOn = updStockComp.LatsUpdatedOn;
                }

                db.Entry<StockCompany>(stockCmp).State = EntityState.Modified;
                db.SaveChanges();


            }
        }

        public static IList<StockCompany> GetAllStockCompany()
        {
            using(var db = new InvestmentEntities())
            {
                return db.StockCompanies.ToList();
            }
        }

    }
}
