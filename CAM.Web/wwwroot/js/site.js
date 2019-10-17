// datepicker function 
$.fn.datepicker.defaults.todayBtn = "linked";
$.fn.datepicker.defaults.clearBtn = "linked";
$.fn.datepicker.defaults.todayHighlight = "true";
$.fn.datepicker.defaults.autoclose = "true";
$(function () {
    $('.datepicker').datepicker({

    });
});
// table search function
$(document).ready(function() {
    (function($) {
        $("#mainTable tbody").addClass("search");
        $('#searchQuery').keyup(function() {
            var rex = new RegExp($(this).val(), 'i');
            $('.search tr ').hide();

            $('.search tr ').filter(function(i, v) {
                var $t = $(this).children(":eq(" + "1" + ")");
                return rex.test($t.text());
            }).show();
        })

    }(jQuery));

});
// table select dropdown filter function
function filterTable() {
    // Variables
    let dropdown, table, rows, cells, status, filter;
    dropdown = document.getElementById("selectFilter");  
    table = document.getElementById("mainTable");
    rows = table.getElementsByTagName("tr");
    filter = dropdown.value;
  
    // Loops through rows and hides those with countries that don't match the filter
    for (let row of rows) { // `for...of` loops through the NodeList
      cells = row.getElementsByTagName("td");
      status = cells[1] || null; // gets the 2nd `td` or nothing
      // if the filter is set to 'All', or this is the header row, or 2nd `td` text matches filter
      if (filter === "All" || !status || (filter === status.innerText)) {
        row.style.display = ""; // shows this row
      }
      else {
        row.style.display = "none"; // hides this row
      }
    }
  }