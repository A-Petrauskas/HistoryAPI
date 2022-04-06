using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Services.Contracts
{
    public class CreationContract
    {
        public string name { get; set; }

        public string description { get; set; }

        public string imageSrc { get; set; }

        public int timeConstraint { get; set; }

        public int mistakes { get; set; }

        public List<CreationEventContract> events { get; set; }

        public IFormFile image { get; set; }
    }
}