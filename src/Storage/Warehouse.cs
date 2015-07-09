using Humanizer;
using Newtonsoft.Json;
using Stag.Configuration;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Stag.Storage
{
    internal class Warehouse<T>
    {
        private string _storepath;
        private IFileSystem _fileSystem;
        private IJsonSerializer _serializer;

        internal Warehouse()
            : this(new Settings(), new FileSystem(), new JsonSerializer())
        {

        }

        internal Warehouse(ISettings settings, IFileSystem fileSystem, IJsonSerializer serializer)
        {
            _fileSystem = fileSystem;
            _serializer = serializer;

            var filename = string.Format("{0}.json", typeof(T).Name.ToLower().Pluralize());
            _storepath = fileSystem.Path.Combine(settings.StorageBasePath, filename);

            if (!fileSystem.Directory.Exists(settings.StorageBasePath)) 
                fileSystem.Directory.CreateDirectory(settings.StorageBasePath);

            if (!fileSystem.File.Exists(_storepath)) 
                using (var file = fileSystem.File.Create(_storepath)) { };
        }

        public virtual void Store(T item)
        {
            var items = this.Retrieve().ToList();
            items.Add(item);

            var serializedItems = _serializer.Serialize(items);
            _fileSystem.File.WriteAllText(_storepath, serializedItems, Encoding.UTF8);
        }

        public virtual IQueryable<T> Retrieve()
        {
            using (System.IO.StreamReader reader = _fileSystem.File.OpenText(_storepath))
            {
                var json = reader.ReadToEnd();
                var items = _serializer.Deserialize<List<T>>(json) ?? new List<T>();

                return items.AsQueryable();
            }
        }

    }
}
