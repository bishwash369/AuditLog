using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditApp.Helper
{
    public interface ICurrentUserService
    {
        string GetCurrentUsername();
    }
}
