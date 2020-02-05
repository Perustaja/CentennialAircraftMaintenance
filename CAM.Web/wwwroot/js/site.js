const partsSearchApiUri = `${window.location.origin}/api/parts/search`
// Popup message for modal
function popupMessage(id) {
    var ele = document.getElementById(id);
    ele.classList.toggle("show");
}

// Parts Delete modal 
$('#deleteModal').on('show.bs.modal', function (e) {
    var modelData = $(e.relatedTarget).data('id');
    var form = $('#deleteOptionHidden');

    form.attr('value', modelData)
})

// Search autocomplete for parts API
function partsAutoComplete(elementId) {
    var input = document.querySelector(`#${elementId}`);
    if (input != null) {
        input.addEventListener("input", async function (e) {
            // create container for items to be displayed
            var list = document.createElement("div");
            list.setAttribute("id", this.id + "autocomplete-list");
            list.setAttribute("class", "autocomplete-items");
            this.parentNode.appendChild(list);

            // setup url with query strings
            var params = new URLSearchParams({
                partNumber: `${input.value}`
            });
            var uriComplete = `${partsSearchApiUri}?${params.toString()}`;

            // make api call and add inputs
            await fetch(uriComplete)
                .then(async response => await response.json())
                .then(json => {
                    for (let i = 0; i < json.length; i++) {
                        value = document.createElement("div");
                        value.innerHTML = "<strong>" + json[i]["id"] + "</strong>";
                        value.addEventListener("click", function(e) {
                            input.value = json[i]["id"];
                            closeAllLists();
                        })
                        list.appendChild(value)
                    }
                });

            // create div for each object in list
        })
    }
}

// datepicker function 
$.fn.datepicker.defaults.todayBtn = "linked";
$.fn.datepicker.defaults.clearBtn = "linked";
$.fn.datepicker.defaults.todayHighlight = "true";
$.fn.datepicker.defaults.autoclose = "true";
$(function () {
    $('.datepicker').datepicker({

    });
});