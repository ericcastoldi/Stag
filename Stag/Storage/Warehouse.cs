using Humanizer;
using Newtonsoft.Json;
using Stag.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stag.Storage
{
    public class Warehouse<T>
    {
        private string _storepath;

        public Warehouse()
        {
            var settings = new Settings();

            var filename = string.Format("{0}.json", typeof(T).Name.ToLower().Pluralize());
            _storepath = Path.Combine(settings.StorageBasePath, filename);

            if(!Directory.Exists(settings.StorageBasePath))
                Directory.CreateDirectory(settings.StorageBasePath);
        
            if (!File.Exists(_storepath))
                using (var file = File.Create(_storepath)) ;
        }

        public virtual void Store(T item)
        {
            var items = this.Retrieve().ToList();
            items.Add(item);

            var serializedItems = JsonConvert.SerializeObject(items);
            File.WriteAllText(_storepath, serializedItems, Encoding.UTF8);
        }

        public virtual IQueryable<T> Retrieve()
        {
            using (StreamReader reader = new StreamReader(_storepath))
            {
                var json = reader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();

                return items.AsQueryable();
            }
        }

    }
}
