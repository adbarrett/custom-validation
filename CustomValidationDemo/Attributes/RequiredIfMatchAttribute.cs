namespace CustomValidationDemo.Attributes
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredIfMatchAttribute : RequiredAttribute
	{
		public string PropertyName { get; set; }

		public object[] PropertyValues
		{
			get => _propertyValues.Select(v => (object)v).ToArray();
			set => _propertyValues = value.Select(v => v?.ToString()).ToArray();
		}

		private string[] _propertyValues = { };

		public RequiredIfMatchAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}

		public RequiredIfMatchAttribute(string propertyName, string propertyValue) : this(propertyName)
		{
			_propertyValues = new[] { propertyValue };
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(PropertyName))
				throw new InvalidOperationException($"{nameof(PropertyName)} must be provided.");

			object instance = validationContext.ObjectInstance;
			object? actualPropertyValue = instance.GetType().GetProperty(PropertyName)?.GetValue(instance, null);
			return _propertyValues.Contains(actualPropertyValue?.ToString())
				? base.IsValid(value, validationContext)
				: ValidationResult.Success;
		}
	}
}
