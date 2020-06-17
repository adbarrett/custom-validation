namespace CustomValidationDemo.Attributes
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	[AttributeUsage(AttributeTargets.Property)]
	public class PasswordStrengthAttribute : ValidationAttribute
	{
		public int Length { get; set; }
		public int LowerCaseLetters { get; set; }
		public int UpperCaseLetters { get; set; }
		public int Numbers { get; set; }
		public int SpecialCharacters { get; set; }
		public bool AllowNullOrEmpty { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null && !(value is string))
				throw new InvalidOperationException($"{nameof(PasswordStrengthAttribute)} can only be used on string properties.");

			var stringValue = (string)value;
			if (string.IsNullOrEmpty(stringValue))
			{
				if (AllowNullOrEmpty)
					return ValidationResult.Success;
				else
					return new ValidationResult(ErrorMessage);
			}

			bool isValid = stringValue.Length >= Length &&
						   stringValue.Count(char.IsLower) >= LowerCaseLetters &&
						   stringValue.Count(char.IsUpper) >= UpperCaseLetters &&
						   stringValue.Count(char.IsDigit) >= Numbers &&
						   stringValue.Count(c => !char.IsLetterOrDigit(c)) >= SpecialCharacters;

			if (isValid)
				return ValidationResult.Success;

			return new ValidationResult(ErrorMessage);
		}

		public override string FormatErrorMessage(string name)
		{
			var lengthWord = Length == 1 ? "character" : "characters";
			var lowerWord = Length == 1 ? "letter" : "letters";
			var upperWord = Length == 1 ? "letter" : "letters";
			var numberWord = Length == 1 ? "number" : "numbers";
			var specialWord = Length == 1 ? "character" : "characters";

			return $"{name} must be at least {Length} {lengthWord} in length, and contain at least {LowerCaseLetters} lower case {lowerWord}, {UpperCaseLetters} upper case {upperWord}, {Numbers} {numberWord}, and {SpecialCharacters} special {specialWord}.";
		}
	}
}