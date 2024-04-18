using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AK_CollectionsOrganizer.Models
{
    internal class Collection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        //private ObservableCollection<Collectable> _Collectables;
        public ObservableCollection<Collectable> Collectables { get; set; }
        //{
        //    get {
        //        //if (Collectables == null)
        //        //{
        //        //return new ObservableCollection<Collectable>();
        //        //}
        //        //LoadCollectablesToCollection();
        //        //return Collectables;
        //    }
        //    set {
        //        //LoadCollectablesToCollection();
        //    }
        //}
        = new ObservableCollection<Collectable>();

        Collection()
        {
            Id = Guid.NewGuid();
            FileName = $"{Id}.col";
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + FileName;
        }

        public Collection(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            FileName = $"{Id}.col";
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + FileName;
            //Collectables = new ObservableCollection<Collectable>();
            LoadCollectablesToCollection();
            //_Collectables = new ObservableCollection<Collectable>();
        }

        public Collection(string id, string name, string description)
        {
            Name = name;
            Id = Guid.Parse(id);
            Description = description;
            FileName = $"{Id}.col";
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + FileName;
            LoadCollectablesToCollection();
            //Collectables = new ObservableCollection<Collectable>();
            //_Collectables = new ObservableCollection<Collectable>();
        }

        public string ToString()
        {
            //return $"'{Id}','{Name}','{Description}'";
            return $"{Id},{Name},{Description}";

        }

        public void LoadCollectablesToCollection()
        {
            //Collectables.Clear();
            ReadCollectablesFromFile();
        }

        private void Collectables_CollectionChanged(Object sender, CollectionChangeEventArgs e)
        {
            //FilesAccess.Instance.SaveCollectables(this);
            SaveCollectablesToFile();

        }

        public static Collectable FromString(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid format for Collectable string representation.");
            }

            return new Collectable { Id = Guid.Parse(parts[0]), Name = parts[1], Description = parts[2] };
        }

        //public Collection()
        //{
        //    Collectables = new List<Collectable>();
        //    FilePath = "Collectables.txt"; // Default file path
        //}

        public void SaveCollectablesToFile()
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (var Collectable in Collectables)
                {
                    writer.WriteLine(Collectable.ToString());
                }
            }
        }

        public void ReadCollectablesFromFile()
        {
            if (Collectables != null) 
            { 
                if (File.Exists(FilePath))
                {
                    Collectables.Clear(); // Clear existing Collectables before reading
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Collectables.Add(FromString(line));
                        }
                    }
                } else
                {
                    using (FileStream fs = File.Create(FilePath))
                    {
                        Collectables = new ObservableCollection<Collectable>();
                    }
                }
            }
        }

        //private Collectable CollectableFromString(string s)
        //{
        //    return Collectable.FromString(s);
        //}


    }
}
