using Microsoft.AspNetCore.Mvc;
using StockyApi.Models;
using StockyApi.Services.Dashboard;

namespace StockyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardInterface _dashboardService;

        public DashboardController(IDashboardInterface dashboardService)

        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<AppResponse<DashboardDto>>> GetDashboard()
        {
            var result = await _dashboardService.GetDashboard();
            return Ok(result);
        }
    }
}