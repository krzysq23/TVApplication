using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Common.Settings
{
    public interface IAppSettings
    {
        string GetTractAccessToken();
    }

    public class WebConfigSettings : IAppSettings
    {
        public string GetTractAccessToken()
        {
            Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (AppSettingsSection)webConfig.GetSection("appSettings");
            var trackAccessTokenSetting = section.Settings["traktAccessToken"];
            return trackAccessTokenSetting.Value;
        }
    }
}
