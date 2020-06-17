namespace CustomValidationDemo.Attributes
{
	using System;
	using System.ComponentModel.DataAnnotations;

	[AttributeUsage(AttributeTargets.Property)]
	public class IsTrueAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
				return new ValidationResult(ErrorMessage);

			if (value.GetType() != typeof(bool))
				throw new InvalidOperationException($"{nameof(IsTrueAttribute)} can only be used on boolean properties.");

			return (bool)value ? ValidationResult.Success : new ValidationResult(ErrorMessage);
		}

		public override string FormatErrorMessage(string name)
		{
			return $"{name} must be true.";
		}
	}
}