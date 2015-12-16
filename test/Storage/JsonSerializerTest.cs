using Stag.Storage;
using Stag.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Stag.Test.Storage
{
    public class JsonSerializerTest
    {
        private const string Id = "401805";

        private const string Title = "Novos Municipios";

        private const string DevBranch = "tarefa/401805-novos-municipios";

        private const string SerializedTask = @"{""Id"":""401805"",""Title"":""Novos Municipios"",""DevelopmentBranchName"":""tarefa/401805-novos-municipios""}";

        [Fact]
        public void ShouldSerialize()
        {
            // Arrange
            var task = new Task(Title, Id) { DevelopmentBranchName = DevBranch };
            var serializer = new JsonSerializer();

            // Act
            var serializedTask = serializer.Serialize(task);

            // Assert
            Assert.Equal<string>(SerializedTask, serializedTask);
        }

        [Fact]
        public void ShouldDeserialize()
        {
            // Arrange
            var task = new Task(Title, Id) { DevelopmentBranchName = DevBranch };
            var serializer = new JsonSerializer();

            // Act
            var deserializedTask = serializer.Deserialize<Task>(SerializedTask);

            // Assert
            Assert.Equal<string>(Id, deserializedTask.Id);
            Assert.Equal<string>(Title, deserializedTask.Title);
            Assert.Equal<string>(DevBranch, deserializedTask.DevelopmentBranchName);
        }
    }
}