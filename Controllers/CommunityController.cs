using Microsoft.AspNetCore.Mvc;

namespace AspnetidentityRoleBased.Controllers
{
    public class CommunityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
