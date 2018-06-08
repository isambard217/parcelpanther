using System;
using Microsoft.AspNetCore.Mvc;

namespace PantherParcel.Controllers {
    
    public class BaseController : Controller {

        public string ConnectionString { get; set; }

        public BaseController(string connection_string) {

            this.ConnectionString = connection_string;
        }
    }
}
