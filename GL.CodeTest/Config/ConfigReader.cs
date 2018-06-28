using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace GL.CodeTest.Config {
    public class ConfigReader : IConfigReader {
        public T Read<T>(string searchKey, T defaultValue) {
            if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == searchKey)) {
                try {       // see if it can be converted
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null) {
                        defaultValue = (T)converter.ConvertFromString(ConfigurationManager.AppSettings.GetValues(searchKey).First());
                    }
                } catch { } // nothing to do, just return default
            }
            return defaultValue;
        }
    }
}
