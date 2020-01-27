// simple popup message
function popupMessage(id) {
  var ele = document.getElementById(id);
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

// testing out modal data
$('#deleteModal').on('show.bs.modal', function (e) {
  var modelData = $(e.relatedTarget).data('id');
  var form = $('#deleteOptionHidden');

  form.attr('value', modelData)
})
