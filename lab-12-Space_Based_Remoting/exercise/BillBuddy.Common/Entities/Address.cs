using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Entities
{


        [Serializable]
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public CountryNames? Country { get; set; }

        public int? ZipCode { get; set; }

    }
}
