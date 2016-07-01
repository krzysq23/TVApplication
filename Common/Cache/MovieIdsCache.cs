using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public static class MovieIdsCache
    {
        public static ConcurrentDictionary<string, object> MoviesIds = new ConcurrentDictionary<string, object>();
    }
}
