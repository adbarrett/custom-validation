namespace CustomValidationDemo.ValidationAttributeAdapterProviders
{
	using AttributeAdapters;
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.Extensions.Localization;
	using System.ComponentModel.DataAnnotations;

	public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
	{
		readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

		public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
			IStringLocalizer stringLocalizer)
		{
			switch (attribute)
			{
				case IsTrueAttribute isTrueAttribute:
					return new IsTrueAttributeAdapter(isTrueAttribute, stringLocalizer);
				case PasswordStrengthAttribute passwordStrengthAttribute:
					return new PasswordStrengthAttributeAdapter(passwordStrengthAttribute, stringLocalizer);
				case RequiredIfMatchAttribute requiredIfMatchAttribute:
					return new RequiredIfMatchAttributeAdapter(requiredIfMatchAttribute, stringLocalizer);
				default:
					return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
			}
		}
	}
}