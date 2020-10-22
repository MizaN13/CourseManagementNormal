using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementNormal.Web.Data.Entities
{
    public class Course : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Level { get; set; }
        public double Fee { get; set; }
        public string Description { get; set; }
        public string Prerequisite { get; set; }
        public string CourseHighlight { get; set; }
        public int Classes { get; set; }
        public int Lessons { get; set; }
        public double Duration { get; set; }
        public int BatchNo { get; set; }
        public DateTime ClassDays { get; set; }
        public DateTime Time { get; set; }
        public DateTime RegClose { get; set; }
        public DateTime ClassStart { get; set; }
        public Guid InstrutorId { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
