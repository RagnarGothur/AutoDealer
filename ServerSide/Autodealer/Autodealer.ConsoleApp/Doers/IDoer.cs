using System.Threading.Tasks;

namespace Autodealer.ConsoleApp.Doers
{
    /// <summary>
    /// Абстрактный "Исполнитель" задач
    /// </summary>
    public interface IDoer
    {
        /// <summary>
        /// Выполняет задачу асинхронно
        /// </summary>
        /// <returns>task</returns>
        Task DoAsync();
    }
}
