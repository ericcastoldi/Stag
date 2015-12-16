using Moq;
using Stag.Configuration;
using Stag.Storage;
using Stag.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stag.Test.Tasks
{
    public class HelpnetTaskProviderTest
    {
        [Fact]
        public void ShouldGetTasks()
        {
            // Arrange
            var query = string.Empty;

            var settingsMock = new Mock<ISettings>();
            settingsMock
                .SetupGet(p => p.Username)
                .Returns("john.doe")
                .Verifiable("O Username deve ser utilizado para buscar somente as tarefas do usuário.");

            settingsMock
                .SetupGet(p => p.MiscTaskId)
                .Returns(string.Empty)
                .Verifiable("O MiscTaskId deve ser utilizado para filtrar as tarefas de apontamentos diversos (elas não devem ser obtidas).");

            var parametersMock = new Mock<IDataParameterCollection>();
            parametersMock
                .Setup(p => p.Add(It.Is<IDataParameter>(d => d.ParameterName == "usuario" && d.Value.ToString() == "john.doe")))
                .Verifiable("O parâmetro que filtra as tarefas pelo usuário deve ser adicionado aos parâmetros do comando.");

            var dataReaderMock = new Mock<IDataReader>();
            dataReaderMock
                .SetupSequence(p => p.Read())
                .Returns(true)
                .Returns(false);

            dataReaderMock
                .SetupGet(p => p["TarId"])
                .Returns("321654")
                .Verifiable("O identificador da tarefa deve ser lido do dataReader através da chave TarId.");

            dataReaderMock
                .SetupGet(p => p["TarTitulo"])
                .Returns("Tarefa com descrição")
                .Verifiable("O título da tarefa deve ser lido do dataReader através da chave TarTitulo.");

            var commandMock = new Mock<IDbCommand>() { CallBase = true };
            commandMock
                .Setup(p => p.ExecuteReader(CommandBehavior.CloseConnection))
                .Returns(dataReaderMock.Object)
                .Verifiable("O IDbCommand deve ser executado para buscar as tarefas.");

            commandMock
                .SetupGet(p => p.Parameters)
                .Returns(parametersMock.Object)
                .Verifiable("A lista de parâmetros do comando deve ser acessada para adicionar os parâmetros do SELECT.");

            commandMock
                .SetupSet(p => p.CommandText = It.IsAny<string>())
                .Callback<string>(value => query = value)
                .Verifiable("A query a ser executada deve ser atribuída à propriedade CommandText do IDbCommand.");

            var connectionMock = new Mock<IDbConnection>();
            connectionMock
                .Setup(p => p.Open())
                .Verifiable("O HelpnetTaskProvider deve abrir a conexão com o banco de dados.");

            connectionMock
                .Setup(p => p.CreateCommand())
                .Returns(commandMock.Object)
                .Verifiable("O HelpnetTaskProvider deve criar um comando para buscar as tarefas do banco de dados.");

            var parameterMock = new Mock<IDataParameter>();
            parameterMock
                .SetupGet(p => p.ParameterName)
                .Returns("usuario");

            parameterMock
                .SetupGet(p => p.Value)
                .Returns("john.doe");

            var connectionManagerMock = new Mock<IConnectionManager>();
            connectionManagerMock
                .Setup(p => p.CreateConnection())
                .Returns(connectionMock.Object)
                .Verifiable("O HelpnetTaskProvider deve criar uma conexão com o banco de dados.");

            connectionManagerMock
                .Setup(p => p.CreateParameter("usuario", "john.doe"))
                .Returns(parameterMock.Object)
                .Verifiable("Deve ser criado o parâmetro que filtra as tarefas pelo usuário");

            var provider = new HelpnetTaskProvider(settingsMock.Object, connectionManagerMock.Object);

            // Act
            var tasks = provider.GetTasks();

            // Assert
            Assert.Equal<int>(1, tasks.Count);

            Assert.Equal<string>("321654", tasks[0].Id);

            Assert.Equal<string>("Tarefa com descrição", tasks[0].Title);

            Assert.Equal("SELECT TarID, TarTitulo FROM Tarefa WHERE UsuIDResponsavel = (SELECT UsuID FROM Usuario WHERE UsuUsuario = @usuario)",
                query);

            settingsMock.VerifyAll();

            parametersMock.VerifyAll();

            dataReaderMock.VerifyAll();

            commandMock.VerifyAll();

            connectionMock.VerifyAll();

            parameterMock.VerifyAll();

            connectionManagerMock.VerifyAll();
        }

        [Fact]
        public void ShouldNotGetMiscTask()
        {
            // Arrange
            var query = string.Empty;

            var settingsMock = new Mock<ISettings>();
            settingsMock
                .SetupGet(p => p.Username)
                .Returns("john.doe")
                .Verifiable("O Username deve ser utilizado para buscar somente as tarefas do usuário.");

            settingsMock
                .SetupGet(p => p.MiscTaskId)
                .Returns("999999")
                .Verifiable("O MiscTaskId deve ser utilizado para filtrar as tarefas de apontamentos diversos (elas não devem ser obtidas).");

            var parametersMock = new Mock<IDataParameterCollection>();
            parametersMock
                .Setup(p => p.Add(It.Is<IDataParameter>(d => d.ParameterName == "usuario" && d.Value.ToString() == "john.doe")))
                .Verifiable("O parâmetro que filtra as tarefas pelo usuário deve ser adicionado aos parâmetros do comando.");

            parametersMock
                .Setup(p => p.Add(It.Is<IDataParameter>(d => d.ParameterName == "miscTaskId" && d.Value.ToString() == "999999")))
                .Verifiable("O parâmetro que filtra a tarefa de atividades diversas do usuário deve ser adicionado aos parâmetros do comando.");

            var dataReaderMock = new Mock<IDataReader>();
            dataReaderMock
                .SetupSequence(p => p.Read())
                .Returns(true)
                .Returns(false);

            dataReaderMock
                .SetupGet(p => p["TarId"])
                .Returns("321654")
                .Verifiable("O identificador da tarefa deve ser lido do dataReader através da chave TarId.");

            dataReaderMock
                .SetupGet(p => p["TarTitulo"])
                .Returns("Tarefa com descrição")
                .Verifiable("O título da tarefa deve ser lido do dataReader através da chave TarTitulo.");

            var commandMock = new Mock<IDbCommand>() { CallBase = true };
            commandMock
                .Setup(p => p.ExecuteReader(CommandBehavior.CloseConnection))
                .Returns(dataReaderMock.Object)
                .Verifiable("O IDbCommand deve ser executado para buscar as tarefas.");

            commandMock
                .SetupGet(p => p.Parameters)
                .Returns(parametersMock.Object)
                .Verifiable("A lista de parâmetros do comando deve ser acessada para adicionar os parâmetros do SELECT.");

            commandMock
                .SetupSet(p => p.CommandText = It.IsAny<string>())
                .Callback<string>(value => query = value)
                .Verifiable("A query a ser executada deve ser atribuída à propriedade CommandText do IDbCommand.");

            var connectionMock = new Mock<IDbConnection>();
            connectionMock
                .Setup(p => p.Open())
                .Verifiable("O HelpnetTaskProvider deve abrir a conexão com o banco de dados.");

            connectionMock
                .Setup(p => p.CreateCommand())
                .Returns(commandMock.Object)
                .Verifiable("O HelpnetTaskProvider deve criar um comando para buscar as tarefas do banco de dados.");

            var parameterMock = new Mock<IDataParameter>();
            parameterMock
                .SetupGet(p => p.ParameterName)
                .Returns("usuario");

            parameterMock
                .SetupGet(p => p.Value)
                .Returns("john.doe");

            var miscTaskParameterMock = new Mock<IDataParameter>();
            miscTaskParameterMock
                .SetupGet(p => p.ParameterName)
                .Returns("miscTaskId");

            miscTaskParameterMock
                .SetupGet(p => p.Value)
                .Returns("999999");

            var connectionManagerMock = new Mock<IConnectionManager>();
            connectionManagerMock
                .Setup(p => p.CreateConnection())
                .Returns(connectionMock.Object)
                .Verifiable("O HelpnetTaskProvider deve criar uma conexão com o banco de dados.");

            connectionManagerMock
                .Setup(p => p.CreateParameter("usuario", "john.doe"))
                .Returns(parameterMock.Object)
                .Verifiable("Deve ser criado o parâmetro que filtra as tarefas pelo usuário");

            connectionManagerMock
                .Setup(p => p.CreateParameter("miscTaskId", "999999"))
                .Returns(miscTaskParameterMock.Object)
                .Verifiable("Deve ser criado o parâmetro que filtra a tarefa de atividades diversas do usuário.");

            var provider = new HelpnetTaskProvider(settingsMock.Object, connectionManagerMock.Object);

            // Act
            var tasks = provider.GetTasks();

            // Assert
            Assert.Equal<int>(1, tasks.Count);

            Assert.Equal<string>("321654", tasks[0].Id);

            Assert.Equal<string>("Tarefa com descrição", tasks[0].Title);

            Assert.Equal("SELECT TarID, TarTitulo FROM Tarefa WHERE UsuIDResponsavel = (SELECT UsuID FROM Usuario WHERE UsuUsuario = @usuario) AND TarID <> @miscTaskId",
                query);

            settingsMock.VerifyAll();

            parametersMock.VerifyAll();

            dataReaderMock.VerifyAll();

            commandMock.VerifyAll();

            connectionMock.VerifyAll();

            parameterMock.VerifyAll();

            miscTaskParameterMock.VerifyAll();

            connectionManagerMock.VerifyAll();
        }
    }
}