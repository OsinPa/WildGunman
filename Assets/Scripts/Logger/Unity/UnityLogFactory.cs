using System;

namespace WildGunman.Logger.Unity
{
    public class UnityLogFactory : ILogFactory
    {
        public ILog GetLogger(Type type)
        {
            return new UnityLog(type);
        }
    }
}