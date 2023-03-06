using CodingCourses.DataAccess.Contracts.Contexts;
using CodingCourses.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CodingCourses.Domain.Tests.Base
{
    public abstract class CodingCoursesUnitTestBase
    {
        public Mock<CodingCoursesDbContext> GetDbContextMock()
        {
            var courses = new List<Course>
            {
                new Course {
                    Id = 1,
                    Name = "Course 1",
                    Description = "Wow, it's my first course!",
                    Topics = new List<Topic>() {
                        new Topic() {
                            Id = 1,
                            OrderNumber = 1,
                            Value = "My first topic!",
                            CourseId = 1
                        },
                        new Topic() {
                            Id = 2,
                            OrderNumber = 2,
                            Value = "My second topic!",
                            CourseId = 1
                        }
                    }
                },
                new Course {
                    Id = 2,
                    Name = "Second course",
                    Description = "Professional course",
                    Topics = new List<Topic>() {
                        new Topic() {
                            Id = 3,
                            OrderNumber = 1,
                            Value = "Nice topic...",
                            CourseId = 2
                        },
                        new Topic() {
                            Id = 4,
                            OrderNumber = 2,
                            Value = "Another nice topic...",
                            CourseId = 2
                        }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Course>>();
            mockSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.Provider);
            mockSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            mockSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            mockSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(() => courses.GetEnumerator());

            var mockContext = new Mock<CodingCoursesDbContext>();
            mockContext.Setup(x => x.Courses).Returns(mockSet.Object);

            return mockContext;
        }
    }
}
