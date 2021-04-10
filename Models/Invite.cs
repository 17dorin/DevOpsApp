using System;
using System.Collections.Generic;

#nullable disable

namespace MockDevOps.Models
{
    public partial class Invite
    {
        public int Id { get; set; }
        public string Receiver { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }

        public virtual Project Project { get; set; }
        public virtual AspNetUser ReceiverNavigation { get; set; }
    }
}
