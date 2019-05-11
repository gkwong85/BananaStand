using BananaStand.Business.Models;
using BananaStand.Data;
using BananaStand.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BananaStand.Business.Services
{
    public class MetricService
    {
        public MetricService() { }

        public async Task<MetricViewModel> GetBananaInfo(DateTime startDate, DateTime endDate)
        {
            try
            {
                using(var context = new BananaStandContext())
                {
                    var bananaPurchased = await context.Bananas_Purchased.Where(x => x.PurchaseDate >= startDate && x.PurchaseDate <= endDate).ToListAsync();
                    return BananaMapper(bananaPurchased, endDate);
                }
            }
            catch(Exception ex)
            {
                // email dev and/or log error
                throw new Exception($"Error {ex.InnerException}");
            }
        }

        // maps all metric information by looping through and calculating based on end date
        private MetricViewModel BananaMapper(List<Bananas_Purchased> bananas_Purchaseds, DateTime endDate)
        {
            var metricModel = new MetricViewModel();
            var sellPrice = Convert.ToDouble(ConfigurationManager.AppSettings["sell"]);
            var purchasePrice = Convert.ToDouble(ConfigurationManager.AppSettings["purchase"]);

            foreach (var banana in bananas_Purchaseds)
            {
                metricModel.Bananas += banana.Bananas;

                if (banana.Bananas != banana.Sold && banana.ExpirationDate <= endDate)
                    metricModel.BananasExpired += banana.Bananas - banana.Sold;

                metricModel.BananasSold += banana.Sold;
                metricModel.ProfitLoss = (metricModel.BananasSold * sellPrice - metricModel.Bananas * purchasePrice).ToString("C");
            }

            return metricModel;
        }
    }
}
