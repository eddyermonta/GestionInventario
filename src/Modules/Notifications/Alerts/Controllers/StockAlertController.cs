
using GestionInventario.src.Modules.Notifications.Alerts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.src.Modules.Notifications.Alerts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockAlertController (IStockAlertService stockAlertService) : ControllerBase
    {
        private readonly IStockAlertService _stockAlertService = stockAlertService;


    /// <summary>
    /// Get the status of the stock check background service
    /// </summary>
    /// <returns>
    /// Status of the stock check background service
    /// </returns>
    [HttpGet("stock-check/status")]
    [ProducesResponseType(typeof(OkObjectResult), 200)]
    public IActionResult GetStockCheckStatus()
    {
        // Devuelve el estado del servicio en segundo plano, por ejemplo, la última vez que se ejecutó
        return Ok(new { Status = "Running", LastRun = DateTime.UtcNow });
    }


    [HttpGet("alerts")]
    public async Task<IActionResult> GetLowStock()
    {
        var alerts = await _stockAlertService.GetLowStockProductsAsync();
        if (alerts == null || !alerts.Any())
            return NoContent();

        return Ok(alerts);
    }
    }
}