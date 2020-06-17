namespace CustomValidationDemo.AttributeAdapters
{
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
	using Microsoft.Extensions.Localization;

	public class IsTrueAttributeAdapter : AttributeAdapterBase<IsTrueAttribute>
	{
		public IsTrueAttributeAdapter(IsTrueAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
		{
		}

		public override void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-istrue", GetErrorMessage(context));
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext)
		{
			return Attribute.ErrorMessage ?? GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
		}
	}
}