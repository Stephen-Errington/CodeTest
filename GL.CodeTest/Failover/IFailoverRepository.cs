namespace GL.CodeTest.Failover {
    public interface IFailoverRepository {
        int CountFailedEntries(int minutes);
    }
}