using LP.PresentationLayer.Filters;
using SpecsFor;

namespace LP.PresentationLayer.Tests.FilterTests.CultureFilterTests
{
    public class BaseGiven : SpecsFor<CultureAttribute>
    {
        protected void PrepareSut()
        {
            SUT = new CultureAttribute();
            
        }
    }
}
