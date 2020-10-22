using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementNormal.Web.Data.Entities
{
    public class Instructor : EntityBase<Guid>
    {
        public string Name {get; set;}
        public string Designation { get; set; }
        public string Skill { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}
