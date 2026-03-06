using Microsoft.AspNetCore.Mvc;
using SevicesDemo.Models;
using SevicesDemo.Services;

namespace SevicesDemo.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IBasicCalculatorService _basicCalc;
        private readonly IAdvancedCalculatorService _advancedCalc;

        public CalculatorController(IBasicCalculatorService basicCalc, IAdvancedCalculatorService advancedCalc)
        {
            _basicCalc = basicCalc;
            _advancedCalc = advancedCalc;
        }

        public IActionResult Index()
        {
            return View(new CalculatorViewModel());
        }

        [HttpPost]
        public IActionResult Calculate(CalculatorViewModel model)
        {
            if (model.Operation == "add")
                model.Result = _basicCalc.Add(model.Number1, model.Number2);
            else if (model.Operation == "subtract")
                model.Result = _advancedCalc.Subtract(model.Number1, model.Number2);

            return View("Index", model);
        }
    }
}
