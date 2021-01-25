using System;

namespace Autodealer.Plugins.Extensions
{
    /// <summary>
    /// Расширения проводника сервисов
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Возращает сервис определённого типа T
        /// </summary>
        /// <typeparam name="T">Тип запрашиваемого сервиса</typeparam>
        /// <param name="serviceProvider">Проводник</param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
