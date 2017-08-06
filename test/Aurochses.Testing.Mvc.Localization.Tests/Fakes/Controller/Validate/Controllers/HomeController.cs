using Microsoft.Extensions.Localization;

namespace Aurochses.Testing.Mvc.Localization.Tests.Fakes.Controller.Validate.Controllers
{
    public class HomeController
    {
        private readonly IStringLocalizer<HomeController> _controllerLocalization;

        public HomeController(IStringLocalizer<HomeController> controllerLocalization)
        {
            _controllerLocalization = controllerLocalization;
        }

        public string Index()
        {
            const string predefinedTestName = "PredefinedTestName";

            var predefinedTestValue = _controllerLocalization[predefinedTestName];

            return _controllerLocalization["TestName"] + _controllerLocalization["SecondTestName"] + predefinedTestValue;
        }
    }
}