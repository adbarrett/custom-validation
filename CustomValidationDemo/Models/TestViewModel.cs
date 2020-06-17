using CustomValidationDemo.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomValidationDemo.Models
{
	public class TestViewModel
	{
		public bool ThisCanBeAnything { get; set; }

		[IsTrue]
		public bool ThisMustBeTrue { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[PasswordStrength(Length = 8, LowerCaseLetters = 1, UpperCaseLetters = 1, Numbers = 1, SpecialCharacters = 1)]
		[DataType(DataType.Password)]
		public string StrongPassword { get; set; }
	}
}
