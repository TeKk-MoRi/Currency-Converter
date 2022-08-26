using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Currency
{
    public class UpdateViewModel
    {
        public IEnumerable<Tuple<string, string, double>> ConversionRates { get; set; }
    }
}
