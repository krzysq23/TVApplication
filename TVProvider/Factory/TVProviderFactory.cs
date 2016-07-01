using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Provider;

namespace TVProvider.Factory
{
    //TODO: refactor to CastleWindsor
    public class TVProviderFactory : AbstractFactory
    {
        public override AbstractProvider GetProvider(string type)
        {
            AbstractProvider data = null;
            switch (type)
            {
                case "trakt":
                    data = new TraktProvider();
                    break;
                case "tmdb":
                    data = new TmdbProvider();
                    break;
            };
            return data;
        }
    }
}
