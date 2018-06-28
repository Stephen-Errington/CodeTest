namespace GL.CodeTest.Config {
    public interface IConfigManager {
        int? FailedRequestLimit { get; }
        int? FailoverMinutes { get; }
        bool IsFailoverModeEnabled { get; }
    }
}