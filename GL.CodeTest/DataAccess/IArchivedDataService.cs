using GL.CodeTest.StudentServices;

namespace GL.CodeTest.DataAccess {
    public interface IArchivedDataService {
        Student GetArchivedStudent(int studentId);
    }
}