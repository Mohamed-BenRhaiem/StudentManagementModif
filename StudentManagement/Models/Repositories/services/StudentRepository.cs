
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models.Repositories.services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext context;

        public StudentRepository(StudentContext context)
        {
            this.context = context;
        }
        public void Add(Student s)
        {
            context.Students.Add(s);
            context.SaveChanges();
        }

        public void Delete(Student s)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id,Student s)
        {
            Student s1 = context.Students.Find(id);
            s1.StudentName = s.StudentName;
            s1.Age = s.Age;
            s1.BirthDate = s.BirthDate;
            s1.SchoolID = s.SchoolID;
            context.SaveChanges();

        }

        public IList<Student> FindByName(string name)
        {
            return context.Students.Where(s =>
            s.StudentName.Contains(name)).Include(std =>
            std.School).ToList();
            }

        public IList<Student> GetAll()
        {
            return context.Students.OrderBy(x => x.StudentName).Include(x => x.School).ToList();
        }

        public Student GetById(int id)
        {
            return context.Students.Find(id);
        }

        public IList<Student> GetStudentsBySchoolID(int? schoolId)
        {

            return context.Students.Where(s =>
            s.SchoolID.Equals(schoolId))
            .OrderBy(s => s.StudentName)
            .Include(std => std.School).ToList();
        }
    }
}
