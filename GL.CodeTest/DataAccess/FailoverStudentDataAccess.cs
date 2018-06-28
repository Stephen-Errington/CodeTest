using GL.CodeTest.StudentServices;

namespace GL.CodeTest.DataAccess {
    public class FailoverStudentDataAccess : IStudentDataAccess {
        public StudentResponse LoadStudent(int studentId) {
            return GetStudentById(studentId);
        }

        public static StudentResponse GetStudentById(int id)
        {
            // retrieve student from database
            return new StudentResponse();
        }
    }
}