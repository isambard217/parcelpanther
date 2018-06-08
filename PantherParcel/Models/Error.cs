using System;
namespace PantherParcel.Models {
    
    public class Error {

        public int Id { get; set; }

        public string Message { get; set; }

        public string Class { get; set; }

        public string Method { get; set; }

        public Error() {
            
        }
    }
}
