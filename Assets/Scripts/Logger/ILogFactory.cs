using System;

namespace WildGunman.Logger
{
    public interface ILogFactory
    {
        public ILog GetLogger(Type type);
    }
}