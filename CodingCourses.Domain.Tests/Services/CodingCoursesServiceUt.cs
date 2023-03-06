using CodingCourses.DataAccess.Contracts.Entities;
using CodingCourses.Domain.Contracts.Models;
using CodingCourses.Domain.Contracts.Services;
using CodingCourses.Domain.Logic.Services;
using CodingCourses.Domain.Tests.Base;
using Moq;
using NUnit.Framework;

namespace CodingCourses.Domain.Tests.Services
{
    [TestFixture]
    public class CodingCoursesServiceUt : CodingCoursesUnitTestBase
    {
        private ICodingCoursesService? _codingCoursesService;

        [Test]
        public void GetAll_ReturnsAllEntities()
        {
            //arrange
            var dbContext = GetDbContextMock().Object;
            _codingCoursesService = new CodingCoursesService(dbContext);

            //act
            var results = _codingCoursesService.GetAll();

            //assert
            Assert.That(results.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAll_SkipFirst_ReturnsOnlyOneEntity()
        {
            //arrange
            var dbContext = GetDbContextMock().Object;
            _codingCoursesService = new CodingCoursesService(dbContext);

            //act
            var results = _codingCoursesService.GetAll(skip: 1);

            //assert
            Assert.That(results.Count, Is.EqualTo(1));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void GetAll_Take_ReturnsNumberOfTakenEntities(int take)
        {
            //arrange
            var dbContext = GetDbContextMock().Object;
            _codingCoursesService = new CodingCoursesService(dbContext);

            //act
            var results = _codingCoursesService.GetAll(take: take);

            //assert
            Assert.That(results.Count, Is.EqualTo(take));
        }

        [Test]
        public void Add_CodingGiant_ReceiveAddMethodAndSaveChanges()
        {
            //arrange
            var dbContextMock = GetDbContextMock();
            _codingCoursesService = new CodingCoursesService(dbContextMock.Object);

            //act
            var codingGiantCourse = new CourseModel(
                "Coding giant course",
                "Nice course",
                new List<TopicModel>()
                {
                    new TopicModel("Topic 1", 1)
                });

            _codingCoursesService.Add(codingGiantCourse);

            //assert
            dbContextMock.Verify(x => x.Courses.Add(It.IsAny<Course>()), Times.Once);
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Remove_CodingGiant_ReceiveRemoveMethodAndSaveChanges()
        {
            //arrange
            var dbContextMock = GetDbContextMock();
            _codingCoursesService = new CodingCoursesService(dbContextMock.Object);

            var firstExistingCourse = _codingCoursesService.GetAll().First();

            //act
            if (firstExistingCourse.Id.HasValue)
            {
                _codingCoursesService.Remove(firstExistingCourse.Id.Value);
            }

            //assert
            dbContextMock.Verify(x => x.Courses.Remove(It.IsAny<Course>()), Times.Once);
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
