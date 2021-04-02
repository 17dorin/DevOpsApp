using System;
using System.Collections.Generic;

#nullable disable

namespace MockDevOps.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectUsers = new HashSet<ProjectUser>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }

        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
