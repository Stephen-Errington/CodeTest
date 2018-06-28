namespace GL.CodeTest.Config {
    public interface IConfigReader {
        T Read<T>(string searchKey, T defaultValue);
    }
}