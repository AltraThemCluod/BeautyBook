﻿// Prepare location info object.
var locationInfo = {
    geo: null,
    country: null,
    state: null,
    city: null,
    postalCode: null,
    street: null,
    streetNumber: null,
    reset: function () {
        this.geo = null;
        this.country = null;
        this.state = null;
        this.city = null;
        this.postalCode = null;
        this.street = null;
        this.streetNumber = null;
    }
};

googleAutocomplete = {
    autocompleteField: function (fieldId) {
        (autocomplete = new google.maps.places.Autocomplete(
            document.getElementById(fieldId)
        )),
            { types: ["geocode"] };
        google.maps.event.addListener(autocomplete, "place_changed", function () {
            // Segment results into usable parts.4r32
            var place = autocomplete.getPlace(),
                address = place.address_components,
                lat = place.geometry.location.lat(),
                lng = place.geometry.location.lng();

            // Reset location object.
            locationInfo.reset();

            // Save the individual address components.
            locationInfo.geo = [lat, lng];
            for (var i = 0; i < address.length; i++) {
                var component = address[i].types[0];
                switch (component) {
                    case "country":
                        locationInfo.country = address[i]["long_name"];
                        break;
                    case "administrative_area_level_1":
                        locationInfo.state = address[i]["long_name"];
                        break;
                    case "locality":
                        locationInfo.city = address[i]["long_name"];
                        break;
                    case "postal_code":
                        locationInfo.postalCode = address[i]["long_name"];
                        break;
                    case "route":
                        locationInfo.street = address[i]["long_name"];
                        break;
                    case "street_number":
                        locationInfo.streetNumber = address[i]["long_name"];
                        break;
                    default:
                        break;
                }
            }

            //lat lng value get input filed

            $('#VisitorLatitude').val(lat);
            $('#VisitorLongitude').val(lng);
        });
    }
};