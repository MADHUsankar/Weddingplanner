using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using weddingplanner.Models;
 
namespace weddingplanner.Models
{
    public class Invitationsinfo : BaseEntity
    {
        [Key]
                public int InvitationId {get; set;}
        public int UserId {get; set;}
        public int WeddingId {get; set;}
        public Userrecord Userrecord { get; set; }
        public weddingrecords weddingrecords { get; set; }
    }
}