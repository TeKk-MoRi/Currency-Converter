using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Currency
{
    public class CurrencyViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
    }
}
