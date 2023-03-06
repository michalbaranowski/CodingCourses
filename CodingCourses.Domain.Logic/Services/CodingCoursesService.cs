using CodingCourses.DataAccess.Contracts.Contexts;
using CodingCourses.DataAccess.Contracts.Entities;
using CodingCourses.Domain.Contracts.Models;
using CodingCourses.Domain.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace CodingCourses.Domain.Logic.Services
{
    public class CodingCoursesService : ICodingCoursesService
    {
        private readonly CodingCoursesDbContext _context;

        public CodingCoursesService(CodingCoursesDbContext context)
        {
            _context = context;
        }

        public void Add(CourseModel course)
        {
            var entityToBeAdded = new Course()
            {
                Name = course.Name,
                Description = course.Description,
                Topics = course.Topics.Select(n => new Topic
                {
                    OrderNumber = n.OrderNumber,
                    Value = n.Value
                }).ToList()
            };

            _context.Courses.Add(entityToBeAdded);
            _context.SaveChanges();
        }

        public void Update(CourseModel course)
        {
            if (course.Id.HasValue == false)
            {
                return;
            }

            var entityToBeUpdated = _context.Courses.Include(x => x.Topics).FirstOrDefault(x => x.Id == course.Id);

            if (entityToBeUpdated == null)
            { 
                return;
            }

            _context.Topics.RemoveRange(entityToBeUpdated.Topics);
            entityToBeUpdated.Name = course.Name;
            entityToBeUpdated.Description = course.Description;
            entityToBeUpdated.Topics = course.Topics.Select(n => new Topic
            {
                OrderNumber = n.OrderNumber,
                Value = n.Value
            }).ToList();

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
