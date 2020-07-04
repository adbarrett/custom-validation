using CustomValidationDemo.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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



		[DateRangeFromToday(DatePart.Days, 0, StepType.AfterToday, true)]
		public DateTime? MustBeTodayOrInThePast { get; set; }

		[DateRangeFromToday(DatePart.Days, -1, StepType.BeforeToday, true)]
		public DateTime? MustBeFutureDate { get; set; }

		[DateRangeFromToday(DatePart.Days, 7, 7, true)]
		public DateTime? MustBeWithinSevenDaysBeforeOrAfterToday { get; set; }

		[DateRangeFromToday(DatePart.Days, 0, 0, true)]
		public DateTime? MustBeToday { get; set; }
	}
}
