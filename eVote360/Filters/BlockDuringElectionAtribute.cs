using eVote360App.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eVote360App.Filters
{
    public class BlockDuringElectionAtribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var eleccionService = context.HttpContext.RequestServices.GetService<IEleccionService>();

            if (eleccionService != null)
            {
                bool getActiveElection = await eleccionService.GetEleccionActivaAsync();

                if (getActiveElection)
                { 
                    var controller = context.Controller as Controller;
                    if (controller != null)
                    {
                        controller.TempData["ElectionError"] = "Acción denegada: Hay una elección en curso. No se pueden realizar modificaciones en este momento.";
                    }

                    var routeController = context.RouteData.Values["controller"]?.ToString();
                    context.Result = new RedirectToActionResult("Index", routeController, null);
                    return;
                }
            }
            await next();
        }
    }
}
