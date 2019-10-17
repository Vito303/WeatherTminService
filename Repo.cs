using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text;

namespace WinService.NetCore
{
    [DataContract(Name = "repo")]
    public class Repository
    {
        [DataMember(Name = "place")]
        public string Place { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "last_update")]
        public string Last_update { get; set; }

        [DataMember(Name = "current")]
        public string Current { get; set; }

        [DataMember(Name = "temperature")]
        public string Temperature { get; set; }

        [DataMember(Name = "max")]
        public string Max { get; set; }

        [DataMember(Name = "min")]
        public string Min { get; set; }

        [DataMember(Name = "unit")]
        public string Unit { get; set; }

        [IgnoreDataMember]
        public DateTime LastUpdate {
            get {
                //return DateTime.ParseExact(Last_update, "dd/M/yyyy tt HH:mm", CultureInfo.InvariantCulture);
                return DateTime.Now;
            }
        }
    }
}
