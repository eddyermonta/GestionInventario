
using GestionInventario.src.Modules.Notifications.Alerts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Notifications.Alerts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertManagerController(IStockAlertService stockAlertService) : ControllerBase
    {
        private readonly IStockAlertService _stockAlertService = stockAlertService;

        [HttpPost("resolver/{alertId}")]
        public async Task<IActionResult> ResolveAlert(int alertId)
        {
            var success = await _stockAlertService.ResolveAlertAsync(alertId);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpGet("GetAlertsByStatus")]
        public async Task<IActionResult> GetAlertsByStatus()
        {
            var alerts = await _stockAlertService.GetAlertsByStatusAsync();
            if (!alerts.Any())
                return NoContent();

            return Ok(alerts);
        }
    }
}