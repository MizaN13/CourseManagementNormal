using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementNormal.Web.Data.Entities
{
    public class Student : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }
        public string InstituteName { get; set; }
        public string DegreeTitle { get; set; }
        public int PassingYear { get; set; }
        public double Result { get; set; }
        public virtual ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
    }
}
