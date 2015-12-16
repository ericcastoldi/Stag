using Stag.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stag.Test.Storage
{
    public class HelpnetConnectionManagerTest
    {
        [Fact]
        public void ShouldCreateConnection()
        {
            // Arrange
            var helpnetConnectionManager = new HelpnetConnectionManager();

            // Act
            var connection = helpnetConnectionManager.CreateConnection();

            // Assert
            Assert.NotNull(connection);

            Assert.IsType<SqlConnection>(connection);

            Assert.Equal<ConnectionState>(ConnectionState.Closed, connection.State);
        }

        [Fact]
        public void ShouldCreateParameter()
        {
            // Arrange
            var value = "value";
            var parameterName = "parameterName";
            var helpnetConnectionManager = new HelpnetConnectionManager();

            // Act
            var parameter = helpnetConnectionManager.CreateParameter(parameterName, value);

            // Assert
            Assert.NotNull(parameter);

            Assert.IsType<SqlParameter>(parameter);

            Assert.Equal<string>(parameterName, parameter.ParameterName);

            Assert.Equal(value, parameter.Value);
        }
    }
}