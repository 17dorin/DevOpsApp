using System;
using System.Collections.Generic;

#nullable disable

namespace MockDevOps.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public string CreatingUser { get; set; }
        public string AssignedUser { get; set; }
        public int? ProjectId { get; set; }
        public bool? Completed { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }

        public virtual AspNetUser AssignedUserNavigation { get; set; }
        public virtual AspNetUser CreatingUserNavigation { get; set; }
        public virtual Project Project { get; set; }
    }
}
