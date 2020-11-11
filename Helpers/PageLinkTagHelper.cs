using CinemaPortal_ASP.NET_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Policy;

namespace CinemaPortal_ASP.NET_Core.Helpers
{
    public class PageLinkTagHelper:TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";
            
            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination justify-content-center");

            // создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage)
            {
                TagBuilder backBtn = CreateBtn(PageModel.PageNumber - 1, urlHelper, "Назад");
                //TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
                tag.InnerHtml.AppendHtml(backBtn);
                //tag.InnerHtml.AppendHtml(prevItem);
            }
            // формируем три ссылки - на текущую, предыдущую и следующую

            for (int i=1;i<=PageModel.TotalPages;i++)
            {
                TagBuilder currentItem = CreateTag(i, urlHelper);
                tag.InnerHtml.AppendHtml(currentItem);
            }
            

            

            
            // создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage)
            {
                
                TagBuilder nextBtn = CreateBtn(PageModel.PageNumber + 1, urlHelper,"Вперед");
                tag.InnerHtml.AppendHtml(nextBtn);
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateBtn(int pageNumber,IUrlHelper urlHelper, string btnText)
        {
            TagBuilder link = new TagBuilder("a");
            link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            link.AddCssClass("page-link");
            link.InnerHtml.Append(btnText);
            return link;
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
