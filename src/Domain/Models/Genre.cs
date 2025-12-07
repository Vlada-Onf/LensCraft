using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Genre
    {
        public Guid Id { get; }
        public string Name { get; private set; }

        public ICollection<PhotographerGenre> Photographers { get; }

        private Genre(Guid id, string name)
        {
            Id = id;
            Name = name;
            Photographers = new List<PhotographerGenre>();
        }

        public static Genre Create(Guid id, string name)
            => new Genre(id, name);

        public void Rename(string name)
        {
            Name = name;
        }
    }
}
