namespace CustomValidationDemo.AttributeAdapters
{
	using Attributes;
	using Microsoft.AspNetCore.Mvc.DataAnnotations;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
	using Microsoft.Extensions.Localization;

	public class PasswordStrengthAttributeAdapter : AttributeAdapterBase<PasswordStrengthAttribute>
	{
		public PasswordStrengthAttributeAdapter(PasswordStrengthAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
		{
		}

		public override void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-passwordstrength", GetErrorMessage(context));
			MergeAttribute(context.Attributes, "data-val-passwordstrength-allownull", Attribute.AllowNullOrEmpty.ToString());
			MergeAttribute(context.Attributes, "data-val-passwordstrength-minlength", Attribute.Length.ToString());
			MergeAttribute(context.Attributes, "data-val-passwordstrength-lowers", Attribute.LowerCaseLetters.ToString());
			MergeAttribute(context.Attributes, "data-val-passwordstrength-uppers", Attribute.UpperCaseLetters.ToString());
			MergeAttribute(context.Attributes, "data-val-passwordstrength-numbers", Attribute.Numbers.ToString());
			MergeAttribute(context.Attributes, "data-val-passwordstrength-specials", Attribute.SpecialCharacters.ToString());
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext)
		{
			return Attribute.ErrorMessage;
		}
	}
}