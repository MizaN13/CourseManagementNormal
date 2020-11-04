using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using CourseManagementNormal.Web.Data;
using CourseManagementNormal.Web.Data.Entities;
using CourseManagementNormal.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace CourseManagementNormal.Web.Controllers
{
    public class CourseController : Controller
    {
        public SchoolContext _context;
        private readonly CancellationToken queryCountd;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCourse()
        {
            CourseViewModel course = new CourseViewModel();
            return View(course);
        }

        public IActionResult EditCourse(Guid id)
        {
            CourseViewModel course = new CourseViewModel();
            return View("AddCourse", course);
        }
        public async Task<JsonResult> CreateOrUpdateCourse(CourseViewModel model)
        {
            try
            {
                Course course;

                bool isEdit = false;
                if (model.Id != Guid.Empty)
                {
                    isEdit = true;
                    course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == model.Id);
                    _context.SaveChanges();
                }
                else
                {
                    course = new Course();
                    //course = new Course { Name = model.Name };
                    //var coursenamematch = _context.Courses.SingleOrDefaultAsync(x => x.Name == model.Name);
                    //if (coursenamematch != null) throw new Exception("Course Name can't be Same !");

                }
                course.Name = model.Name;
                course.Type = model.Type;
                course.Target = model.Target;
                course.Level = model.Level;
                course.Fee = model.Fee;
                course.Description = model.Description;
                course.Prerequisite = model.Prerequisite;
                course.CourseHighlight = model.CourseHighlight;
                course.Classes = model.Classes;
                course.Lessons = model.Lessons;
                course.Duration = model.Duration;
                course.BatchNo = model.BatchNo;
                course.ClassDays = model.ClassDays == null ? null : model.ClassDays;
                course.Time = model.Time == null ? null : model.Time;
                course.RegClose = model.RegClose == null ? null : model.RegClose;
                course.ClassStart = model.ClassStart == null ? null : model.ClassStart;
                course.Picture = model.Picture;
                course.InstrutorId = model.InstrutorId;


                if (isEdit)
                {
                    await _context.Courses.SingleOrDefaultAsync(x => x.Id == course.Id);
                    _context.SaveChanges();
                }
                else
                {
                    course.Id = Guid.NewGuid();
                    await _context.Courses.AddAsync(course);
                }

                return Json(new { Success = true, Message = (isEdit == true) ? "Update Successfully" : "Save Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                
            }
        }

        public async Task<JsonResult> GridInitCourse(int page, int rows, string sidx, string sord)
        {

            return await CourseList(Guid.Empty, page, rows, sidx, sord);
        }

        public async Task<JsonResult> CourseList(Guid courseId, int page, int rows, string sidx, string sord)
        {
            try
            {
                List<CourseViewModel> courseList = new List<CourseViewModel>();

                //IQueryable<Course> query = new IQueryable<Course>();
                //IQueryable<Course> queryCount = new IQueryable<Course>();
                var query = _context.Courses.AsQueryable();
                var queryCount = _context.Courses.AsQueryable();

                if (courseId != Guid.Empty)
                {
                    query = query.Where(x => x.Id == courseId);
                }

                //var courses = await _context.Courses.AsQueryable<fil(query.OrderBy(x => x.Name), rows, page);
                var courses = await _context.Courses.FilterAsync(query.OrderBy(x => x.Name), rows, page);
                var count = await _context.Courses.CountAsync(queryCountd);
                //var count = await _context.Courses.CountAsync<Course>(IQueryable<Course>, Expressions<Func<Course, queryCount>>, queryCount);

                double pageCount = Math.Ceiling((count * 1.0) / (rows * 1.0));

                foreach (Course item in courses)
                {
                    //var cat = await _context.StudentCourses.GetAsync(item.Students);
                    CourseViewModel model = new CourseViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Type = item.Type,
                        Target = item.Target,
                        Level = item.Level,
                        Fee = item.Fee,
                        Description = item.Description,
                        Prerequisite = item.Prerequisite,
                        CourseHighlight = item.CourseHighlight,
                        Classes = item.Classes,
                        Lessons = item.Lessons,
                        Duration = item.Duration,
                        BatchNo = item.BatchNo,
                        ClassDays = item.ClassDays,
                        Time = item.Time,
                        RegClose = item.RegClose,
                        ClassStart = item.ClassStart,
                        Picture = item.Picture,
                        InstrutorId = item.InstrutorId,
                        //IsActive = item.IsActive
                    };
                    courseList.Add(model);
                }

                var jsonData = new
                {
                    total = pageCount,
                    page,
                    records = count,
                    rows = courseList.Select(x => new
                    {
                        id = x.Id,
                        cell = new[]
                        {
                            x.Id.ToString(),
                            x.Name,x.Type,
                            x.Target,
                            x.Fee.ToString(),
                            x.Description,
                            x.Prerequisite,
                            x.CourseHighlight,
                            x.Classes.ToString(),
                            x.Lessons.ToString(),
                            x.Duration.ToString(),
                            x.BatchNo.ToString(),
                            x.ClassDays?.ToString("dd-MMM-yyyy")??"N/A",
                            x.Time?.ToString("dd-MMM-yyyy")??"N/A",
                            x.RegClose?.ToString("dd-MMM-yyyy")??"N/A",
                            x.ClassStart?.ToString("dd-MMM-yyyy")??"N/A",
                            x.Picture,
                            x.InstrutorId.ToString(),
                            //x.IsActive ? "YES":"NO",
                            //"<a href='#' id='btnEdit' courseId='" + x.Id.ToString() + "'><img src='/Assets/Images/edit.png' title='Edit' /></a>"
                            "<a href='/Course/EditAsset?id=" + x.Id + "'><img src='/Assets/Images/edit.png' title='Edit' /></a>"
                        }
                    })
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                
            }
        }
    }
}
