#pragma checksum "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0fe2c79039d29fa0c3cd8b74fb66a0616ee1877a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Items_Delete), @"mvc.1.0.view", @"/Views/Items/Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0fe2c79039d29fa0c3cd8b74fb66a0616ee1877a", @"/Views/Items/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e07d67a06ae0e598a8715697dcb07e0ad9d2e24", @"/Views/_ViewImports.cshtml")]
    public class Views_Items_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ZoomIn.Models.Item>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark w-100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
  
    ViewData["Title"] = "Delete";
    Layout = "~/Views/Shared/_adminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Delete</h1>

<script type=""text/javascript"">
    window.onload = function () {
        var element = document.getElementById(""_items"");
        element.classList.add(""active"");
    }
</script>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Item</h4>
    <hr />

    <div>

");
#nullable restore
#line 24 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
          
            String img64 = Convert.ToBase64String(Model.ItemInnerFile);
            String img64Url = string.Format("data:image/" + Model.ItemExtension + ";base64,{0}", img64); //imagetype can be e.g. gif, jpeg, png etc.
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        <img");
            BeginWriteAttribute("src", " src=\"", 691, "\"", 706, 1);
#nullable restore
#line 28 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
WriteAttributeValue("", 697, img64Url, 697, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"100%\" height=\"100%\" />\r\n    </div>\r\n\r\n    <dl class=\"row mt-5\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Itemname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Itemname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 39 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Uploaddate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Uploaddate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 45 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Createdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Createdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 51 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Width));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Width));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 57 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Height));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Height));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 63 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 69 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Itemdescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Itemdescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 75 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Takenlocation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 78 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Takenlocation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 81 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Noviews));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 84 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Noviews));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 87 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 90 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Category.Catename));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 93 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Owner));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 96 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Owner.UserUsername));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 99 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Type));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 102 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Type.Typename));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0fe2c79039d29fa0c3cd8b74fb66a0616ee1877a13799", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "0fe2c79039d29fa0c3cd8b74fb66a0616ee1877a14066", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 107 "E:\Courses\Tahaluf\6_First project\ZoomIn\ZoomIn\Views\Items\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ItemId);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
        <div class=""form-group row"">
            <div class=""col-8 offset-7 row"">
                <div class=""col-4"">
                    <input type=""submit"" value=""Delete"" class=""btn btn-danger w-100"" />
                </div>
                <div class=""col-4"">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0fe2c79039d29fa0c3cd8b74fb66a0616ee1877a16068", async() => {
                    WriteLiteral("<i class=\"fas fa-sign-out-alt\"></i> &nbsp; Back ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ZoomIn.Models.Item> Html { get; private set; }
    }
}
#pragma warning restore 1591
