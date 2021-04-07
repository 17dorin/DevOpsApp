using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockDevOps.Models
{
    public class ProjectViewModel
    {
        public List<Project> UserProjects { get; set; }
        public List<int?> UserAdminGroups { get; set; }
    }
}
