using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdpApi.Model
{

    public class IdentityProvider
    {
        public IdentityProvider()
        {
            attributes_map = new AttributesMap();
        }
        public string cert { get; set; }
        public AttributesMap attributes_map { get; set; }
        public string entityID { get; set; }
        public string metadata_url { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class AttributesMap
    {
        public string email { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

}
