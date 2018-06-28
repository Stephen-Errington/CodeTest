using GL.CodeTest.StudentServices;

namespace GL.CodeTest.DataAccess {
    public class StudentDataAccess : IStudentDataAccess {
        public StudentResponse LoadStudent(int studentId)
        {
            // rettrieve student from 3rd party webservice
            return new StudentResponse();
        }
    }
}