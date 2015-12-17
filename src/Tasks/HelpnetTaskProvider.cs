using Stag.Configuration;
using Stag.Storage;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Stag.Tasks
{
    internal class HelpnetTaskProvider : ITaskProvider
    {
        private readonly ISettings _settings;
        private readonly IConnectionManager _connectionManager;

        public HelpnetTaskProvider()
            : this(new Settings(), new HelpnetConnectionManager())
        {
        }

        internal HelpnetTaskProvider(ISettings settings, IConnectionManager connectionManager)
        {
            _settings = settings;
            _connectionManager = connectionManager;
        }

        public IList<Task> GetTasks()
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                var query = "SELECT TarID, TarTitulo FROM Tarefa WHERE UsuIDResponsavel = (SELECT UsuID FROM Usuario WHERE UsuUsuario = @usuario)";
                cmd.CommandText = this.AddQueryParameters(cmd, query);

                using (var dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    var taskList = new List<Task>();
                    while (dataReader.Read())
                    {
                        var id = dataReader["TarId"].ToString();
                        var title = dataReader["TarTitulo"].ToString();

                        taskList.Add(new Task(title, id));
                    }

                    return taskList;
                }
            }
        }

        private string AddQueryParameters(IDbCommand cmd, string query)
        {
            cmd.Parameters.Add(_connectionManager.CreateParameter("usuario", _settings.Username.ToLower(CultureInfo.InvariantCulture)));

            if (!string.IsNullOrWhiteSpace(_settings.MiscTaskId))
            {
                query += " AND TarID <> @miscTaskId";

                var parameter = _connectionManager.CreateParameter("miscTaskId", _settings.MiscTaskId);
                cmd.Parameters.Add(parameter);
            }

            return query;
        }
    }
}