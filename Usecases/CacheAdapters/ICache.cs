using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.Search.Usecases.Cache
{
    public interface ICache
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan time);
    }
}
