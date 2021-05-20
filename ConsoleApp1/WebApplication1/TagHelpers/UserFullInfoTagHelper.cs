using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication1.TagHelpers
{
	[HtmlTargetElement("user-full-info")]
	public class UserFullInfoTagHelper : TagHelper
	{
		public string FirstName { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{

			string preContent = $@"
				<h2>Full information about {FirstName}</h2>
				<div class='row'>
			";
			const string postContent = @"</div>";

			output.TagName = "div";
			output.PreContent.SetHtmlContent(preContent);
			output.PostContent.SetHtmlContent(postContent);
		}
	}
}
