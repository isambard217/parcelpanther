using System;
using System.Collections.Generic;

namespace PantherParcel.Models {
    
    public class ParcelEnquiry {

        public string fromCountry { get; set; }

        public string fromAddress { get; set; }

        public string toCountry { get; set; }

        public string toAddress { get; set; }

        public string parcelQuantity { get; set; }

        public string parcelDescription { get; set; }

        public List<ParcelDetails> AllParcels { get; set; }

        public string contactNumber { get; set; }

        public ParcelEnquiry() {

            AllParcels = new List<ParcelDetails>();
            
        }
    }
}
