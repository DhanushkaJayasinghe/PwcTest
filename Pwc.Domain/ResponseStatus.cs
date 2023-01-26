using System;

namespace Pwc.Domain
{
    public class ResponseStatus
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public Guid ResultGuid { get; set; }
        public string Message { get; set; }
    }
}
