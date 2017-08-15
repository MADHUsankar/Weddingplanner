using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using weddingplanner.Models;
 
namespace weddingplanner.Models
{
    public class Userrecord : BaseEntity
    {
        [Key]
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string EmailAddress {get; set;}
        public string Password {get; set;}
        // public DateTime Createdat {get;set;}
        public List<Invitationsinfo> Invitationsinfo {get; set;}

        public Userrecord(){
            Invitationsinfo= new List<Invitationsinfo>();
        }

    }
}