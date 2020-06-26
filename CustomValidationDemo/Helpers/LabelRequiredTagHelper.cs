namespace CustomValidationDemo.Helpers
{
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.AspNetCore.Mvc.TagHelpers;
	using Microsoft.AspNetCore.Mvc.ViewFeatures;
	using Microsoft.AspNetCore.Razor.TagHelpers;
	using System.Threading.Tasks;

	[HtmlTargetElement("label", Attributes = "asp-for")]
	public class LabelRequiredTagHelper : LabelTagHelper
	{
		public LabelRequiredTagHelper(IHtmlGenerator generator) : base(generator) { }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			await base.ProcessAsync(context, output);

			bool isBool = For.Metadata.ModelType.Name == "Boolean";
			if (isBool)
				return;

			if (For.Metadata.IsRequired)
			{
				var sup = new TagBuilder("sup");
				sup.InnerHtml.Append(" *");
				output.Content.AppendHtml(sup);
			}
		}
	}
}
