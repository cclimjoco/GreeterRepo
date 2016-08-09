using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Domain.Greeter
{
    public  interface ILog
    {
        void Log(string message, string stacktrace);
    }
}
