namespace CustomValidationDemo.AttributeAdapters
{
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
	using Microsoft.Extensions.Localization;

	public class RequiredIfMatchAttributeAdapter : AttributeAdapterBase<RequiredIfMatchAttribute>
	{
		public RequiredIfMatchAttributeAdapter(RequiredIfMatchAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
		{
		}

		public override void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-requiredifmatch", GetErrorMessage(context));
			MergeAttribute(context.Attributes, "data-val-requiredifmatch-propertyname", Attribute.PropertyName);
			MergeAttribute(context.Attributes, "data-val-requiredifmatch-propertyvalues", string.Join(';', Attribute.PropertyValues));
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext)
		{
			return Attribute.ErrorMessage ?? GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
		}
	}
}