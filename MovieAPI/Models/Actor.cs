using System.Collections.Generic;

namespace MovieAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int BirthYear { get; set; }

        // N:M med Movie via MovieActor
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
