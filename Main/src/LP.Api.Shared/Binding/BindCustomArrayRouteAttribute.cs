using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LP.Api.Shared.Binding
{
    public class BindCustomArrayRouteAttribute : ParameterBindingAttribute
    {
        private readonly char _delimiter;

        public BindCustomArrayRouteAttribute(char delimiter)
        {
            _delimiter = delimiter;
        }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new CustomArrayParameterRouteBinding(parameter,_delimiter);
        }
    }
}
