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

            var courseMockSet = new Mock<DbSet<Course>>();
            courseMockSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.Provider);
            courseMockSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            courseMockSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            courseMockSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(() => courses.GetEnumerator());

            var topicMockSet = new Mock<DbSet<Topic>>();
            var topicsQueryable = courses.SelectMany(n => n.Topics).ToList().AsQueryable();
            topicMockSet.As<IQueryable<Topic>>().Setup(m => m.Provider).Returns(topicsQueryable.Provider);
            topicMockSet.As<IQueryable<Topic>>().Setup(m => m.Expression).Returns(topicsQueryable.Expression);
            topicMockSet.As<IQueryable<Topic>>().Setup(m => m.ElementType).Returns(topicsQueryable.ElementType);
            topicMockSet.As<IQueryable<Topic>>().Setup(m => m.GetEnumerator()).Returns(() => topicsQueryable.GetEnumerator());

            var mockContext = new Mock<CodingCoursesDbContext>();
            mockContext.Setup(x => x.Courses).Returns(courseMockSet.Object);
            mockContext.Setup(x => x.Topics).Returns(topicMockSet.Object);

            return mockContext;
        }
    }
}
