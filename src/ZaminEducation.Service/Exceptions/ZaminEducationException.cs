using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaminEducation.Service.Exceptions;

public class ZaminEducationException : Exception
{
    public int Code { get; set; }
    public ZaminEducationException(int code, string message) 
        : base(message)
    {
        this.Code = code;
    }
}
