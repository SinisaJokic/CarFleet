

function initializeMap(la , lo) {
    require([
        "esri/config",
        "esri/WebMap",
        "esri/views/MapView",
        "esri/widgets/Legend",
        "esri/widgets/ScaleBar",
        "esri/layers/GraphicsLayer",
        "esri/widgets/Sketch",
        "esri/widgets/CoordinateConversion",
        "esri/Graphic"
    ], function (esriConfig, WebMap, MapView, Legend, ScaleBar, GraphicsLayer, Sketch, CoordinateConversion, Graphic) {

        //esriConfig.apiKey = "AAPK924860fb0595471ab65e162073f66172YlO5GbY3ylG44q3D5scF_0nqM3tduETXZUQnuZVlhKmm2wdD0umc-yHBjgueQKO0";
        var webmap = new WebMap({
            //portalItem: {
            //    //add your map id
            //    //id: "**Omitted**",
            //    //layers: [graphicsLayer]
            //    basemap:"dark-gray"
            //}
            basemap: "topo-vector"
        });

        var view = new MapView({
            container: "viewDiv",
            map: webmap,
            zoom: 15,
            center: [16.459, 43.515]
        });

        var legend = new Legend({
            view: view
        });

        view.ui.add(legend, "top-right");

        var scalebar = new ScaleBar({
            view: view
        });

        view.ui.add(scalebar, "bottom-left");

        var graphicsLayer = new GraphicsLayer();
        webmap.add(graphicsLayer);

        const point = { //Create a point
            type: "point",
            longitude: lo,
            latitude: la
        };
        const simpleMarkerSymbol = {
            type: "simple-marker",
            color: [226, 119, 40],  // Orange
            outline: {
                color: [255, 255, 255], // White
                width: 1
            }
        };

        const pointGraphic = new Graphic({
            geometry: point,
            symbol: simpleMarkerSymbol
        });
        graphicsLayer.add(pointGraphic);

        var sketch = new Sketch({
            view: view,
            layer: graphicsLayer
        });

        view.ui.add(sketch, "top-right");

        var coordsWidget = document.createElement("div");
        coordsWidget.id = "coordsWidget";
        coordsWidget.className = "esri-widget esri-component";
        coordsWidget.style.padding = "7px 15px 5px";

        view.ui.add(coordsWidget, "bottom-right");

        function showCoordinates(pt) {
            var coords = "Lat/Lon " + pt.latitude.toFixed(3) + " " + pt.longitude.toFixed(3) +
                " | Scale 1:" + Math.round(view.scale * 1) / 1 +
                " | Zoom " + view.zoom;
            coordsWidget.innerHTML = coords;
        }

        view.watch("stationary", function (isStationary) {
            showCoordinates(view.center);
        });

        view.on("pointer-move", function (evt) {
            showCoordinates(view.toMap({ x: evt.x, y: evt.y }));
        });

        var coordinateConversionWidget = new CoordinateConversion({
            view: view
        });

        view.ui.add(coordinateConversionWidget, "bottom-right");

    });
}