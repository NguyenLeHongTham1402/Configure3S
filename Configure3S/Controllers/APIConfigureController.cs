using Configure3S.Models;
using Configure3S.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configure3S.Controllers
{
    public class APIConfigureController : Controller
    {
        private readonly int page;
        private readonly int size = 1;
        private readonly ILogger<APIConfigureController> _logger;
        private APIConfigRepository apiConfig = new APIConfigRepository();

        public APIConfigureController(ILogger<APIConfigureController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListAPIConfig()
        {
            string companyId = User.Claims.LastOrDefault().Value.ToString();
            List<TbApiconfigure> result = new List<TbApiconfigure> { };
            if (companyId.Equals("3SCOMP"))
            {
                result = apiConfig.GetAllApiconfigures();
            }
            else
            {
                result = apiConfig.GetListAPIConfigByCompanyId(companyId);
            }

            return View(result);
        }

        public IActionResult CreateAPIConfig()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAPIConfig(IFormCollection collection)
        {
            string noti = string.Empty;
            string result = apiConfig.CreateAPIConfig(collection);
            switch (result)
            {
                case "t":
                    return RedirectToAction("ListAPIConfig");
                case "f":
                    noti = "Saved data failed.";
                    break;
                default:
                    noti = result;
                    break;
            }

            TempData["cNotification"] = noti;
            return this.CreateAPIConfig();
        }

        public IActionResult UpdateAPIConfig(int id)
        {
            var config = apiConfig.GetApiconfigureById(id);
            return View(config);
        }

        [HttpPut]
        public IActionResult UpdateAPIConfig(IFormCollection collection, int id)
        {
            string noti = string.Empty;
            string result = apiConfig.UpdateAPIConfig(collection, id);

            switch (result)
            {
                case "t":
                    return RedirectToAction("ListAPIConfig");
                case "f":
                    noti = "Saved data failed";
                    break;
                default:
                    noti = result;
                    break;
            }

            TempData["uNoti"] = noti;
            return View();
        }

        public IActionResult DeleteAPIConfig(int source)
        {
            var result = apiConfig.GetApiconfigureById(source);
            return View(result);
        }

        [HttpDelete]
        [Authorize(Roles ="3SCOMP")]
        public IActionResult DeleteAPIConfig(IFormCollection collection, int source)
        {
            var result = apiConfig.DeleteAPIConfig(source);

            string noti = string.Empty;

            switch (result)
            {
                case "t":
                    noti = "Data Deleted Success. ";
                    break;
                case "f":
                    noti = "Data Deleted Fail.";
                    break;
                default:
                    noti = result;
                    break;
            }

            TempData["dNoti"] = noti;
            return View();

        }
    }
}
