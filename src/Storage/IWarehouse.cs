using System.Linq;

namespace Stag.Storage
{
    /// <summary>
    /// Representa uma estrutura de armazenamento de dados do tipo T.
    /// </summary>
    /// <typeparam name="T">Tipo de dado a ser armazenado.</typeparam>
    internal interface IWarehouse<T>
    {
        /// <summary>
        /// Obtém todos os registros do tipo T armazenados.
        /// </summary>
        /// <returns>Um <see cref="IQueryable"/> representando os registros de T.</returns>
        IQueryable<T> Retrieve();

        /// <summary>
        /// Armazena objeto do tipo T.
        /// </summary>
        /// <param name="item">Objeto a ser armazenado.</param>
        void Store(T item);
    }
}