using MyAccountAPI.Producer.Application.Commands;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAccountAPI.Producer.UI.Filters
{
    public class CorrelationFilter : ActionFilterAttribute
    {
        private Guid correlationId;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("command"))
                return;

            CommandBase command = context.ActionArguments["command"] as CommandBase;

            if (command == null)
                return;

            if (context.HttpContext.Request.Headers.ContainsKey("correlationid"))
                correlationId = Guid.Parse(context.HttpContext.Request.Headers["correlationid"]);
            else
                correlationId = Guid.NewGuid();


            string userName = (context.HttpContext.User.Identity as ClaimsIdentity).Claims.Where(e => e.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString();

            command.Header = new Domain.Model.Header(correlationId, userName); 
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            context.HttpContext.Response.Headers.Add("correlationid", correlationId.ToString());
        }
    }
}
