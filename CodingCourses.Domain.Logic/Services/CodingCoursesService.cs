using CodingCourses.DataAccess.Contracts.Contexts;
using CodingCourses.DataAccess.Contracts.Entities;
using CodingCourses.Domain.Contracts.Models;
using CodingCourses.Domain.Contracts.Services;

namespace CodingCourses.Domain.Logic.Services
{
    public class CodingCoursesService : ICodingCoursesService
    {
        private readonly CodingCoursesDbContext _context;

        public CodingCoursesService(CodingCoursesDbContext context)
        {
            _context = context;
        }

        public void Add(CourseModel courseToBeAdded)
        {
            var entity = new Course()
            {
                Name = courseToBeAdded.Name,
                Description = courseToBeAdded.Description,
                Topics = courseToBeAdded.Topics.Select(n => new Topic()
                {
                    OrderNumber = n.OrderNumber,
                    Value = n.Value,
                }).ToList()
            };

            _context.Courses.Add(entity);
            _context.SaveChanges();
        }

        public IList<CourseModel> GetAll(int skip = 0, int take = 500)
        {
            return _context.Courses.Skip(skip).Take(take).Select(n => new CourseModel(n.Id, n.Name, n.Description, n.Topics.Select(t => new TopicModel(t.Id, t.Value, t.OrderNumber)).ToList())).ToList();
        }

        public void Remove(int courseId)
        {
            var objectToBeRemoved = _context.Courses.FirstOrDefault(x => x.Id == courseId);
            if (objectToBeRemoved != null)
            {
                _context.Courses.Remove(objectToBeRemoved);
                _context.SaveChanges();
            }
        }
    }
}
