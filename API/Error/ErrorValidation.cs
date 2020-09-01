using System.Collections.Generic;

namespace API.Error
{
    public class ErrorValidation : ErrorRes
    {
        public ErrorValidation() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}