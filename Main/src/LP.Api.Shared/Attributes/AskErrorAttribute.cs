using System;
using System.Web.Http.Filters;
using Ask.Core.Logging;

namespace LP.Api.Shared.Attributes
{
    public class AskErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            var exception = actionExecutedContext.Exception;

            if (exception != null)
            {
                var logger = LoggingServiceLocator.GetLogger();
                const string context = "LP.Host.Api";
                logger.AddErrorLog(
                LoggingServiceLocator.ApplicationEnvironment,
                context,
                string.Format(
                    @"DbUpdateException errors, inner exception:{0}.",
                    exception.InnerException != null ? exception.InnerException.Message : "null"
                    ),
                exception);
            }

            base.OnException(actionExecutedContext);
        }
    }
}
