using CED.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CED.Controllers
{
    public class CEDBaseController : ControllerBase
    {
        protected string RetrieveToken()
        {
            return HttpContext?.Request?.Headers?.FirstOrDefault(a => a.Key == "Authorization")
                .Value.FirstOrDefault().Remove(0, 7);
        }

        protected GenericResponseDTO GenerateErrorResponse(string message, object data)
        {
            return new GenericResponseDTO()
            {
                message = message,
                status = "FAILURE",
                data = data
            };
        }

        protected GenericResponseDTO GenerateSuccessResponse(string message, object data)
        {
            return new GenericResponseDTO()
            {
                message = message,
                status = "SUCCESS",
                data = data
            };
        }
    }
}
