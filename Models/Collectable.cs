namespace AK_CollectionsOrganizer.Models
{
    internal class Collectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime DateOfEdit { get; set; }

        public Collectable(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Collectable(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public Collectable(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = "";
        }

        public string ToString()
        {
            return $"{Id},{Name},{Description}";

        }

        public static Collectable CollectableFromString(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid format for Collectable string representation.");
            }

            return new Collectable { Id = Guid.Parse(parts[0]), Name = parts[1], Description = parts[2] };
        }

        public Collectable()
        {
        }
    }
}
