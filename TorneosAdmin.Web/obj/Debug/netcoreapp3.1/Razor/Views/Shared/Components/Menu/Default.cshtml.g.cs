#pragma checksum "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79b1c4b832c5dcdd7a1e18b32af426e8fee947e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Menu_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Menu/Default.cshtml")]
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
#line 1 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\_ViewImports.cshtml"
using TorneosAdmin.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
using TorneosAdmin.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79b1c4b832c5dcdd7a1e18b32af426e8fee947e5", @"/Views/Shared/Components/Menu/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21e66fed2d491451bfd1f247411f192a0ad0309d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_Menu_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MenuViewModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<ul class=\"nav nav-list\">\n    <!-- /.nav-list -->\n");
#nullable restore
#line 76 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
   Write(Html.Raw(new Microsoft.AspNetCore.Html.HtmlString(Menu(Model))));

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
                                                                        ;
    

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 4 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
 
    String Menu(IEnumerable<MenuViewModel> menus)
    {
        var menu = "";
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
         foreach (var item in menus)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
             if (item.SubMenus == null)
            {
                menu = menu + $@"<li class="""">";
                menu = menu + $@"<a href=""../" + item.Ruta + $@""">";
                menu = menu + $@"<i class=""menu-icon " + item.Icono + $@"""></i>";
                menu = menu + $@"<span class=""menu-text"">" + item.Titulo + $@"</span>";
                menu = menu + $@"</a>";
                menu = menu + $@"<b class=""arrow""></b>";
                menu = menu + $@"</li>";
            }
            else
            {
                menu = menu + $@"<li class="""">";
                menu = menu + $@"<a href=""#"" class=""dropdown-toggle"">";
                menu = menu + $@"<i class=""menu-icon " + item.Icono + $@"""></i>";
                menu = menu + $@"<span class=""menu-text"">" + item.Titulo + $@"</span>";
                menu = menu + $@"<b class=""arrow fas fa-angle-down""></b>";
                menu = menu + $@"</a>";
                menu = menu + $@"<b class=""arrow""></b>";
                menu = menu + $@"<ul class=""submenu"">";
                menu = menu + @SubMenu(item.SubMenus);
                menu = menu + $@"</ul></li>";
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
             

        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
         
        return menu;
    }
    String SubMenu(IEnumerable<MenuViewModel> menus)
    {
        var menu = "";
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
         foreach (var item in menus)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
             if (item.SubMenus == null)
            {
                menu = menu + $@"<li class="""">";
                menu = menu + $@"<a href=""../" + item.Ruta + $@""">";
                menu = menu + $@"<i class=""menu-icon fas fa-caret-right""></i>";
                menu = menu + $@"<i class=""" + item.Icono + $@"""></i> ";
                menu = menu + $@"<span class=""menu-text"">" + item.Titulo + $@"</span>";
                menu = menu + $@"</a>";
                menu = menu + $@"<b class=""arrow""></b>";
                menu = menu + $@"</li>";
            }
            else
            {
                menu = menu + $@"<li class="""">";
                menu = menu + $@"<a href=""#"" class=""dropdown-toggle"">";
                menu = menu + $@"<i class=""menu-icon fas fa-caret-right""></i>";
                menu = menu + $@"<i class=""" + item.Icono + $@"""></i>";
                menu = menu + $@"<span class=""menu-text"">" + item.Titulo + $@"</span>";
                menu = menu + $@"<b class=""arrow fas fa-angle-down""></b>";
                menu = menu + $@"</a>";
                menu = menu + $@"<b class=""arrow""></b>";
                menu = menu + $@"<ul class=""submenu"">";
                menu = menu + @Menu(item.SubMenus);
                menu = menu + $@"</ul></li>";
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
             

        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 68 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Shared\Components\Menu\Default.cshtml"
         
        return menu;
    }

#line default
#line hidden
#nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MenuViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
