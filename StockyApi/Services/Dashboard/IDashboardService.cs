using StockyApi.Models;

namespace StockyApi.Services.Dashboard
{
    public interface IDashboardInterface
    {
        Task<AppResponse<DashboardDto>> GetDashboard();
    }
}