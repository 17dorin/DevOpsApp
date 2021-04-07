using System;
using System.Collections.Generic;

#nullable disable

namespace MockDevOps.Models
{
    public partial class ProjectUser
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string UserId { get; set; }
        public bool? GroupAdmin { get; set; }

        public virtual Project Project { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
