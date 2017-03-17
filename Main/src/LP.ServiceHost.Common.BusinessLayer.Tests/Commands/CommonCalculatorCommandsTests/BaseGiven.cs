using LP.ServiceHost.Common.BusinessLayer.Commands;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CommonCalculatorCommandsTests
{
    public class BaseGiven : SpecsFor<CommonCalculatorCommands>
    {
        protected void PrepareSut()
        {
            SUT = new CommonCalculatorCommands();
        }
    }
}
