#pragma checksum "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2116cab2236fe3eedeeb18277773ca00bbc5d22a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Campeonato_TableroPosiciones), @"mvc.1.0.view", @"/Views/Campeonato/TableroPosiciones.cshtml")]
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
#line 2 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\_ViewImports.cshtml"
using TorneosAdmin.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2116cab2236fe3eedeeb18277773ca00bbc5d22a", @"/Views/Campeonato/TableroPosiciones.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21e66fed2d491451bfd1f247411f192a0ad0309d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Campeonato_TableroPosiciones : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
  
    ViewData["Title"] = "Tablero de Posiciones";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 7 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""page-header"">
    <h1>
        Tablero de posiciones
        <small>
            <i class=""ace-icon fa fa-angle-double-right""></i>
            Esta sección es para la visualización de posiciones del campeonato.
        </small>
    </h1>
</div><!-- /.page-header -->

<div class=""row"">
    <div class=""col-xs-12"">
        <!-- PAGE CONTENT BEGINS -->
        <div id=""busqueda"" class=""form-inline"">
            <h4>
                <i class=""ace-icon fas fa-hand-point-right icon-animated-hand-pointer blue""></i>
                <select class=""form-control"" id=""formCampeonatos"">
");
#nullable restore
#line 26 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                     foreach (var item in ViewBag.CampeonatosLista)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2116cab2236fe3eedeeb18277773ca00bbc5d22a5007", async() => {
#nullable restore
#line 28 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                                             Write(item.Value);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                           WriteLiteral(item.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 29 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n                &nbsp;\n                <select class=\"form-control\" id=\"formCategoria\">\n");
#nullable restore
#line 33 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                     foreach (var item in ViewBag.CategoriasLista)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2116cab2236fe3eedeeb18277773ca00bbc5d22a7719", async() => {
#nullable restore
#line 35 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                                             Write(item.Value);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 35 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                           WriteLiteral(item.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 36 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n                &nbsp;\n                <select class=\"form-control\" id=\"formSerie\">\n");
#nullable restore
#line 40 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                     foreach (var item in ViewBag.SeriesLista)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2116cab2236fe3eedeeb18277773ca00bbc5d22a10423", async() => {
#nullable restore
#line 42 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                                             Write(item.Value);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 42 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                           WriteLiteral(item.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 43 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n                &nbsp;\n                <select class=\"form-control\" id=\"formRonda\">\n");
#nullable restore
#line 47 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                     foreach (var item in ViewBag.RondasLista)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2116cab2236fe3eedeeb18277773ca00bbc5d22a13128", async() => {
#nullable restore
#line 49 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                                             Write(item.Value);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 49 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                           WriteLiteral(item.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 50 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Campeonato\TableroPosiciones.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </select>
                <button id=""btnseleccionar"" class=""pull-right btn btn-sm btn-primary"">
                    <i class=""ace-icon fas fa-pen-fancy""></i>
                    <span class=""bigger-110"">Seleccionar Partido</span>
                </button>
            </h4>
        </div>
        <div id=""tablero"" class=""hidden"">
            <div class=""row"">
                <div class=""col-xs-12 text-right"">
                    <button id=""btnRegresar"" class=""btn btn-app btn-primary"">
                        <i class=""ace-icon fas fa-undo-alt bigger-230""></i>
                        <span>Regresar</span>
                    </button>
                </div>
            </div>
            <br />
            <div class=""row"">
                <!--final es este tabla-->
                <table id=""posiciones-table"" class=""table table-striped table-bordered table-hover"">
                    <thead>
                        <tr>
                            <th>
                                Posición");
            WriteLiteral(@"
                            </th>
                            <th>
                                Equipo
                            </th>
                            <th>
                                Partidos Jugados
                            </th>
                            <th>
                                Partidos Ganados
                            </th>
                            <th>
                                Partidos Empatados
                            </th>
                            <th>
                                Partidos Perdidos
                            </th>
                            <th>
                                Goles a Favor
                            </th>
                            <th>
                                Goles en Contra
                            </th>
                            <th>
                                Diferencia Goles
                            </th>
                            <th>
                                Puntos
");
            WriteLiteral(@"                            </th>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div id=""dialogSeleccionar"" class=""hidden"">
            <div class=""row"">
                <div id=""errorDiv"" class=""col-lg-12"">
                </div>
            </div>
            <div class=""row"">
                <div class=""col-xs-12"">
                    <h2><strong>Seleccione un partido</strong></h2>
                </div>
            </div><!-- /.row -->
        </div><!-- /.dialog -->
        <template id=""posicionesTemplate"">
            <tr>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""width:120px;"">
                    <img");
            BeginWriteAttribute("src", " src=\"", 4654, "\"", 4660, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""width:50px;height:50px"" /><br />
                    <span></span>
                </td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
                <td class=""text-center"" style=""font-size: 20px;""></td>
            </tr>
        </template><!-- row template -->
        <!-- PAGE CONTENT ENDS -->
    </div><!-- /.col -->
</div><!-- /.row -->

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <!-- inline scripts related to this page -->
    <script type=""text/javascript"">

        jQuery(function ($) {

            // Configuración para colocar el el titulo del dialog con html
            $.widget(""ui.dialog"", $.extend({}, $.ui.dialog.prototype, {
                _title: function (title) {
                    var $title = this.options.title || '&nbsp;'
                    if ((""title_html"" in this.options) && this.options.title_html == true)
                        title.html($title);
                    else title.text($title);
                }
            }));

            $(""#tablero"").hide();

            //Creamos el dialogo para eliminar grupo jornadas
            dialogSeleccionar = $(""#dialogSeleccionar"").dialog({
                modal: true,
                title: ""<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-check'></i>Tablero de Posiciones</h4></div>"",
                title_html: true,
                autoOpen: false,
                ");
                WriteLiteral(@"height: 200,
                width: 450,
                buttons: [
                    {
                        text: ""Ok"",
                        ""class"": ""btn btn-minier"",
                        click: function () {
                            $(this).dialog(""close"");
                        }
                    }
                ]
            });

            // Cuando damos click en boton mostramos los datos del partido
            $(""#btnseleccionar"").on(""click"", function (e) {
                var jornadaModel = {
                    campeonatoID: $(""#formCampeonatos"").val(),
                    categoriaID: $(""#formCategoria"").val(),
                    serieID: $(""#formSerie"").val(),
                    ronda: $(""#formRonda"").val()
                }

                $.get(""../Jornadas/ObtenerJornadasTablero"", jornadaModel, function (data) {
                    $(""#posiciones-table > tbody"").empty();
                    insertaTablero(data);
                    // Mostramos los datos del partido en");
                WriteLiteral(@" pantalla
                    $(""#busqueda"").slideUp(1000);
                    $(""#tablero"").removeClass(""hidden"").slideDown(5000);
                });
            });

            // Muestra los controles para seleccionar el partido
            $(""#btnRegresar"").on(""click"", function (e) {
                $(""#busqueda"").slideDown(1000);
                $(""#tablero"").slideUp(2000);
            });
        });
    </script>
");
            }
            );
            WriteLiteral("\n");
        }
        #pragma warning restore 1998
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
