using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PhotographerProfile
    {
        public Guid PhotographerId { get; private set; } 
        public string Phone { get; private set; }
        public string Website { get; private set; }
        public string Instagram { get; private set; }

        public Photographer Photographer { get; private set; } = null!;

        private PhotographerProfile() { }

        private PhotographerProfile(Guid photographerId, string phone, string website, string instagram)
        {
            PhotographerId = photographerId;
            Phone = phone;
            Website = website;
            Instagram = instagram;
        }

        public static PhotographerProfile Create(Guid photographerId, string phone, string website, string instagram)
            => new(photographerId, phone, website, instagram);

        public void Update(string phone, string website, string instagram)
        {
            Phone = phone;
            Website = website;
            Instagram = instagram;
        }
    }
}
