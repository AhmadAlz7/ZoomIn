#pragma checksum "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ba4312a8414b27daf2ec1cd3c764e79a96744b28"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Balances_LoadPurchases), @"mvc.1.0.view", @"/Views/Balances/LoadPurchases.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\_ViewImports.cshtml"
using ZoomIn;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\_ViewImports.cshtml"
using ZoomIn.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba4312a8414b27daf2ec1cd3c764e79a96744b28", @"/Views/Balances/LoadPurchases.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e07d67a06ae0e598a8715697dcb07e0ad9d2e24", @"/Views/_ViewImports.cshtml")]
    public class Views_Balances_LoadPurchases : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ZoomIn.Models.Userpurchaseitem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreatePurchase", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 6 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
  
    ViewData["Title"] = "LoadPurchases";
    Layout = "~/Views/Shared/_adminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Purchases</h1>
<h4>Finance</h4>

<script type=""text/javascript"">
    window.onload = function () {
        var element = document.getElementById(""_finance"");
        element.classList.add(""active"");
    }
</script>

<div class=""row"">
    <div class=""col-12 text-right"">
");
#nullable restore
#line 23 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
         if (!string.IsNullOrEmpty(HttpContextAccessor.HttpContext.Session.GetString("UserObject") as string))
        {
            if (@HttpContextAccessor.HttpContext.Session.GetString("UserRoleId") == "1")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba4312a8414b27daf2ec1cd3c764e79a96744b285718", async() => {
                WriteLiteral("\r\n                    <i class=\"fas fa-plus\"></i> &nbsp; Add New Purchase\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 30 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ba4312a8414b27daf2ec1cd3c764e79a96744b287278", async() => {
                WriteLiteral("\r\n            <i class=\"fas fa-sign-out-alt\"></i> &nbsp; Back\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        <a class=""btn btn-success text-decoration-none text-white"" onclick=""htmlTableToExcel(document.getElementById('purchases').id, fileName = 'purchases')"">
            <i class=""fas fa-file-excel""></i> &nbsp; Excel
        </a>
    </div>
</div>
<table class=""table"" id=""purchases"">
    <thead>
        <tr>
            <th>Purchase Id</th>
            <th>
                ");
#nullable restore
#line 45 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
           Write(Html.DisplayNameFor(model => model.Purchasedate));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </th>
            <th>
                Payment
            </th>
            <th>
                Profit Rate
            </th>
            <th>
                Buyer ID
            </th>
            <th>
                Item ID
            </th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 62 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 66 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
               Write(item.PurchaseId.ToString("0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 69 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
               Write(Html.DisplayFor(modelItem => item.Purchasedate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    $&nbsp;");
#nullable restore
#line 72 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
                      Write(item.Paymenttotal.Value.ToString("00.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    %&nbsp;");
#nullable restore
#line 75 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
                       Write(item.Relatedsiterate.Value * 100);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 78 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
               Write(item.BuyerId.Value.ToString("0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 81 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
               Write(item.ItemId.Value.ToString("0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 84 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Balances\LoadPurchases.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ZoomIn.Models.Userpurchaseitem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
