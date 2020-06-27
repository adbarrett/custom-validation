namespace CustomValidationDemo.Helpers
{
	using Attributes;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.AspNetCore.Mvc.TagHelpers;
	using Microsoft.AspNetCore.Mvc.ViewFeatures;
	using Microsoft.AspNetCore.Razor.TagHelpers;
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	[HtmlTargetElement("label", Attributes = "asp-for")]
	public class LabelRequiredTagHelper : LabelTagHelper
	{
		public LabelRequiredTagHelper(IHtmlGenerator generator) : base(generator) { }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			await base.ProcessAsync(context, output);

			if (!(For.Metadata is DefaultModelMetadata metadata))
				return;

			var isBool = metadata.ModelType.Name == "Boolean";

			var isRequired =
				(!isBool && metadata.IsRequired) ||
				HasAttribute(metadata, typeof(IsTrueAttribute)) ||
				HasAttribute(metadata, typeof(RequiredIfMatchAttribute));

			if (!isRequired)
				return;

			var sup = new TagBuilder("sup");
			sup.InnerHtml.Append(" *");

			if (HasAttribute(metadata, typeof(RequiredIfMatchAttribute)))
				sup.Attributes.Add("hidden", "hidden");

			output.Content.AppendHtml(sup);
		}

		private bool HasAttribute(DefaultModelMetadata metadata, Type type)
		{
			return metadata.Attributes.PropertyAttributes
				.Any(i => i.GetType() == type);
		}
	}
}
