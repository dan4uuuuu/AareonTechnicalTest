
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Utils.Exceptions;
using static Utils.Exceptions.CustomErrorCodes;

namespace Utils
{
    public abstract class ErrorsController : ControllerBase
    {
        protected ActionResult UnprocessableEntity(string error = null, List<string> args = null)
        {
            error ??= UNPROCESSABLE_ENTITY;
            return this.UnprocessableEntity(new ErrorResponse(error, args, 422));
        }

        protected ActionResult NotFound(string error = null, List<string> args = null)
        {
            error ??= NOT_FOUND;
            return this.NotFound(new ErrorResponse(error, args, 404));
        }

        protected ActionResult BadRequest(string error = null, List<string> args = null)
        {
            error ??= BAD_REQUEST;
            return this.BadRequest(new ErrorResponse(error, args, 400));
        }
    }
}
