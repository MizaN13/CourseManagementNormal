using CourseManagementNormal.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementNormal.Web.ViewModels
{
    public class EnrollmentViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
