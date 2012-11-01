var geocoder;
var map;
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(57.754007, 11.990753);
    var mapOptions = {
        zoom: 13,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    codeAddress();
}

function codeAddress() {
    var address = document.getElementById("address").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
            });
        console.log();
        var infowindow = new google.maps.InfoWindow({
           content: $('#address').val(),
        });
        google.maps.event.addListener(marker, 'click', function() {
          infowindow.open(map,marker);
        });
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }

    });
}