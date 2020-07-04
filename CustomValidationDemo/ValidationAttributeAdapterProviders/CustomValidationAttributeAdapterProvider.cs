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
			return attribute switch
			{
				IsTrueAttribute isTrueAttribute =>
					new IsTrueAttributeAdapter(isTrueAttribute, stringLocalizer),
				PasswordStrengthAttribute passwordStrengthAttribute =>
					new PasswordStrengthAttributeAdapter(
						passwordStrengthAttribute, stringLocalizer),
				RequiredIfMatchAttribute requiredIfMatchAttribute =>
					new RequiredIfMatchAttributeAdapter(
						requiredIfMatchAttribute, stringLocalizer),
				DateRangeFromTodayAttribute dateRangeFromTodayAttribute =>
					new DateRangeFromTodayAttributeAdapter(
						dateRangeFromTodayAttribute, stringLocalizer),
				_ => _baseProvider.GetAttributeAdapter(attribute, stringLocalizer)
			};
		}
	}
}