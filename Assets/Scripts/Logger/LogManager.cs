using System;
using WildGunman.Logger.Unity;

namespace WildGunman.Logger
{
    public static class LogManager
    {
        private static readonly ILogFactory DefaultFactory = new UnityLogFactory();

        public static ILog GetLogger(Type type)
        {
            return DefaultFactory.GetLogger(type);
        }
    }
}