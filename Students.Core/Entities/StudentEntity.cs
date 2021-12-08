using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Core.Entities
{
    public class StudentEntity
    {
        [Key]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Woman = 0,
        Man = 1
    }
}
