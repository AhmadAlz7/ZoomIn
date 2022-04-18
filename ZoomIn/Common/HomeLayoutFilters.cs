using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZoomIn.Models;

namespace ZoomIn.Common
{
    public class HomeLayoutFilters : ActionFilterAttribute, IActionFilter
    {
        private readonly ModelContext _context;

        public HomeLayoutFilters(ModelContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;
            controller.ViewBag.Configurations = _context.Configurations.FirstOrDefault();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Do something after the action executes.
        }
    }
}
