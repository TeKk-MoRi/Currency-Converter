using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Currency
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Rate { get; set; }
    }
}
