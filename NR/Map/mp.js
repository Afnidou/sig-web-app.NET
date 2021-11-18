
var mapMain;
var widgetEditor;
// @formatter:off
require([
    "esri/map",
    "esri/dijit/Search",
    "dojo/ready",
    "dojo/parser",
    "dojo/on",
    "dojo/_base/array",
    "dijit/layout/BorderContainer",
    "dijit/layout/ContentPane", "esri/layers/FeatureLayer", "esri/config", "esri/dijit/editing/Editor",
    "esri/dijit/editing/TemplatePicker", "esri/dijit/Legend", "esri/dijit/BasemapGallery",
    "esri/layers/ArcGISDynamicMapServiceLayer", "esri/dijit/Print", "dijit/form/Button", "dojo/dom", "dojo/dom-construct"
    , "esri/dijit/Measurement",
    "dojo/dom",
    "esri/units",
    "esri/dijit/Print",
    "esri/toolbars/draw",
    "esri/graphic",
    "esri/tasks/query",
    "esri/symbols/SimpleFillSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/symbols/SimpleMarkerSymbol",
    "dojo/ready",
    "dojo/parser",
    "dojo/on",
    "dojo/dom",
    "dojo/store/Memory",
    "dojo/date/locale",
    "dojo/_base/Color",
    "dojo/_base/declare",
    "dojo/_base/array",
    "dgrid/OnDemandGrid",
    "dgrid/Selection",
    "dijit/layout/BorderContainer",
    "dijit/layout/ContentPane",
    "dijit/form/Button",
    "esri/toolbars/draw",
    "esri/graphic",
    "esri/tasks/query",
    "dojo/_base/array",
    "dijit/layout/BorderContainer",
    "dijit/layout/ContentPane", "esri/layers/FeatureLayer", "esri/config", "esri/dijit/editing/Editor", "esri/tasks/GeometryService", "esri/dijit/editing/TemplatePicker",
    "esri/tasks/GeometryService",
    "esri/layers/ArcGISTiledMapServiceLayer",
    "esri/layers/FeatureLayer",
    "esri/Color",
    "esri/symbols/SimpleMarkerSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/dijit/editing/Editor",
    "esri/dijit/editing/TemplatePicker",
    "esri/config",
    "dojo/i18n!esri/nls/jsapi",
    "dojo/_base/array", "dojo/parser", "dojo/keys",
    "dijit/layout/BorderContainer", "dijit/layout/ContentPane",
    "dojo/domReady!",
    "esri/tasks/Geoprocessor",
    "esri/dijit/editing/AttachmentEditor",
    "esri/dijit/InfoWindow"
], function (Map, Search,
    ready, parser, on, array,
    BorderContainer, ContentPane, FeatureLayer,
    config, Editor, TemplatePicker, Legend, BasemapGallery, ArcGISDynamicMapServiceLayer, Print, Button, dom, domConstruct, Measurement, dom, Units, Print, Draw, Graphic, Query, SimpleFillSymbol, SimpleLineSymbol, SimpleMarkerSymbol,
    ready, parser, on, dom,
    Memory, locale,
    Color, declare, array,
    Grid, Selection,
    BorderContainer, ContentPane, Button, Draw, Graphic, Query, array,
    BorderContainer, ContentPane, FeatureLayer, config, Editor, GeometryService, TemplatePicker, GeometryService,
    ArcGISTiledMapServiceLayer, FeatureLayer,
    Color, SimpleMarkerSymbol, SimpleLineSymbol,
    Editor, TemplatePicker,
    esriConfig, jsapiBundle,
    arrayUtils, parser, keys, Geoprocessor, AttachmentEditor, InfoWindow) {

    ready(function () {

        // Parse DOM nodes decorated with the data-dojo-type attribute
        parser.parse();

        /*
         * Step: Specify the proxy Url
         */
        config.defaults.io.proxyUrl = "http://localhost/proxy/proxy.ashx";

        // Create the map
        mapMain = new Map("divMap", {
            basemap: "topo",
            center: [-7.62, 33.59],
            zoom: 11,
            slider: true
        });
       
        var CASA = new ArcGISDynamicMapServiceLayer("http://localhost:6080/arcgis/rest/services/PA/MapServer");
        mapMain.addLayers([CASA]);

        var c = new FeatureLayer("http://localhost:6080/arcgis/rest/services/NR1/FeatureServer/0", {
            outFields: ['NR_Id', 'Reference_', 'Prenom', 'Societe'],
            hasAttachments: true,
            isEditable: true,

        });

        mapMain.on("layers-add-result", function () {
            var dijitLegend = new Legend({
                map: mapMain,
                arrangement: Legend.ALIGN_LEFT
            }, "divLegend");
            dijitLegend.startup();
        });
        

        mapMain.on("layers-add-result", initEditor);

        mapMain.addLayers([c]);

        function initEditor(results) {

            // Map the event results into an array of layerInfo objects
            var layerInfosWildfire = array.map(results.layers, function (result) {
                return {
                    featureLayer: result.layer
                };
            });


            /*
             * Step: Map the event results into an array of Layer objects
             */

            var layersWildfire = array.map(results.layers, function (result) {
                return result.layer;
            });

            var tpCustom = new TemplatePicker({
                featureLayers: layersWildfire,
                columns: 2
            }, 'divLeft');

            tpCustom.startup();
            var editorSettings =
            {
                map: mapMain,
                geometryService: "http://sampleserver6.arcgisonline.com/arcgis/rest/services/Utilities/Geometry/GeometryServer",
                layerInfos: layerInfosWildfire,
                toolbarVisible: true,
                templatePicker: tpCustom,
                createOptions: {
                    polygonDrawTools: [Editor.CREATE_TOOL_FREEHAND_POLYGON,
                    Editor.CREATE_TOOL_RECTANGLE, Editor.CREATE_TOOL_TRIANGLE,
                    Editor.CREATE_TOOL_CIRCLE]
                },
                toolbarOptions: {
                    reshapeVisible: true
                },
                enableUndoRedo: true
            }
            var editorParams = {
                settings: editorSettings
            };
            var widgetEditor = new Editor(editorParams, 'divTop');
            widgetEditor.startup();
        };



        var basemapGallery = new BasemapGallery({
            showArcGISBasemaps: true,
            map: mapMain
        }, "basemapGallery");
        basemapGallery.startup();



        var s = new Search({
            map: mapMain
        }, "divSearch");
        s.startup();


        var measurement = new Measurement({
            map: mapMain,
            defaultAreaUnit: Units.SQUARE_MILES,
            defaultLengthUnit: Units.KILOMETERS
        }, "measurement");
        measurement.startup();


        var printer = new Print({
            map: mapMain,
            url: "http://sampleserver6.arcgisonline.com/arcgis/rest/services/Utilities/PrintingTools/GPServer/Export%20Web%20Map%20Task"
        }, "printButton");
        printer.startup();

    });
});

