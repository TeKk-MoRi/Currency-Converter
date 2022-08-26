using Messaging.Base;
using Service.ViewModel.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Messaging.Core
{
    public class ConvertRequest : BaseApiRequest<CurrencyViewModel> { }
    public class UpdateRequest : BaseApiRequest<UpdateViewModel> { }
}
