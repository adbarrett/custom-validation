namespace CustomValidationDemo.ValidationAttributeAdapterProviders
{
	using AttributeAdapters;
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.Extensions.Localization;
	using System.ComponentModel.DataAnnotations;

	public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
	{
		IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();
		public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
			IStringLocalizer stringLocalizer)
		{
			switch (attribute)
			{
				case IsTrueAttribute isTrueAttribute:
					return new IsTrueAttributeAdapter(isTrueAttribute, stringLocalizer);
				case PasswordStrengthAttribute passwordStrengthAttribute:
					return new PasswordStrengthAttributeAdapter(passwordStrengthAttribute, stringLocalizer);
				default:
					return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
			}
		}
	}
}