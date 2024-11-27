
using GestionInventario.src.Modules.Notifications.Alerts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Notifications.Alerts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockAlertController (IStockAlertService stockAlertService) : ControllerBase
    {
        private readonly IStockAlertService _stockAlertService = stockAlertService;


    [HttpPost("check")]
    public async Task<IActionResult> TriggerStockCheck()
    {
        await _stockAlertService.CheckAndNotifyLowStockAsync();
        return Ok("Stock check triggered.");
    }

    [HttpGet("alerts")]
    public async Task<IActionResult> GetLowStock()
    {
        var alerts = await _stockAlertService.GetLowStockProductsAsync();
        if (!alerts.Any())
            return NoContent();

        return Ok(alerts);
    }
    }
}