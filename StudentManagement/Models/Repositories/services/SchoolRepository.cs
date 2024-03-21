
namespace StudentManagement.Models.Repositories.services
{
    public class SchoolRepository : ISchoolRepository
    {
        readonly StudentContext context;
        public SchoolRepository(StudentContext context)
        {
            this.context = context;
        }

        public void Add(School s)
        {
            context.Schools.Add(s);
            context.SaveChanges();
        }

        public void Delete(School s)
        {
            context.Schools.Remove(s);
            context.SaveChanges();
        }

        public void Edit(int id,School updatedSchool)
        {
            School s = context.Schools.Find(id);
            s.SchoolName = updatedSchool.SchoolName;
            s.SchoolAdress = updatedSchool.SchoolAdress;
            s.Students = updatedSchool.Students;
            context.SaveChanges();
        }

        public IList<School> GetAll()
        {
            return context.Schools.OrderBy(s=>s.SchoolName).ToList();
        }

        public School GetById(int id)
        {
            return context.Schools.Find(id);
        }

        public double StudentAgeAverage(int schoolId)
        {
            if (StudentCount(schoolId) == 0)
                return 0;
            else
                return context.Students.Where(s => s.SchoolID ==schoolId).Average(e => e.Age);
        }

        public int StudentCount(int schoolId)
        {
            return context.Students.Where(s => s.SchoolID == schoolId).Count();
        }
    }
}
