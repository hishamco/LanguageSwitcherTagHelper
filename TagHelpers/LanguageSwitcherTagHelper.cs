using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;

namespace LanguageSwitcherTagHelper.TagHelpers
{
    [HtmlTargetElement("language-switcher")]
    public class LanguageSwitcherTagHelper : TagHelper
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LanguageSwitcherTagHelper(IOptions<RequestLocalizationOptions> options)
        {
            _localizationOptions = options;
        }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public DisplayMode Mode { get; set; } = DisplayMode.ImageAndText;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var selectedCulture = ViewContext.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
            var cultures = _localizationOptions.Value.SupportedUICultures;

            output.TagName = null;

            switch (Mode)
            {
                case DisplayMode.ImageAndText:
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropdown'>
                            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'><img src='/images/{selectedCulture.Name}.png' /> {selectedCulture.EnglishName}<span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a href='#' onclick=\"appendLanguageSwitcherCookie('{culture.Name}')\"><img src='/images/{culture.Name}.png' /> {culture.EnglishName}</a></li>");
                    }
                    break;
                case DisplayMode.Image:
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropdown'>
                            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'><img src='/images/{selectedCulture.Name}.png' /> <span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a href='#' onclick=\"appendLanguageSwitcherCookie('{culture.Name}')\"><img src='/images/{culture.Name}.png' /></a></li>");
                    }
                    break;
                case DisplayMode.Text:
                    
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropdown'>
                            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'> {selectedCulture.EnglishName}<span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a href='#' onclick=\"appendLanguageSwitcherCookie('{culture.Name}')\">{culture.EnglishName}</a></li>");
                    }
                    break;
            }
            
            output.Content.AppendHtml(@"</ul>
                        </li>
                    </ul>");
        }
    }
}