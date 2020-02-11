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
function partsAutoComplete(inputElementId) {
    var input = document.getElementById(inputElementId);
    if (input != null) {
        input.addEventListener("input", updateListFromApi)
    }
    async function updateListFromApi() {
        closeAllLists();

        if (input == null || input.value.trim().length < 3) {
            return false;
        }
        // create container for items to be displayed
        var list = document.createElement("ul");
        list.setAttribute("class", "autocomplete-list");
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
                    // Create li and append it to the ul
                    var liEle = document.createElement("li");
                    liEle.setAttribute("class", "list-item")
                    list.appendChild(liEle);
                    // create spans and append them to the li
                    var partNumSpan = document.createElement("span");
                    partNumSpan.setAttribute("class", "list-item-header")
                    partNumSpan.innerHTML = `Part Number: ${json[i]["id"]} <br>`;
                    var descSpan = document.createElement("span");
                    descSpan.setAttribute("class", "list-item-desc")
                    descSpan.innerText = `${json[i]["name"]}`;
                    liEle.appendChild(partNumSpan);
                    liEle.appendChild(descSpan);
                    
                    liEle.addEventListener("click", function (e) {
                        input.value = json[i]["id"];
                        closeAllLists();
                    })
                }
            });
    }
    function closeAllLists() {
        var lists = document.getElementsByClassName("autocomplete-list");
        for (let i = 0; i < lists.length; i++) {
            lists[i].parentNode.removeChild(lists[i]);
        };
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