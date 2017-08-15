using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using weddingplanner.Models;
 
namespace weddingplanner.Models
{
    public class weddingrecords : BaseEntity
    {
        [Key]
        public int WeddingId {get; set;}
       
        [Required]
        public string SpouseOne {get;set;}
       [Required]
        public string SpouseTwo {get;set;}
        [Required]
        public DateTime WeddingDate {get; set;}
        
        [Required]
        public string Weddingaddress {get; set;}
        public int UserId {get; set;}
        public Userrecord Userrecord {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get; set;}
        public List<Invitationsinfo> Invitationsinfo {get;set;}

        public weddingrecords(){
            Invitationsinfo = new List<Invitationsinfo>();
            CreatedAt= DateTime.Now;
            UpdatedAt=DateTime.Now;
        }
    }
}