#pragma checksum "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6267a8231205d8ec29d2b0730cfca1a86e249c01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(web.Pages.Clients.Pages_Clients_Index), @"mvc.1.0.razor-page", @"/Pages/Clients/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Clients/Index.cshtml", typeof(web.Pages.Clients.Pages_Clients_Index), null)]
namespace web.Pages.Clients
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\_ViewImports.cshtml"
using web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6267a8231205d8ec29d2b0730cfca1a86e249c01", @"/Pages/Clients/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"899aeb869cd09b2ba6752583e2712da9d81d87c9", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Clients_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Client", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-id", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
  
    ViewData["Title"] = "Clients";

#line default
#line hidden
            DefineSection("Scripts", async() => {
                BeginContext(84, 10, true);
                WriteLiteral("\n  <script");
                EndContext();
                BeginWriteAttribute("src", " src=\'", 94, "\'", 126, 1);
#line 8 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
WriteAttributeValue("", 100, Url.Content("/js/ims.js"), 100, 26, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(127, 11, true);
                WriteLiteral("></script>\n");
                EndContext();
            }
            );
            BeginContext(140, 67, true);
            WriteLiteral("\n<div class=\"text-center container\">\n    <div class=\"row\">\n        ");
            EndContext();
            BeginContext(207, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6267a8231205d8ec29d2b0730cfca1a86e249c014890", async() => {
                BeginContext(272, 14, true);
                WriteLiteral("Add New Client");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(290, 260, true);
            WriteLiteral(@"
    </div>
    <div>
        <table id=""clientTable"" class=""table table-striped table-hover"">
            <thead>
                <th>Name</th>
                <th>Description</th>
                <th>Client Page</th>
            </thead>
            <tbody>
");
            EndContext();
#line 23 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
                 foreach (var client in Model.AllClients)
                {

#line default
#line hidden
            BeginContext(626, 82, true);
            WriteLiteral("                    <tr>\n                        <td>\n                            ");
            EndContext();
            BeginContext(709, 11, false);
#line 27 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
                       Write(client.Name);

#line default
#line hidden
            EndContext();
            BeginContext(720, 88, true);
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
            EndContext();
            BeginContext(809, 18, false);
#line 30 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
                       Write(client.Description);

#line default
#line hidden
            EndContext();
            BeginContext(827, 88, true);
            WriteLiteral("\n                        </td>\n                        <td>\n                            ");
            EndContext();
            BeginContext(915, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6267a8231205d8ec29d2b0730cfca1a86e249c018371", async() => {
                BeginContext(970, 9, true);
                WriteLiteral("View/edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 33 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
                                                     WriteLiteral(client.ClientId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(983, 57, true);
            WriteLiteral("\n                        </td>\n                    </tr>\n");
            EndContext();
#line 36 "c:\Users\Ben\Documents\Projects\ims-microservices\web\Pages\Clients\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1058, 56, true);
            WriteLiteral("            </tbody>\n        </table>\n    </div>\n</div>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ClientsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClientsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClientsModel>)PageContext?.ViewData;
        public ClientsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
