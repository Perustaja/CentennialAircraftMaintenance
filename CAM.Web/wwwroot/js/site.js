// part details popup
function partsPopupMessage() {
  var ele = document.getElementById("partPopup");
  ele.classList.toggle("show");
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
