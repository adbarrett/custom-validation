namespace CustomValidationDemo.AttributeAdapters
{
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
	using Microsoft.Extensions.Localization;
	using System;

	public class DateRangeFromTodayAttributeAdapter : AttributeAdapterBase<DateRangeFromTodayAttribute>
	{
		public DateRangeFromTodayAttributeAdapter(DateRangeFromTodayAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
		{
		}

		public override void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-daterangefromtoday", GetErrorMessage(context));

			DateTime minDate = Attribute.GetMinDate();
			MergeAttribute(context.Attributes, "min", Attribute.IsDateOnly ? minDate.Date.ToString("yyyy-MM-dd") : minDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));

			DateTime maxDate = Attribute.GetMaxDate();
			MergeAttribute(context.Attributes, "max", Attribute.IsDateOnly ? maxDate.Date.ToString("yyyy-MM-dd") : maxDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext)
		{
			return Attribute.ErrorMessage;
		}
	}
}