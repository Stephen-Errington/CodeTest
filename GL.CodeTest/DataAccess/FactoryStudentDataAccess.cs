namespace GL.CodeTest.DataAccess {
    public class FactoryStudentDataAccess : IFactoryStudentDataAccess {

        public IStudentDataAccess GetStudentDataAccess(bool isFailoverMode) {
            if (isFailoverMode)
                return new FailoverStudentDataAccess();

            return new StudentDataAccess();
        }
    }
}
