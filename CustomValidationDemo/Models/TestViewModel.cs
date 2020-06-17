using CustomValidationDemo.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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



		[Required]
		public string Question1 { get; set; }

		[RequiredIfMatch(nameof(Question1), PropertyValues = new object[] { "Yes" })]
		public string RequiredIfQuestion1IsYes { get; set; }

		[Required]
		public string Question2 { get; set; }

		[RequiredIfMatch(nameof(Question2), PropertyValues = new object[] { "Yes" })]
		public string RequiredIfQuestion2IsYes { get; set; }

		public IEnumerable<SelectListItem> QuestionAnswers => new List<SelectListItem> { new SelectListItem("No", "No"), new SelectListItem("Yes", "Yes") };
	}
}
