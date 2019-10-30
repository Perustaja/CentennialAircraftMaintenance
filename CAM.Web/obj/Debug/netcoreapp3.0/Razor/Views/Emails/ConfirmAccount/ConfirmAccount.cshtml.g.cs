#pragma checksum "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\Emails\ConfirmAccount\ConfirmAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e97f496c6b9127ec7a0457715f62da3942df30f2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Emails_ConfirmAccount_ConfirmAccount), @"mvc.1.0.view", @"/Views/Emails/ConfirmAccount/ConfirmAccount.cshtml")]
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
#line 1 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\_ViewImports.cshtml"
using CAM.Infrastructure.Data.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\Emails\ConfirmAccount\ConfirmAccount.cshtml"
using CAM.Web.Views.Emails.ConfirmAccount;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\Emails\ConfirmAccount\ConfirmAccount.cshtml"
using CAM.Web.Views.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e97f496c6b9127ec7a0457715f62da3942df30f2", @"/Views/Emails/ConfirmAccount/ConfirmAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb662ecb4ab0a3ff1c57055be266e9461abb64b7", @"/Views/_ViewImports.cshtml")]
    public class Views_Emails_ConfirmAccount_ConfirmAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ConfirmAccountEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\Emails\ConfirmAccount\ConfirmAccount.cshtml"
  
    ViewData["EmailTitle"] = "Welcome!";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    We\'re excited to have you get started. First, you need to confirm your account. Just press the button below.\r\n</p>\r\n\r\n<br />\r\n\r\n");
#nullable restore
#line 15 "C:\Dev\CentennialAircraftMaintenance\CAM.Web\Views\Emails\ConfirmAccount\ConfirmAccount.cshtml"
Write(await Html.PartialAsync("EmailButton", new EmailButtonViewModel("Confirm Account", Model.ConfirmAccountUrl)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<br />\r\n\r\n<p>\r\n    If you have any questions, just reply to this email—we\'re always happy to help out.\r\n</p>\r\n\r\n<br />\r\n\r\n<p>\r\n    The Contoso Team\r\n</p>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ConfirmAccountEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
