using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PhotographerGenre
    {
        public Guid PhotographerId { get; }
        public Photographer Photographer { get; }

        public Guid GenreId { get; }
        public Genre Genre { get; }

        private PhotographerGenre(Guid photographerId, Guid genreId)
        {
            PhotographerId = photographerId;
            GenreId = genreId;
        }

        public static PhotographerGenre Create(Guid photographerId, Guid genreId)
            => new PhotographerGenre(photographerId, genreId);
    }
}
