using CodingCourses.Domain.Contracts.Models;
using CodingCourses.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingCourses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICodingCoursesService _codingCoursesService;

        public CoursesController(ICodingCoursesService codingCoursesService)
        {
            _codingCoursesService = codingCoursesService;
        }

        [HttpGet]
        public IEnumerable<CourseModel> GetAll()
        {
            return _codingCoursesService.GetAll();
        }

        [HttpPost]
        public void AddCourse(CourseModel model) 
        {
            _codingCoursesService.Add(model);
        }

        [HttpPut]
        public void UpdateCourse(CourseModel model)
        {
            _codingCoursesService.Update(model);
        }

        [HttpDelete]
        public void DeleteCourse(int id)
        {
            _codingCoursesService.Remove(id);
        }
    }
}
