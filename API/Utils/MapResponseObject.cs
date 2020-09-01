using AutoWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object Error { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
        public string Message { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_Details)]
        public string StackTrace { get; set; }

        [AutoWrapperPropertyMap(Prop.IsError)]
        public bool IsError { get; set; }

        [AutoWrapperPropertyMap(Prop.StatusCode)]
        public int StatusCode { get; set; }

        [AutoWrapperPropertyMap(Prop.Result)]
        public object Result { get; set; }
    }
}
