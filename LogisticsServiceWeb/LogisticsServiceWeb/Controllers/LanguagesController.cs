using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IStringLocalizer<LanguagesController> stringLocalizer;

        public LanguagesController(IStringLocalizer<LanguagesController> localizer)
        {
            stringLocalizer = localizer;
        }


        /// <summary>
        /// https://localhost:7018/api/Languages?Ui-culture=uk-UA
        /// https://localhost:7018/api/Languages?Ui-culture=en-US
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult GetLanguage()
        {
            var result = stringLocalizer.GetAllStrings(true);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var keyValue in result)
            {
                keyValuePairs.Add(keyValue.Name, keyValue.Value);
            }
            return new JsonResult(keyValuePairs);
        }
    }
}
