
(function (window, undefined) {
    'use strict';

    /*
    NOTE:
    ------
    PLACE HERE YOUR OWN JAVASCRIPT CODE IF NEEDED
    WE WILL RELEASE FUTURE UPDATES SO IN ORDER TO NOT OVERWRITE YOUR JAVASCRIPT CODE PLEASE CONSIDER WRITING YOUR SCRIPT HERE.  */

    // console.warn('Hello World!');



})(window);

window.JqTrigger = triggerName => $(document).trigger(triggerName)

window.JqTriggerEx = function (triggerName, interval) {

    setTimeout(() => {
        $(document).trigger(triggerName)
    }, interval)
}

//   window.UpdateFeatherIcons = function () {
//     setTimeout(() => {
//       feather.replace({
//         width: 14,
//         height: 14
//       });
//     }, 10);
//   }

window.BeauityJsonInSelector = function (selector, value) {
    setTimeout(() => {
        $div = $(selector)
        $div.html(beauityJSON(value || $div.html()))
    }, 10);
}

function beauityJSON(data) {
    if (!data) return '';
    var z;
    if (typeof data === 'string') {
        // json = JSON.stringify(json, undefined, 2);
        if ('[object Object]' === data) return 'is String:: ' + data;
        // throw new Error('beauityJSON: data is damaged');

        data = JSON.parse(data);
    }
    z = syntaxHighlight(data);
    return $('<pre>' + z + '</pre>');
}


function syntaxHighlight(json) {
    if (typeof json != 'string') {
        json = JSON.stringify(json, undefined, 2);
    }
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}



var map;
var markers = DG.featureGroup();


// https://api.2gis.ru/doc/maps/en/examples/markers/
function gis2_init(location, zoom = 13) {

    setTimeout(() => {
        //console.warn("map:gis2_init", location);
        map = DG.map("map", {
            center: location.split(","),
            zoom: zoom,
        });

        map.on('click', function (e) {
            //clickedElement.innerHTML = 'map, coorinates ' + e.latlng.lat + ', ' + e.latlng.lng;
            //console.warn(e)

            let x = {
                containerPoint: {
                    x: e.containerPoint.x,
                    y: e.containerPoint.y,
                },
                layerPoint: {
                    x: e.layerPoint.x,
                    y: e.layerPoint.y,
                },
                latlng: {
                    lat: e.latlng.lat,
                    lng: e.latlng.lng,
                },
                type: e.type
            }

            DotNet.invokeMethodAsync('AppFront', 'JS_CallMapClick', x)
                .then(data => {
                    //console.log(data);
                });

        });

    }, 10)
}

/**
 * 
 * @param {Array<{location:string, id:string, title:string, desc:string}>} locations
 */
function gis2_setCouriers(locations) {

    //console.warn("map:gis2_setCouriers", locations);

    //console.warn("LL",locations);

    setTimeout(() => {
        locations.forEach(e => {

            var marker = DG.marker([
                e.location.split(",")[0],
                e.location.split(",")[1],
            ])
                .addTo(markers)
                .bindPopup(
                    e.title +
                    "</br>" +
                    e.desc +
                    "</br>"
                    //+
                    //'<a href="/courier-info/' +
                    //e.id +
                    //'">Подробнее</a></br>'
                );
            markers.addTo(map);
            //markers.push(marker);
        });
    }, 10)
}



function gis2_removeAll() {
    //console.warn("removeAll", map)
    //map.markers.removeAll();
    //markers.forEach(m => m.remove())
    markers.removeFrom(map)
    markers = DG.featureGroup()
}

class DG_gis2_event {
    latlng = ''
    layerPoint = ''
    containerPoint = ''
    originalEvent = ''
}

class DG_gis2_pup_event {
    id = ''
}
/**
 * @param {DG_gis2_pup_event} _DG_gis2_pup_event
 */
function gis2_internal_OnClickPup(_DG_gis2_pup_event) {
    //console.warn("gis2_internal_OnClickPup");
    DotNet.invokeMethodAsync('AppFront', 'JS_CallMapPupClick', _DG_gis2_pup_event)
        .then(data => {
            //console.log(data);
        });
}


function gis2_fitBounds() {
    //Multiple markers display and adjustment of boundaries
    //https://api.2gis.ru/doc/maps/en/examples/markers/

    //let _markers = DG.featureGroup()
    //for (let m of markers) {
    //    m.addTo(_markers)
    //}

    setTimeout(() => {
        map.fitBounds(markers.getBounds());
    },10)

}


$(document).on('load2', function () {
    //$(".editor1").
    CKEDITOR.replace('editor1');
})

$(document).on('trigger-slider1', function () {
    //var item = document.querySelector('.d-horizontal-scroll')

    //item.addEventListener("wheel", function (e) {
    //    if (e.deltaY > 0) item.scrollLeft += 100;
    //    else item.scrollLeft -= 100;
    //});
    $('.slider.multiple-items').slick({
        infinite: true,
        //slidesToShow: 3,
        variableWidth: true,
        slidesToScroll: 3,
        arrows: false,
    });
});

function CallPringPageReport1() {
    let pid = "report-print-page1";

    //let mywindow = window.open('', 'PRINT', 'height=650,width=900,top=100,left=150');

    //mywindow.document.write(`<html><head><title>Печать заявки</title>`);
    //mywindow.document.write('</head><body >');
    //mywindow.document.write(document.getElementById(pid).innerHTML);
    //mywindow.document.write('</body></html>');

    //mywindow.document.close(); // necessary for IE >= 10
    //mywindow.focus(); // necessary for IE >= 10*/

    //mywindow.print();
    //mywindow.close();

    window.print();

    return true;
}