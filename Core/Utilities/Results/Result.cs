using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success)
        {
            //ctor'un base ile çalışması 
            Message = message;
            
        }
        public Result(bool success)
        {
            Success = success;
            //overloading :aşırı yükleme 
        }

        public bool Success { get; }
        //get :yalnızca ctorda set edilebilirler.

        public string Message { get; }
        //readonlyler yalnızca ctorda  set edilebilirler.
    }
}
