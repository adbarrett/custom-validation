using CustomValidationDemo.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CustomValidationDemo.Models
{
	public class TestViewModel
	{
		[Display(Name = "This can be true or false")]
		public bool ThisCanBeAnything { get; set; }

		[IsTrue]
		[Display(Name = "This must be true")]
		public bool ThisMustBeTrue { get; set; }
	}
}
