#pragma checksum "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2df33c0937e5e7a52f042597bc116c7c9ee84346"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Registros_Calificacion), @"mvc.1.0.view", @"/Views/Registros/Calificacion.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2df33c0937e5e7a52f042597bc116c7c9ee84346", @"/Views/Registros/Calificacion.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21e66fed2d491451bfd1f247411f192a0ad0309d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Registros_Calificacion : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
  
    ViewData["Title"] = "Calificacion";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 7 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""page-header"">
    <h1>
        Calificar jugadores del Sistema
        <small>
            <i class=""ace-icon fa fa-angle-double-right""></i>
            Esta sección es con fin de calificar la información de un jugador.
        </small>
    </h1>
</div><!-- /.page-header -->

<div class=""row"">
    <div class=""col-xs-12"">
        <!-- PAGE CONTENT BEGINS -->
");
            WriteLiteral("\n        <table id=\"grid-table\"></table>\n        <div id=\"grid-pager\"></div>\n\n        <!-- PAGE CONTENT ENDS -->\n    </div><!-- /.col -->\n</div><!-- /.row -->\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    <!-- inline scripts related to this page -->\n    <script type=\"text/javascript\">\n\n        // Variable Globales\n        var listaLigas = JSON.parse(\'");
#nullable restore
#line 43 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                Write(ViewBag.LigasLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaDirigentes = JSON.parse(\'");
#nullable restore
#line 44 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                     Write(ViewBag.DirigentesLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaEquipos = JSON.parse(\'");
#nullable restore
#line 45 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                  Write(ViewBag.EquiposLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaEstadosCiviles = JSON.parse(\'");
#nullable restore
#line 46 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                         Write(ViewBag.EstadosCivilesLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaInstrucciones = JSON.parse(\'");
#nullable restore
#line 47 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                        Write(ViewBag.InstruccionesLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaProfesiones = JSON.parse(\'");
#nullable restore
#line 48 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                      Write(ViewBag.ProfesionesLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaProvincias = JSON.parse(\'");
#nullable restore
#line 49 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                     Write(ViewBag.ProvinciasLista);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'.replace(/&quot;/g, \'\"\'));\n        var listaParroquias = JSON.parse(\'");
#nullable restore
#line 50 "C:\Users\JohanHernández-SEII\Documents\JOHAN\UMG\PROYECTO DE GRADUACIÓN\AdminSoccerLeague\TorneosAdmin.Web\Views\Registros\Calificacion.cshtml"
                                     Write(ViewBag.ParroquiasLista);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'.replace(/&quot;/g, '""'));

        jQuery(function ($) {

            var grid_selector = ""#grid-table"";
            var pager_selector = ""#grid-pager"";
            var parent_column = $(grid_selector).closest('[class*=""col-""]');

            //resize to fit page size
            $(window).on('resize.jqGrid', function () {
                $(grid_selector).jqGrid('setGridWidth', parent_column.width());
            });

            //resize on sidebar collapse/expand
            $(document).on('settings.ace.jqGrid', function (ev, event_name, collapsed) {
                if (event_name === 'sidebar_collapsed' || event_name === 'main_container_fixed') {
                    //setTimeout is for webkit only to give time for DOM changes and then redraw!!!
                    setTimeout(function () {
                        $(grid_selector).jqGrid('setGridWidth', parent_column.width());
                    }, 20);
                }
            })

            jQuery(grid_selector).jqGrid({
                url: ""/Equ");
                WriteLiteral(@"ipos/ObtenerEquipos"",
                datatype: 'json',
                mtype: 'GET',
                height: 320,
                colNames: ['ID', 'Nombre', 'Color', 'Fecha Fundación', 'Dirigente', 'Liga', 'Foto'],
                colModel: [
                    { key: true, hidden: true, name: 'id', index: 'id', editable: false },
                    { name: 'nombre', index: 'nombre', width: 150, editable: false },
                    { name: 'color', index: 'color', width: 50, editable: false },
                    { name: 'fechaFundacion', index: 'fechaFundacion', width: 90, editable: false, sorttype: ""date"", formatter: dateTable },
                    { name: 'dirigenteID', index: 'dirigenteID', width: 150, editable: false, edittype: 'select', formatter: 'select', editoptions: { value: getDirigentes() } },
                    { name: 'ligaID', index: 'ligaID', width: 150, editable: false, edittype: 'select', formatter: 'select', editoptions: { value: getLigas() } },
                    { name: 'foto', in");
                WriteLiteral(@"dex: 'foto', width: 150, align: 'center', editable: false, edittype: ""file"", editoptions: { enctype: 'multipart/form-data' }, formatter: imageFormat }
                ],
                iconSet: ""fontAwesome"",
                viewrecords: true,
                rowNum: 5,
                rowList: [5, 10, 15, 20, 25],
                pager: pager_selector,
                altRows: true,
                //toppager: true,

                multiselect: false,
                //multikey: ""ctrlKey"",
                //multiboxonly: true,

                loadComplete: function () {
                    var table = this;
                    setTimeout(function () {
                        updatePagerIcons(table);
                        enableTooltips(table);
                    }, 0);
                },
                caption: ""Equipos"",
                subGrid: true,
                subGridOptions: {
                    plusicon: ""fas fa-chevron-right"",
                    minusicon: ""fas fa-chevron-down"",
         ");
                WriteLiteral(@"           openicon: ""fas fa-angle-double-right"",
                    expandOnLoad: false,
                    selectOnExpand: false,
                    reloadOnExpand: true
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    // we pass two parameters
                    // subgrid_id is a id of the div tag created within a table
                    // the row_id is the id of the row
                    // If we want to pass additional parameters to the url we can use
                    // the method getRowData(row_id) - which returns associative array in type name-value
                    // here we can easy construct the following
                    var subgrid_table_id;
                    subgrid_table_id = subgrid_id + ""_t"";
                    jQuery(""#"" + subgrid_id).html(""<table id='"" + subgrid_table_id + ""' class='scroll'></table>"");
                    jQuery(""#"" + subgrid_table_id).jqGrid({
                        url: ""/Jugadores/ObtenerJu");
                WriteLiteral(@"gadores"",
                        datatype: 'json',
                        mtype: 'GET',
                        postData: { equipoID: function () { return row_id; } },
                        height: '100%',
                        colNames: ['ID', 'Equipo', 'Estado Civil', 'Instrucción', 'Profesión', 'Provincia', 'Parroquia', 'Cédula de identidad', 'Nombre', 'Apellido', 'Fecha de Nacimiento', 'Carnet', 'Fecha de Afiliacion', 'Activo', 'Aprobado' , 'Foto', ''],
                        colModel: [
                            { key: true, hidden: true, name: 'id', index: 'id', editable: true },
                            { name: 'equipoID', index: 'equipoID', hidden: true, editable: false, editrules: { edithidden: false }, edittype: 'select', formatter: 'select', editoptions: { value: getEquipos() } },
                            { name: 'estadoCivilID', index: 'estadoCivilID', hidden: true, editable: true, editrules: { edithidden: true }, edittype: 'select', formatter: 'select', editoptions: { value: getEst");
                WriteLiteral(@"adosCiviles(), disabled: true } },
                            { name: 'instruccionID', index: 'instruccionID', hidden: true, editable: true, editrules: { edithidden: true }, edittype: 'select', formatter: 'select', editoptions: { value: getInstrucciones(), disabled: true } },
                            { name: 'profesionID', index: 'profesionID', hidden: true, editable: true, editrules: { edithidden: true }, edittype: 'select', formatter: 'select', editoptions: { value: getProfesiones(), disabled: true } },
                            { name: 'provinciaID', index: 'provinciaID', hidden: true, editable: true, editrules: { edithidden: true }, edittype: 'select', formatter: 'select', editoptions: { value: getProvincias(), disabled: true } },
                            { name: 'parroquiaID', index: 'parroquiaID', hidden: true, editable: true, editrules: { edithidden: true }, edittype: 'select', formatter: 'select', editoptions: { value: getParroquias(), disabled: true } },
                            { name: '");
                WriteLiteral(@"cedula', index: 'cedula', editable: true, editoptions: { readonly: ""readonly""} },
                            { name: 'nombre', index: 'nombre', editable: true, editoptions: { readonly: ""readonly""} },
                            { name: 'apellido', index: 'apellido', editable: true, editoptions: { readonly: ""readonly"" } },
                            { name: 'fechaNacimiento', index: 'fechaNacimiento', align: 'center', editable: true, sorttype: ""date"", unformat: pickDate, formatter: dateTable, editoptions: { readonly: ""readonly"" } },
                            { name: 'carnet', index: 'carnet', align: 'center', editable: true, editoptions: { readonly: ""readonly"" } },
                            { name: 'fechaAfiliacion', index: 'fechaAfiliacion', align: 'center', editable: true, sorttype: ""date"", unformat: pickDate, formatter: dateTable, editoptions: { readonly: ""readonly"" } },
                            { name: 'estado', index: 'estado', hidden: true, editable: true, editrules: { edithidden: false }, hided");
                WriteLiteral(@"lg: true, edittype: ""checkbox"", align: 'center', formatter: checkTable, editoptions: { readonly: ""readonly"" } },
                            { name: 'calificado', index: 'calificado', width: 70, editable: false, edittype: ""checkbox"", align: 'center', formatter: checkTable},
                            { name: 'foto', index: 'foto', align: 'center', editable: true, edittype: ""custom"", editoptions: { enctype: 'multipart/form-data', custom_element: imageFormatEdit }, formatter: imageFormat, unformat: imageUnFormat},
                            {
                                name: 'myac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
                                formatoptions: {
                                    key: true,
                                    editbutton: true,
                                    delbutton: false,
                                    editformbutton: true,
                                    editOptions: {
                                      ");
                WriteLiteral(@"  url: ""/Jugadores/Calificar"",
                                        mtype: 'PUT',
                                        modal: true,
                                        recreateForm: true,
                                        closeAfterEdit: true,
                                        reloadAfterSubmit: true,
                                        beforeShowForm: function (form) {
                                            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class=""widget-header"" />');
                                            var dlgDiv = form.closest('.ui-jqdialog');
                                            dlgDiv[0].style.top = ""0px"";
                                            dlgDiv[0].style.left = Math.round((screen.width / 2) - (dlgDiv.width() / 2)) + ""px"";
                                            dlgDiv.find("".ui-jqdialog-title"").html(""Calificación"");
                                            dlgDiv.find(""#sData"").text('Aprobar');
      ");
                WriteLiteral(@"                                  },
                                        errorTextFormat: FormatedorMensajesError,
                                    }
                                }
                            },
                        ],
                        rowNum: 30,
                        viewrecords: true,
                        shrinkToFit: true,
                        autowidth: true,
                        altRows: true,
                        sortname: 'num',
                        sortorder: ""asc""
                    }).setGridWidth($(grid_selector).width() * .98);
                }

            });

            $(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size

            $(document).one('ajaxloadstart.page', function (e) {
                $.jgrid.gridDestroy(grid_selector);
                $('.ui-jqdialog').remove();
            });
        });

        function getDirigentes() {
            return listaDirigentes;
        ");
                WriteLiteral(@"}

        function getLigas() {
            return listaLigas;
        }

        function getEquipos() {
            return listaEquipos;
        }

        function getEstadosCiviles() {
            return listaEstadosCiviles;
        }

        function getInstrucciones() {
            return listaInstrucciones;
        }

        function getProfesiones() {
            return listaProfesiones;
        }

        function getProvincias() {
            return listaProvincias;
        }

        function getParroquias() {
            return listaParroquias;
        }
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
