using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockDevOps.Models
{
    public class InviteViewModel
    {
        public List<InviteUserModel> Users { get; set; }
        public List<Project> Projects { get; set; }
    }
}
