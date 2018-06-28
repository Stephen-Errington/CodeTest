using GL.CodeTest.Config;

namespace GL.CodeTest.Failover {
    public class FailoverService : IFailoverService {
        private readonly IFailoverRepository failoverRepository;
        private readonly IConfigManager config;

        public FailoverService(IFailoverRepository failoverRepository, IConfigManager config) {
            this.failoverRepository = failoverRepository;
            this.config = config;
        }

        public bool IsFailoverMode() {
            return this.config.IsFailoverModeEnabled && this.failoverRepository.CountFailedEntries(config.FailoverMinutes.Value) > config.FailedRequestLimit.Value;
        }
    }
}
