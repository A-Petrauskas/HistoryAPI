using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public class CreationEventContract
    {
        public string date { get; set; }

        public string description { get; set; }

        public string imageSrc { get; set; }

        public IFormFile image { get; set; }
    }
}
