using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace LanguageSwitcherTagHelper.TagHelpers
{
    public class LanguageSwitcherTagHelperComponent : TagHelperComponent
    {
        public override int Order => 1;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "head", StringComparison.Ordinal))
            {
                output.PostContent.AppendHtml(string.Format(Resources.LanguageSwitcherScript,
                    CookieRequestCultureProvider.DefaultCookieName));
            }              

            return Task.CompletedTask;
        }
    }
}
