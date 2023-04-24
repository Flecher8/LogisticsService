using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class LanguageService : ILanguageService
    {
        public string GetLanguageType(string language)
        {
            if(IsLanguageTypeValid(language))
            {
                return Enum.Parse(typeof(LanguageType), language).ToString();
            }
            return default(LanguageType).ToString();
        }

        public bool IsLanguageTypeValid(string language)
        {
            return Enum.IsDefined(typeof(LanguageType), language);
        }
    }
}
