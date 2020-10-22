using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementNormal.Web.Data.Entities
{
    public class StudentCourse
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
