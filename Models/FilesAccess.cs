using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AK_CollectionsOrganizer.Models
{
    class FilesAccess : INotifyPropertyChanged
    {
        public static FilesAccess Instance { get; } = Instance == null ? new FilesAccess() : Instance;
        public const string FileName = "collections_names.col";
        public string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + FileName;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Collection> AllCollections { get; set; } //= new ObservableCollection<Collection>();

        public void AddCollectionToCollections(Collection collectionToSave)
        {
            AllCollections.Add(collectionToSave);
            SaveCollections();
        }

        public void AddCollectableToCollection(Collectable collectableToSave, string targetName)
        {
            Collection target = FindCollectionByName(targetName); //AllCollections.FirstOrDefault(collection => collection.Name == targetName);
            if (target != null)
            {
                target.Collectables.Add(collectableToSave);
                target.SaveCollectablesToFile();
            }
        }
        public void RemoveCollectableToCollection(Collectable collectableToDelete, string targetName)
        {
            Collection target = FindCollectionByName(targetName); //AllCollections.FirstOrDefault(collection => collection.Name == targetName);
            if (target != null)
            {
                target.Collectables.Remove(collectableToDelete);
                target.SaveCollectablesToFile();
            }
        }

        private void AllCollections_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            SaveCollections();

        }
        public void SaveCollectables(Collection target) 
        {
            try
            {
                //string dataToSave;

                using (StreamWriter writer = new StreamWriter(target.FilePath))
                {
                    foreach (var Collectable in target.Collectables)
                    {
                        writer.WriteLine(Collectable.ToString());
                        target.SaveCollectablesToFile();
                    }
                }

                //dataToSave = JsonSerializer.Serialize(AllCollections);
                //File.WriteAllText(FilePath, dataToSave);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving classes: {ex.Message}");
            }
        }

        public void LoadAllCollections()
        {
            try
            {
                //string jsonData;

                if (File.Exists(FilePath))
                {
                    AllCollections = new ObservableCollection<Collection>();
                    //if (AllCollections == null)
                    //{
                    //    AllCollections = new ObservableCollection<Collection>();
                    //}


                    //AllCollections.Clear(); // Clear existing collectibles before reading
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            AllCollections.Add(FromStringCollection(line));
                        }
                    }
                    //jsonData = File.ReadAllText(SchoolClassesFilePath);
                    //return JsonSerializer.Deserialize<ObservableCollection<SchoolClass>>(jsonData);
                }
                else
                {
                    Console.WriteLine($"File {FileName} does not exist, creating...");
                    AllCollections = new ObservableCollection<Collection>();

                    try
                    {
                        using (FileStream fs = File.Create(FilePath)) 
                        using (StreamWriter writer = new StreamWriter(FilePath))
                        {
                            //writer.WriteLine(AllCollections.ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating file '{FilePath}': {ex.Message}");
                    }

                    //jsonData = JsonSerializer.Serialize(new ObservableCollection<SchoolClass>());
                    //File.WriteAllText(SchoolClassesFilePath, jsonData);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erroe loading prducts: {ex.Message}");
                AllCollections = new ObservableCollection<Collection>();
            }
        }

        public static Collection FromStringCollection(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid format for Collectable string representation.");
            }

            //return new Collection { Id = Guid.Parse(parts[0]), Name = parts[1], Description = parts[2] };
            return new Collection(parts[0], parts[1], parts[2]);
        }

        public static Collectable FromStringCollectable(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid format for Collectable string representation.");
            }

            return new Collectable { Id = Guid.Parse(parts[0]), Name = parts[1], Description = parts[2] };
        }

        public void SaveCollections()
        {
            try
            {
                //string dataToSave;

                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (var Collection in AllCollections)
                    {
                        writer.WriteLine(Collection.ToString());
                        Collection.SaveCollectablesToFile();
                    }
                }

                //dataToSave = JsonSerializer.Serialize(AllCollections);
                //File.WriteAllText(FilePath, dataToSave);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving classes: {ex.Message}");
            }

        }

        public Collection FindCollectionByName(string targetName)
        {
            Collection foundCollection;

            foundCollection = AllCollections.FirstOrDefault(collection => collection.Name == targetName);

            if (foundCollection != null)
            {
                return foundCollection;
            }
            else
            {
                return null;
            }
        }

    }
}
