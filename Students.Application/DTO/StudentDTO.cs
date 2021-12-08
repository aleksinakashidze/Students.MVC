using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Students.Application.DTO
{
    public class StudentDTO
    {
        public int StudentID { get; set; }

        [DisplayName("სახელი")]
        [Required(ErrorMessage = "გთხოვთ შეავსოთ სახელის ველი*")]
        public string FirstName { get; set; }
        [DisplayName("გვარი")]
        [Required(ErrorMessage = "გთხოვთ შეავსოთ გვარის ველი*")]
        public string LastName { get; set; }
        [DisplayName("პირადი ნომერი")]
        [Required(ErrorMessage = "გთხოვთ შეავსოთ პირადი ნომრის ველი*")]
        public string PersonalNumber { get; set; }
        [DisplayName("დაბადების თარიღი")]
        [Required(ErrorMessage = "გთხოვთ შეავსოთ დაბადების თარიღი*")]
        [MinimumAge(16)]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("სქესი")]
        [Required(ErrorMessage = "გთხოვთ აირჩიოთ სქესი*")]
        public Gender Gender { get; set; }

    }

    public enum Gender
    {
        Women,
        Man,
    }
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }
            return false;
        }
    }
}
