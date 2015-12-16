using Moq;
using Stag.Configuration;
using Stag.Storage;
using Stag.Tasks;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Xunit;

namespace Stag.Test.Storage
{
    public class WarehouseTest
    {
        private const string StoragePath = "C:\\storagebasepath";

        private Mock<ISettings> CreateSettingsMock()
        {
            var settingsMock = new Mock<ISettings>();
            settingsMock
                .SetupGet(p => p.Username)
                .Returns("john.doe")
                .Verifiable("O Username deve ser utilizado para montar o nome do arquivo de storage.");

            settingsMock
                .SetupGet(p => p.StorageBasePath)
                .Returns(StoragePath)
                .Verifiable("O StorageBasePath deve ser utilizado para a criação/leitura do arquivo de storage.");

            return settingsMock;
        }

        private Mock<PathBase> CreatePathMock()
        {
            var pathMock = new Mock<PathBase>();
            pathMock
                .Setup(p => p.Combine(StoragePath, "john.doe-tasks.json"))
                .Returns(StoragePath + "\\john.doe-tasks.json")
                .Verifiable("O caminho do arquivo de storage deve ser montado utilizando Path.Combine.");

            return pathMock;
        }

        private Mock<IFileSystem> CreateFileSystemMock(PathBase pathBase, DirectoryBase directoryBase, FileBase fileBase)
        {
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock
                .SetupGet(p => p.Path)
                .Returns(pathBase)
                .Verifiable("O objeto Path do IFileSystem deve ser utilizado para montar o caminho do arquivo de storage.");

            fileSystemMock
                .SetupGet(p => p.Directory)
                .Returns(directoryBase)
                .Verifiable("O objeto Directory do IFileSystem deve ser utilizado para verificar a existência e para criar a pasta de storage.");

            fileSystemMock
                .SetupGet(p => p.File)
                .Returns(fileBase)
                .Verifiable("O objeto File do IFileSystem deve ser utilizado para verificar a existência e para criar o arquivo de storage.");

            return fileSystemMock;
        }

        [Fact]
        public void ShouldInitializeDirectory()
        {
            // Arrange
            var settingsMock = CreateSettingsMock();

            var pathMock = CreatePathMock();

            var directoryMock = new Mock<DirectoryBase>();
            directoryMock
                .Setup(p => p.Exists(StoragePath))
                .Returns(true)
                .Verifiable("Deve ser checada a existência da pasta de storage utilizando Directory.Exists.");

            var fileMock = new Mock<FileBase>();
            fileMock
                .Setup(p => p.Exists(StoragePath + "\\john.doe-tasks.json"))
                .Returns(true)
                .Verifiable("Deve ser checada a existência do arquivo de storage utilizando File.Exists.");

            var fileSystemMock = CreateFileSystemMock(pathMock.Object, directoryMock.Object, fileMock.Object);

            // Act
            var warehouse = new Warehouse<Task>(fileSystemMock.Object, Mock.Of<IJsonSerializer>(), settingsMock.Object);

            // Assert
            settingsMock.VerifyAll();

            pathMock.VerifyAll();

            directoryMock.VerifyAll();
            directoryMock.Verify(p => p.CreateDirectory(It.IsAny<string>()), Times.Never,
                "Quando a pasta de storage já existe ela não deve ser criada novamente.");

            fileMock.VerifyAll();
            fileMock.Verify(p => p.Create(It.IsAny<string>()), Times.Never,
                "Quando o arquivo de storage já existe ele não deve ser criado novamente.");
        }

        [Fact]
        public void ShouldCreateDirectoryAndFileOnInitialization()
        {
            // Arrange
            var settingsMock = CreateSettingsMock();

            var pathMock = CreatePathMock();

            var directoryMock = new Mock<DirectoryBase>();
            directoryMock
                .Setup(p => p.Exists(StoragePath))
                .Returns(false)
                .Verifiable("Deve ser checada a existência da pasta de storage utilizando Directory.Exists.");

            directoryMock
                .Setup(p => p.CreateDirectory(It.IsAny<string>()))
                .Verifiable("A pasta de storage deve ser criada caso não exista.");

            var fileMock = new Mock<FileBase>();
            fileMock
                .Setup(p => p.Exists(StoragePath + "\\john.doe-tasks.json"))
                .Returns(false)
                .Verifiable("Deve ser checada a existência do arquivo de storage utilizando File.Exists.");

            fileMock
                .Setup(p => p.Create(It.IsAny<string>()))
                .Verifiable("O arquivo de storage deve ser criado caso não exista.");

            var fileSystemMock = CreateFileSystemMock(pathMock.Object, directoryMock.Object, fileMock.Object);

            // Act
            var warehouse = new Warehouse<Task>(fileSystemMock.Object, Mock.Of<IJsonSerializer>(), settingsMock.Object);

            // Assert
            settingsMock.VerifyAll();

            pathMock.VerifyAll();

            directoryMock.VerifyAll();

            fileMock.VerifyAll();
        }

        [Fact]
        public void ShouldStore()
        {
            // Arrange
            var settingsMock = CreateSettingsMock();
            var pathMock = CreatePathMock();
            var task = new Task("Minha tarefa", "123456");
            var taskList = new List<Task>() { new Task("Tarefa 1", "111111"), new Task("Tarefa complexa", "333222") };

            var serializerMock = new Mock<IJsonSerializer>();
            serializerMock
                .Setup(p => p.Serialize(It.Is<List<Task>>(l => l.Any(t => t.Id == task.Id))))
                .Verifiable("Os objetos a serem armazenados devem ser serializados.");

            var directoryMock = new Mock<DirectoryBase>();
            directoryMock
                .Setup(p => p.Exists(StoragePath))
                .Returns(true)
                .Verifiable("Deve ser checada a existência da pasta de storage utilizando Directory.Exists.");

            var fileMock = new Mock<FileBase>();
            fileMock
                .Setup(p => p.Exists(StoragePath + "\\john.doe-tasks.json"))
                .Returns(true)
                .Verifiable("Deve ser checada a existência do arquivo de storage utilizando File.Exists.");

            fileMock
                .Setup(p => p.WriteAllText(StoragePath + "\\john.doe-tasks.json", It.IsAny<string>(), Encoding.UTF8))
                .Verifiable("O Json serializado deve ser armazenado no arquivo de storage");

            var fileSystemMock = CreateFileSystemMock(pathMock.Object, directoryMock.Object, fileMock.Object);

            var warehouseMock = new Mock<Warehouse<Task>>(fileSystemMock.Object, serializerMock.Object, settingsMock.Object);
            warehouseMock
                .Setup(p => p.Retrieve())
                .Returns(taskList.AsQueryable())
                .Verifiable("Devem ser buscados todos os registros antes de salvar um novo registro.");

            warehouseMock.CallBase = true;

            // Act
            warehouseMock.Object.Store(task);

            // Assert
            settingsMock.VerifyAll();

            pathMock.VerifyAll();

            serializerMock.VerifyAll();

            directoryMock.VerifyAll();

            fileMock.VerifyAll();

            fileSystemMock.VerifyAll();

            warehouseMock.VerifyAll();
        }

        [Fact]
        public void ShouldRetrieve()
        {
            // Arrange
            var taskList = new List<Task>() { new Task("Tarefa 1", "111111"), new Task("Tarefa complexa", "333222") };

            var settingsMock = CreateSettingsMock();

            var pathMock = CreatePathMock();

            var serializerMock = new Mock<IJsonSerializer>();
            serializerMock
                .Setup(p => p.Deserialize<List<Task>>("{json:true}"))
                .Returns(taskList)
                .Verifiable("Os objetos armazenados devem ser deserializados.");

            var directoryMock = new Mock<DirectoryBase>();
            directoryMock
                .Setup(p => p.Exists(StoragePath))
                .Returns(true)
                .Verifiable("Deve ser checada a existência da pasta de storage utilizando Directory.Exists.");

            var fileMock = new Mock<FileBase>();
            fileMock
                .Setup(p => p.Exists(StoragePath + "\\john.doe-tasks.json"))
                .Returns(true)
                .Verifiable("Deve ser checada a existência do arquivo de storage utilizando File.Exists.");

            fileMock
                .Setup(p => p.ReadAllText(StoragePath + "\\john.doe-tasks.json"))
                .Returns("{json:true}")
                .Verifiable("Deve ser realizada a leitura do arquivo de storage.");

            var fileSystemMock = CreateFileSystemMock(pathMock.Object, directoryMock.Object, fileMock.Object);

            var warehouse = new Warehouse<Task>(fileSystemMock.Object, serializerMock.Object, settingsMock.Object);

            // Act
            var tasks = warehouse.Retrieve();

            // Assert
            Assert.Equal<int>(2, tasks.Count());

            Assert.True(tasks.Any(p => p.Id == "111111" && p.Title == "Tarefa 1"));

            Assert.True(tasks.Any(p => p.Id == "333222" && p.Title == "Tarefa complexa"));

            settingsMock.VerifyAll();

            pathMock.VerifyAll();

            serializerMock.VerifyAll();

            directoryMock.VerifyAll();

            fileMock.VerifyAll();

            fileSystemMock.VerifyAll();
        }

        [Fact]
        public void ShouldRetrieveEmptyListWhenNothingIsStored()
        {
            // Arrange
            var settingsMock = CreateSettingsMock();

            var pathMock = CreatePathMock();

            var serializerMock = new Mock<IJsonSerializer>();
            serializerMock
                .Setup(p => p.Deserialize<List<Task>>("{json:true}"))
                .Returns((List<Task>)null)
                .Verifiable("Os objetos armazenados devem ser deserializados.");

            var directoryMock = new Mock<DirectoryBase>();
            directoryMock
                .Setup(p => p.Exists(StoragePath))
                .Returns(true)
                .Verifiable("Deve ser checada a existência da pasta de storage utilizando Directory.Exists.");

            var fileMock = new Mock<FileBase>();
            fileMock
                .Setup(p => p.Exists(StoragePath + "\\john.doe-tasks.json"))
                .Returns(true)
                .Verifiable("Deve ser checada a existência do arquivo de storage utilizando File.Exists.");

            fileMock
                .Setup(p => p.ReadAllText(StoragePath + "\\john.doe-tasks.json"))
                .Returns("{json:true}")
                .Verifiable("Deve ser realizada a leitura do arquivo de storage.");

            var fileSystemMock = CreateFileSystemMock(pathMock.Object, directoryMock.Object, fileMock.Object);

            var warehouse = new Warehouse<Task>(fileSystemMock.Object, serializerMock.Object, settingsMock.Object);

            // Act
            var tasks = warehouse.Retrieve();

            // Assert
            Assert.Equal<int>(0, tasks.Count());

            settingsMock.VerifyAll();

            pathMock.VerifyAll();

            serializerMock.VerifyAll();

            directoryMock.VerifyAll();

            fileMock.VerifyAll();

            fileSystemMock.VerifyAll();
        }
    }
}