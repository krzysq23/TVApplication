using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TVApi.Models
{
    public class RegisterExternalBindingModel
    {
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ExternalAccessToken { get; set; }
        public string UserName { get; set; }
    }
}