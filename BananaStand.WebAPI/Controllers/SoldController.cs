using BananaStand.Business.Models;
using BananaStand.Business.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace BananaStand.Controllers
{
    [RoutePrefix("api/sold")]
    public class SoldController : ApiController
    {
        BananaService _bananaService = new BananaService();

        [HttpPost, Route("bananas")]
        public async Task<IHttpActionResult> PurchaseBananas(BananaModel bananas)
        {
            await _bananaService.SoldBananas(bananas);

            return Ok();
        }
    }
}
