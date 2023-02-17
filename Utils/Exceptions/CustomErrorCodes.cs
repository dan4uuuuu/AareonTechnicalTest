using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Exceptions
{
    public static class CustomErrorCodes
    {
        public const string UNPROCESSABLE_ENTITY = nameof(UNPROCESSABLE_ENTITY);
        public const string NOT_FOUND = nameof(NOT_FOUND);
        public const string BAD_REQUEST = nameof(BAD_REQUEST);
        public const string INTERNAL_SERVER_ERROR = nameof(INTERNAL_SERVER_ERROR);
        public const string INSUFFICIENT_RIGHTS = nameof(INSUFFICIENT_RIGHTS);
        public const string INSUFFICIENT_FUNDS = nameof(INSUFFICIENT_FUNDS);
        public const string USER_NOT_FOUND = nameof(USER_NOT_FOUND);
    }
}
