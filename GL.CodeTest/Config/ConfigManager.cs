namespace GL.CodeTest.Config {
    public class ConfigManager : IConfigManager {
        private readonly IConfigReader configReader;

        public ConfigManager(IConfigReader configReader) {
            this.configReader = configReader;
        }

        public bool IsFailoverModeEnabled
        {
            get
            {
                var isEnabled = configReader.Read<bool>(Constraints.FAILOVER_MODE_ENABLED, false);

                if (!FailedRequestLimit.HasValue || !FailoverMinutes.HasValue)
                    return false;

                return isEnabled;
            }
        }

        public int? FailedRequestLimit
        {
            get
            {
                return configReader.Read<int?>(Constraints.FAILED_REQUEST_LIMIT, null);
            }
        }

        public int? FailoverMinutes
        {
            get
            {
                return configReader.Read<int?>(Constraints.FAILOVER_MINUTES, null);
            }
        }


    }
}