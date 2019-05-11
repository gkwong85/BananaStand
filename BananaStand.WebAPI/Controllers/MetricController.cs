using BananaStand.Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BananaStand.Controllers
{
    [RoutePrefix("api/metrics")]
    public class MetricController : ApiController
    {
        MetricService _metricService = new MetricService();

        [HttpGet, Route("bananas/{startDate}/{endDate}")]
        public async Task<IHttpActionResult> GetBananaInfo([FromUri] DateTime startDate, DateTime endDate)
        {
            if (startDate == null || endDate == null)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Provide start and end dates"));

            return Ok(await _metricService.GetBananaInfo(startDate, endDate));
        }
    }
}
