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
function searchAutoComplete(elementId, apiUri) {
    var input = document.querySelector(`#${elementId}`);
    if (input != null) {
        input.addEventListener("input", function (e) {
            var testEle = document.createElement("div");
            testEle.setAttribute("id", this.id + "autocomplete-list");
            testEle.setAttribute("class", "autocomplete-items");
            this.parentNode.appendChild(testEle);
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