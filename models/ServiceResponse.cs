using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace complainSystem.models
{
    public class ServiceResponse<T>
    {
     
        public T? Data {get; set;} = default;
        public int StatusCode { get; set; }
        public string Message { get; set; } =string.Empty;
        public bool Success { get; set; } = true;
        public List<string>? ValidationMessages { get; set; } = new List<string>();
        public string? access_token { get; set; }
        
        // public Exception Exception { get; set; }
        // public bool IsError { get; set; }
        // public bool IsWarning { get; set; }
        // public bool IsInfo { get; set; }
        // public bool IsValidation { get; set; }
        // public bool IsUnauthorized { get; set; }
        // public bool IsForbidden { get; set; }
        // public bool IsNotFound { get; set; }
        // public bool IsBadRequest { get; set; }
        // public bool IsOk { get; set; }
        // public bool IsCreated { get; set; }
        // public bool IsNoContent { get; set; }

    
}




    }
