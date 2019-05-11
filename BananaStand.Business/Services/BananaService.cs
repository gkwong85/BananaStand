using BananaStand.Business.Models;
using BananaStand.Data;
using BananaStand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BananaStand.Business.Services
{
    public class BananaService
    {
        public BananaService() { }

        public async Task PurchaseBananas(BananaModel bananas)
        {
            try
            {
                using (var context = new BananaStandContext())
                {
                    var mappedBananas = BananaPurchaseMapper(bananas);

                    context.Bananas_Purchased.Add(mappedBananas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // send email to dev team and/or log events
                throw new Exception($"Error connecting to Database. Error: {ex.Message}");
            }
        }

        public async Task SoldBananas(BananaModel bananas)
        {
            try
            {
                using (var context = new BananaStandContext())
                {
                    var soldList = new List<Bananas_Sold>();
                    var sellableBananas = context.Bananas_Purchased.Where(x => x.ExpirationDate > bananas.TransactionDate && x.Sold < x.Bananas).OrderBy(x => x.ExpirationDate).ToList();
                    var soldBananas = bananas.Bananas;

                    // updating sold count for purchased bananas table
                    for (var i = 0; i < sellableBananas.Count; i++)
                    {
                        
                        // sold all bananas that was purchased with left over
                        if (sellableBananas[i].Sold + soldBananas > sellableBananas[i].Bananas)
                        {
                            soldBananas -= sellableBananas[i].Bananas;
                            sellableBananas[i].Sold = sellableBananas[i].Bananas;
                            soldList.Add(BananaSoldMapper(sellableBananas[i].Sold, sellableBananas[i].Id, bananas.TransactionDate));
                        }
                        // no more bananas to roll over
                        else
                        {
                            sellableBananas[i].Sold += soldBananas;
                            soldList.Add(BananaSoldMapper(sellableBananas[i].Sold, sellableBananas[i].Id, bananas.TransactionDate));
                            break;
                        }
                    }

                    // insert into sold bananas table
                    context.Bananas_Sold.AddRange(soldList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // send email to dev team and/or log events
                throw new Exception($"Error connecting to Database. Error: {ex.Message}");
            }
        }

        private Bananas_Purchased BananaPurchaseMapper(BananaModel bananas)
        {
            return new Bananas_Purchased
            {
                Bananas = bananas.Bananas,
                PurchaseDate = bananas.TransactionDate,
                ExpirationDate = bananas.TransactionDate.AddDays(10),
                Sold = 0
            };
        }

        private Bananas_Sold BananaSoldMapper(int soldBananas, int bananasPurchasedId, DateTime transactionDate)
        {
            return new Bananas_Sold
            {
                Bananas = soldBananas,
                BananasPurchasedId = bananasPurchasedId,
                SellDate = transactionDate
            };
        }
    }
}
