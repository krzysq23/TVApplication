using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Provider;

namespace TVProvider.Factory
{
    public abstract class AbstractFactory
    {
        public abstract AbstractProvider GetProvider(string type);
    }
}
