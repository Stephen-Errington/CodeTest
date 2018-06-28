using GL.CodeTest.StudentServices;

namespace GL.CodeTest.DataAccess {
    public interface IStudentDataAccess {
        StudentResponse LoadStudent(int studentId);
    }
}