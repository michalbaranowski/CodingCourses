using CodingCourses.Domain.Contracts.Models;

namespace CodingCourses.Domain.Contracts.Services
{
    public interface ICodingCoursesService
    {
        void Add(CourseModel course);

        void Update(CourseModel course);

        IList<CourseModel> GetAll(int skip = 0, int take = 500);

        void Remove(int courseId);
    }
}
