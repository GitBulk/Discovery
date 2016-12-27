using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        //public string MailTo { get; set; }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    output.TagName = "a"; // replaces <email> with <a> tag
        //    string address = this.MailTo;
        //    output.Attributes.SetAttribute("href", "mailto:" + address);
        //    output.Content.SetContent(address);
        //}

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            string target = content.GetContent();
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }
    }
}
