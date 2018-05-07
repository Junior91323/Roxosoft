using Microsoft.AspNetCore.Mvc;
using Roxosoft.BLL.Services.Abstract;
using System.Threading.Tasks;

namespace Roxosoft_TEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            return View();
        }
    }
}