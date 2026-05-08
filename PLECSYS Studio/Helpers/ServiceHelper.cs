namespace PLECSYS_Studio.Helpers
{
    public static class ServiceHelper
    {
        public static T GetService<T>() where T : class
        {
            var service = IPlatformApplication.Current?.Services.GetService<T>();
            if (service == null)
                throw new InvalidOperationException($"No se pudo obtener el servicio: {typeof(T).Name}");
            return service;
        }
    }
}