namespace GL.CodeTest.Failover {
    public class FailoverRepository : IFailoverRepository {
        public int CountFailedEntries(int minutes) {

            // Call To uspCountFailedEntriesInLastXMinutes (see SQL folder)
            return 4;
        }
    }
}