namespace GL.CodeTest.DataAccess {
    public interface IFactoryStudentDataAccess {
        IStudentDataAccess GetStudentDataAccess(bool isFailoverMode);
    }
}