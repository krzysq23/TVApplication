using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Helpers
{
    public class ProviderNameHelper
    {
        public static IEnumerable<string> providerNames = new List<string> { "trakt", "tmdb" };
    }
}