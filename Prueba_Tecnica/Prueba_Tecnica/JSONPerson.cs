using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Prueba_Tecnica
{
    public class JSONPerson
    {
        [DataMember(Name = "results")]
        public Person [] results { get; set; }
        [DataMember(Name = "info")]
        public Info info { get; set; }
        
    }

    public class Person
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
        public string email { get; set; }
        public Login login { get; set; }
        public string dob { get; set; }
        public string registered { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        //public string id { get; set; }
        public Imagen picture { get; set; }
    }

    public class Login {
        public string username { get; set; }
    }

    public class Info {
        public string seed { get; set; }
        public int results { get; set; }
        public int page { get; set; } 
        public string version { get; set; }
    }


    public class Name {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Location {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
    }
    public class Imagen {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }
}
