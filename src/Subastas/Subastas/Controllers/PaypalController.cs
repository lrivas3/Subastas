using Microsoft.AspNetCore.Mvc;

namespace Subastas.Controllers
{
    public class PayPalController(IConfiguration configuration) : Controller
    {
        [HttpGet]
        public IActionResult GetClientId()
        {
            return Json(new { clientId = configuration["PayPal:ClientId"] });
        }
    }
}
