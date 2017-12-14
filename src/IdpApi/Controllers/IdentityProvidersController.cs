using CsvHelper;
using CsvHelper.Configuration;
using IdpApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdpApi.Controllers
{
    [Route("api/[controller]")]
    public class IdentityProvidersController : Controller
    {
        private readonly IHostingEnvironment env;
        public IdentityProvidersController(IHostingEnvironment env)
        {
            this.env = env;
        }

        [HttpGet]
        public List<IdentityProvider> GetIdentityProviders()
        {
            var identityProviders = new List<IdentityProvider>();
            using (var reader = System.IO.File.OpenText(Path.Combine(env.ContentRootPath, "data.csv")))
            {
                var config = new CsvConfiguration { HasHeaderRecord = false, Delimiter = "\t", Quote = '"', QuoteAllFields = true, };

                var csv = new CsvReader(reader, config);
                while (csv.Read())
                {
                    var name = csv.GetField<string>(0);
                    var metadataUrl = csv.GetField<string>(1);
                    var certificate = csv.GetField<string>(2).Replace(" ", "").Replace("\r", "").Replace("\t", "");
                    var attributes = JsonConvert.DeserializeObject<AttributesMap>(csv.GetField<string>(3));
                    identityProviders.Add(new IdentityProvider
                    {
                         metadata_url = metadataUrl,
                         cert = certificate,
                         entityID = metadataUrl,
                         logo = $"{Request.Scheme}://{Request.Host}/logo.png",
                         name = name,
                         attributes_map = attributes
                    });
                }
            }

            return identityProviders;
        }
    }
}
