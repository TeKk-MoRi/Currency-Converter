using Service.Messaging.Core;
using Service.ViewModel.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = Domain;

namespace Service.Core.User
{
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Clears any prior configuration.
        /// </summary>
        void ClearConfiguration();
        /// <summary>
        /// Updates the configuration. Rates are inserted or replaced internally.
        /// </summary>
        UpdateResponse UpdateConfiguration(UpdateRequest conversionRates);
        /// <summary>
        /// Converts the specified amount to the desired currency. 
        /// </summary>
        ConvertResponse Convert(ConvertRequest currencyViewModel);
    }
}
