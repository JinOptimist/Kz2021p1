using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApplication1.ViewModels;

namespace WebApplication1.TagHelpers
{
	[HtmlTargetElement("user-card", Attributes = "user-info", ParentTag = "user-full-info")]
	public class UserCardTagHelper : TagHelper
    {
		[HtmlAttributeName("user-info")]
		public UserInfoViewModel UserInfo { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			string content = $@"
                <div class='personal-card'>
                    <div class='personal-card-img'>
                        <img src='{UserInfo.Uri}' alt='Avatar' style='width: 300px; height: 350px' />
                    </div>
                    <div class='personal-card-description'>
                        <h5>Full name: <span>{UserInfo.Name}</span></h5>
                        <h5>Age: <span>{UserInfo.Age}</span></h5>
                        <h5>Street: <span>{UserInfo.Street}</span></h5>                     
                        <h5>House number: <span>{UserInfo.HouseNumber}</span></h5>
                        <h5>Live in city from: <span>{UserInfo.CreateDate}</span></h5>
                        <div class='btn-group mt-3'>
                            <button class='btn btn-outline-danger add-violation' id='add-violation' data-izimodal-open='#modal-custom'>Add Violation</button>
                            <button class='btn btn-outline-danger goToJail'>Go to jail</button>                           
                        </div>
                    </div>
                <input type='file'
                    id='avatar' name='avatar'
                    accept='image/png, image/jpeg'>
                    <button class='btn btn-outline-danger upload-photo'>Upload</button>
                </div>";

            output.Attributes.SetAttribute("class", "col-3 p-2 shadow-generate");
            output.Attributes.SetAttribute("data-userid", $"{UserInfo.CitizenId}");
            output.Attributes.SetAttribute("id", "user-card");
            output.Content.SetHtmlContent(content);
		}
	}
}
