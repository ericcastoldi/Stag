using Humanizer;
using Stag.Configuration;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Stag.Storage
{
    internal class Warehouse<T> : IWarehouse<T>
    {
        private readonly string _storepath;
        private readonly IFileSystem _fileSystem;
        private readonly IJsonSerializer _serializer;

        internal Warehouse()
            : this(new FileSystem(), new JsonSerializer(), new Settings())
        {
        }

        internal Warehouse(IFileSystem fileSystem, IJsonSerializer serializer, ISettings settings)
        {
            var filename = string.Format("{0}-{1}.json", settings.Username, typeof(T).Name.ToLower().Pluralize());

            _fileSystem = fileSystem;
            _serializer = serializer;
            _storepath = fileSystem.Path.Combine(settings.StorageBasePath, filename);

            this.InitializeDirectory(settings);
        }

        private void InitializeDirectory(ISettings settings)
        {
            if (!_fileSystem.Directory.Exists(settings.StorageBasePath))
                _fileSystem.Directory.CreateDirectory(settings.StorageBasePath);

            if (!_fileSystem.File.Exists(_storepath))
                using (var file = _fileSystem.File.Create(_storepath)) { };
        }

        public void Store(T item)
        {
            var items = this.Retrieve().ToList();

            items.Add(item);

            var serializedItems = _serializer.Serialize(items);
            _fileSystem.File.WriteAllText(_storepath, serializedItems, Encoding.UTF8);
        }

        public virtual IQueryable<T> Retrieve()
        {
            var json = _fileSystem.File.ReadAllText(_storepath);
            var items = _serializer.Deserialize<List<T>>(json) ?? new List<T>();

            return items.AsQueryable();
        }
    }
}