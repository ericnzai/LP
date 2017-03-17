using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace LP.Api.Shared.Binding
{
    public class CustomArrayParameterRouteBinding : HttpParameterBinding
    {
        private readonly string _parameterName;
        private readonly char _delimiter;

        public CustomArrayParameterRouteBinding(HttpParameterDescriptor descriptor, char delimiter) : base(descriptor)
        {
            
        }

       
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var routeValues = actionContext.ControllerContext.RouteData.Values;

            if (routeValues[_parameterName] != null)
            {

                string[] catchAllValues =
                    routeValues[_parameterName].ToString().Split(_delimiter);

                actionContext.ActionArguments.Add(_parameterName, catchAllValues);
            }
            else
            {

                actionContext.ActionArguments.Add(_parameterName, new string[0]);
            }

            return Task.FromResult(0);
        }
    }
}
