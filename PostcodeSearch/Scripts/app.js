"use strict";

$(document).ready(function () {
    var mapProp = {
        center: new google.maps.LatLng(51.508742, -0.120850),
        zoom: 5,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    var markers = [];

    $('button[data-load]').on('click', function (event) {
        function getLatLngFromPostcode(postcode) {
            return $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?address=' + postcode, null);
        }

        function setOriginalMarker(parameters) {
            getLatLngFromPostcode(parameters.postcode)
                .success(function (data) {
                    var position = data.results[0].geometry.location
                    var latlng = new google.maps.LatLng(position.lat, position.lng);

                    markers.push(new google.maps.Marker({
                        position: latlng,
                        map: map
                    }));

                    map.panTo(latlng);
                    map.setZoom(15);
                });
        }

        function addMarker(parameters) {
            getLatLngFromPostcode(parameters.Postcode).success(function (data) {
                var position = data.results[0].geometry.location
                var latlng = new google.maps.LatLng(position.lat, position.lng);

                markers.push(new google.maps.Marker({
                    position: latlng,
                    map: map
                }));
            })
        }

        function clearMakers() {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
        }

        function fetchNearbyAddresses(parameters) {
            $.get('/api/Postcodes', parameters)
                .success(function (result) {
                    $('#resultTable tbody tr').remove();

                    var table = $('#resultTable tbody');

                    for (var item in result) {
                        var record = result[item];
                        table.append("<tr>"
                            + "<td>" + record.Postcode + "</td>"
                            + "<td>" + record.Thoroughfare + ", " + record.Posttown + "</td>"
                        );

                        addMarker(record);
                    }
                });
        };

        event.preventDefault();

        clearMakers();

        var parameters = {
            postcode: $('#postcode').val(),
            distance: $('#distance').val()
        };

        setOriginalMarker(parameters);
        fetchNearbyAddresses(parameters);
    });
});
