using Microsoft.AspNetCore.Mvc;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IConfiguration Configuration;
        public BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Helpers
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string CurrentLang()
        {
            return "ar";
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        protected string CurrentUser()
        {
            return "";
        }
        #endregion
    }
}
