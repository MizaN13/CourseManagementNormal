using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementNormal.Web.Data.Entities
{
    public class Enrollment: EntityBase<Guid>
    {

        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }

    public enum Grade
    {
        A, B, C, D, F
    }
}
