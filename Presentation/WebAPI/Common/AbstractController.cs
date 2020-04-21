using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Todo.WebAPI.Common
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public abstract class AbstractController : Controller
    {
        private const int _defaultPageSize = 10;

        /// <summary>
        /// Get pagination values from the header
        /// </summary>
        /// <returns>Tuple of Page and PageSize</returns>
        protected (int page, int pageSize) GetPagination()
        {
            return (GetPage(), GetPageSize());
        }

        #region Private Methods

        private int GetPage()
        {
            Request.Headers.TryGetValue("X-Page-Number", out var headerValues);

            var headerValue = headerValues.FirstOrDefault();

            if (headerValue != null && int.TryParse(headerValue, out var pageValue))
            {
                return pageValue;
            }
            else
            {
                return 0;
            }
        }

        private int GetPageSize()
        {
            Request.Headers.TryGetValue("X-Page-Size", out var headerValues);

            var headerValue = headerValues.FirstOrDefault();

            if (headerValue != null && int.TryParse(headerValue, out var pageSizeValue))
            {
                return pageSizeValue;
            }
            else
            {
                return _defaultPageSize;
            }
        }

        #endregion Private Methods
    }
}