#region Using ...
using System;
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
