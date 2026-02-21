// JavaScript to handle location dropdown selection on Home page
document.addEventListener('DOMContentLoaded', function() {
    // Home page location dropdown handling
    const locationItems = document.querySelectorAll('.option-list-destination li[data-location-id]');
    const locationIdInput = document.getElementById('selectedLocationId');
    const displayInput = document.querySelector('.destination-dropdown input[type="text"]');
    const displayDestination = document.querySelector('.destination-dropdown .destination');

    locationItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            const locationId = this.getAttribute('data-location-id');
            const locationName = this.getAttribute('data-location-name');
            
            // Update hidden input
            if (locationIdInput) {
                locationIdInput.value = locationId;
            }
            
            // Update display
            if (displayInput) {
                displayInput.value = locationName;
            }
            
            if (displayDestination) {
                const h6 = displayDestination.querySelector('h6');
                const span = displayDestination.querySelector('span');
                if (h6) h6.textContent = locationName;
                if (span) span.textContent = 'Selected Location';
            }
            
            // Close dropdown
            const dropdown = document.querySelector('.custom-select-wrap.three');
            if (dropdown) {
                dropdown.style.display = 'none';
            }
        });
    });

    // Hotel page location dropdown handling
    const hotelLocationItems = document.querySelectorAll('#hotel-page .option-list-destination li[data-location-id]');
    const hotelLocationIdInput = document.querySelector('input[name="locationId"]');
    
    hotelLocationItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            const locationId = this.getAttribute('data-location-id');
            const locationName = this.getAttribute('data-location-name');
            
            // Update hidden input
            if (hotelLocationIdInput) {
                hotelLocationIdInput.value = locationId;
            }
            
            // Update display
            const displayInput = document.querySelector('#hotel-page .destination-dropdown input[type="text"]');
            if (displayInput) {
                displayInput.value = locationName;
            }
            
            const displayDestination = document.querySelector('#hotel-page .destination-dropdown .destination');
            if (displayDestination) {
                const h6 = displayDestination.querySelector('h6');
                const span = displayDestination.querySelector('span');
                if (h6) h6.textContent = locationName;
                if (span) span.textContent = 'Selected Location';
            }
            
            // Close dropdown
            const dropdown = document.querySelector('#hotel-page .custom-select-wrap.three');
            if (dropdown) {
                dropdown.style.display = 'none';
            }

            // Automatically submit form to refresh results
            const form = document.querySelector('#hotel-page form');
            if (form) {
                form.submit();
            }
        });
    });
});