using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;
using WebApplication1.ViewModels;

namespace WebApplication1.TagHelpers
{
	[HtmlTargetElement("user-violation", Attributes = "user-violations", ParentTag = "user-full-info")]
	public class UserViolationTagHelper : TagHelper
	{
		[HtmlAttributeName("user-violations")]
		public List<UserViolationViewModel> Violations { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			StringBuilder sb = new StringBuilder();

			string preContent = $@"
				<table class='table'>
				    <thead>
				        <tr>
				            <th scope='col'>SeverityViolation</th>
				            <th scope='col'>DateExpired</th>
				            <th scope='col'>Description</th>
							<th scope='col'>Actions</th>
				         </tr>
				    </thead>
				<tbody class='violations-body'>";

			sb.Append(preContent);

			foreach (UserViolationViewModel userViolation in Violations)
			{
				sb.Append($@"
					<tr class='violation'>
						<td> {userViolation.SeverityViolation} </td>
						<td> {userViolation.DateExpired} </td>
						<td> {userViolation.Description} </td>
						<td><button class='btn btn-outline-info amnesty' data-violationid='{userViolation.ViolationId}'>Amnesty</button></td>
					</tr>		
");
			}
			string postContent = @"
					</tbody>
				</table>";

			sb.Append(postContent);
			output.Attributes.SetAttribute("class", "col-9 shadow-generate");
			output.Content.SetHtmlContent(sb.ToString());
		}
	}
}
