using Extension;
using Microsoft.AspNetCore.Mvc;
using Service.Core.User;
using Service.ViewModel.Currency;

namespace CurrencyConverter.Api.Controllers
{
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyConverter _currencyService;
        public CurrencyController(ICurrencyConverter currencyService)
        {
            this._currencyService = currencyService;
        }
        [HttpGet]
        public IActionResult Convert()
        {
            _currencyService.ClearConfiguration();
            return Ok();
        }
        [HttpPost]
        public IActionResult Convert(CurrencyViewModel model)
        {
            var res = _currencyService.Convert(new Service.Messaging.Core.ConvertRequest { ViewModel = model });
            return Response(res);
        }
        [HttpPost]
        public IActionResult UpdateOrAddConfiguration(UpdateViewModel model)
        {
            var res = _currencyService.UpdateConfiguration(new Service.Messaging.Core.UpdateRequest { ViewModel = model });
            return Response(res);
        }
    }
}
