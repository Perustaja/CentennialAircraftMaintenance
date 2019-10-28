#pragma checksum "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5089ca35d15a13c08e4f99d915df222b55e3587e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Pages_Account_Login), @"mvc.1.0.razor-page", @"/Areas/Identity/Pages/Account/Login.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Areas/Identity/Pages/Account/Login.cshtml", typeof(AspNetCore.Areas_Identity_Pages_Account_Login), null)]
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
#line 1 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/_ViewImports.cshtml"
using CAM.Web.Areas.Identity.Pages.Account;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5089ca35d15a13c08e4f99d915df222b55e3587e", @"/Areas/Identity/Pages/Account/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b50e685b95abf4a591f4dccd6752662ca5be8c5f", @"/Areas/Identity/Pages/Account/_ViewImports.cshtml")]
    public class Areas_Identity_Pages_Account_Login : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(26, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
  
    ViewData["Title"] = "Log in";

#line default
#line hidden
            BeginContext(70, 6, true);
            WriteLiteral("\r\n<h1>");
            EndContext();
            BeginContext(77, 17, false);
#line 8 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(94, 1102, true);
            WriteLiteral(@"</h1>
<div class=""row"">
    <div class=""col-md-4"">
        <section>
            <form id=""account"" method=""post"">
                <h4>Use a local account to log in.</h4>
                <hr />
                <div asp-validation-summary=""All"" class=""text-danger""></div>
                <div class=""form-group"">
                    <label asp-for=""Input.Email""></label>
                    <input asp-for=""Input.Email"" class=""form-control"" />
                    <span asp-validation-for=""Input.Email"" class=""text-danger""></span>
                </div>
                <div class=""form-group"">
                    <label asp-for=""Input.Password""></label>
                    <input asp-for=""Input.Password"" class=""form-control"" />
                    <span asp-validation-for=""Input.Password"" class=""text-danger""></span>
                </div>
                <div class=""form-group"">
                    <div class=""checkbox"">
                        <label asp-for=""Input.RememberMe"">
                ");
            WriteLiteral("            <input asp-for=\"Input.RememberMe\" />\r\n                            ");
            EndContext();
            BeginContext(1197, 44, false);
#line 30 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                       Write(Html.DisplayNameFor(m => m.Input.RememberMe));

#line default
#line hidden
            EndContext();
            BeginContext(1241, 506, true);
            WriteLiteral(@"
                        </label>
                    </div>
                </div>
                <div class=""form-group"">
                    <button type=""submit"" class=""btn btn-primary"">Log in</button>
                </div>
                <div class=""form-group"">
                    <p>
                        <a id=""forgot-password"" asp-page=""./ForgotPassword"">Forgot your password?</a>
                    </p>
                    <p>
                        <a asp-page=""./Register""");
            EndContext();
            BeginWriteAttribute("asp-route-returnUrl", " asp-route-returnUrl=\"", 1747, "\"", 1785, 1);
#line 42 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
WriteAttributeValue("", 1769, Model.ReturnUrl, 1769, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1786, 268, true);
            WriteLiteral(@">Register as a new user</a>
                    </p>
                </div>
            </form>
        </section>
    </div>
    <div class=""col-md-6 col-md-offset-2"">
        <section>
            <h4>Use another service to log in.</h4>
            <hr />
");
            EndContext();
#line 52 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
              
                if ((Model.ExternalLogins?.Count ?? 0) == 0)
                {

#line default
#line hidden
            BeginContext(2151, 404, true);
            WriteLiteral(@"                    <div>
                        <p>
                            There are no external authentication services configured. See <a href=""https://go.microsoft.com/fwlink/?LinkID=532715"">this article</a>
                            for details on setting up this ASP.NET application to support logging in via external services.
                        </p>
                    </div>
");
            EndContext();
#line 61 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(2615, 74, true);
            WriteLiteral("                    <form id=\"external-account\" asp-page=\"./ExternalLogin\"");
            EndContext();
            BeginWriteAttribute("asp-route-returnUrl", " asp-route-returnUrl=\"", 2689, "\"", 2727, 1);
#line 64 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
WriteAttributeValue("", 2711, Model.ReturnUrl, 2711, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2728, 105, true);
            WriteLiteral(" method=\"post\" class=\"form-horizontal\">\r\n                        <div>\r\n                            <p>\r\n");
            EndContext();
#line 67 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                                 foreach (var provider in Model.ExternalLogins)
                                {

#line default
#line hidden
            BeginContext(2949, 97, true);
            WriteLiteral("                                    <button type=\"submit\" class=\"btn btn-primary\" name=\"provider\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3046, "\"", 3068, 1);
#line 69 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
WriteAttributeValue("", 3054, provider.Name, 3054, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 3069, "\"", 3124, 6);
            WriteAttributeValue("", 3077, "Log", 3077, 3, true);
            WriteAttributeValue(" ", 3080, "in", 3081, 3, true);
            WriteAttributeValue(" ", 3083, "using", 3084, 6, true);
            WriteAttributeValue(" ", 3089, "your", 3090, 5, true);
#line 69 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
WriteAttributeValue(" ", 3094, provider.DisplayName, 3095, 21, false);

#line default
#line hidden
            WriteAttributeValue(" ", 3116, "account", 3117, 8, true);
            EndWriteAttribute();
            BeginContext(3125, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(3127, 20, false);
#line 69 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                                                                                                                                                                            Write(provider.DisplayName);

#line default
#line hidden
            EndContext();
            BeginContext(3147, 11, true);
            WriteLiteral("</button>\r\n");
            EndContext();
#line 70 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                                }

#line default
#line hidden
            BeginContext(3193, 95, true);
            WriteLiteral("                            </p>\r\n                        </div>\r\n                    </form>\r\n");
            EndContext();
#line 74 "/home/perustaja/Dev/CentennialAircraftMaintenance/CAM.Web/Areas/Identity/Pages/Account/Login.cshtml"
                }
            

#line default
#line hidden
            BeginContext(3322, 42, true);
            WriteLiteral("        </section>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3382, 52, true);
                WriteLiteral("\r\n    <partial name=\"_ValidationScriptsPartial\" />\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoginModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LoginModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LoginModel>)PageContext?.ViewData;
        public LoginModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
