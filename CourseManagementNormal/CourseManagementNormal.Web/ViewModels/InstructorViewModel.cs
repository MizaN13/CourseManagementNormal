using CourseManagementNormal.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementNormal.Web.ViewModels
{
    public class InstructorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Skill { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
