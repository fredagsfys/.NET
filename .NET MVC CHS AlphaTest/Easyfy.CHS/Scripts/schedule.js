$(document).on("focus", ".datepicker", function (e) {
  $(this).datepicker({ "format": "yyyy-mm-dd", "weekStart": 1, "autoclose": true });
});

$(document).on("focus", "#WodName", function (e) {
  $("#WodName").autocomplete({
    source: function (request, response) {
      $.ajax({
        url: "Affiliate/FindDistinctWod",
        type: "POST",
        dataType: "json",
        data: { term: request.term },
        success: function(data) {
          response($.map(data, function(item) {
            console.log(data);
            return { value: item };
          }));
        }
      });
    },
    minLength: 1,
    focus: function (event, ui) {
      $(this).val(ui.item.label);
      return false;
    },
    select: function (event, ui) {
      $(this).val(ui.item.label);

      addWod(ui.item.label);
      return false;
    }
  });
});

function autoCompleteAddWod(val) {
  $.ajax({
    url: "/Affiliate/AddedWod?wodName=" + val,
    cache: false,
    success: function (html) {

    }
  });
  return false;
}

function autoCompleteAddWod(selector) {
  var count = 0;
  var item = $(selector);
  if (item.data("autocomplete-er-event") == undefined) {
    item.autocomplete({
      source: function (request, response) {
        $.ajax({
          url: "Affiliate/FindDistinctWod",
          dataType: "json",
          data: { q: request.term, limit: 15 },
          success: function (data) {
            response($.map(data, function (item) {
              return {
                label: item.name + " (" + item.wodType + ")",
                value: item.wodId
              }
            }))
          }
        })
      },
      minLength: 2,
      // Choose the first result, as in Google's search
      change: function (event, ui) {
        count++;
        if (count == 1) {
          $(".ui-menu-item").children().first().trigger("click");
          return false;
        }
      },
      focus: function (event, ui) {
        $(this).val(ui.item.label);
        return false;
      },
      select: function (event, ui) {
        count++;
        $(this).val(ui.item.label);
        addWod(ui.item.value, $(this).parent().parent());
        return false;
      }
    });
  }
}

function addWod(val, $owner) {
  $.ajax({
    url: "Affiliate/AddWod?wodId=" + val,
    cache: false,
    success: function (html) {
      $owner.append(html);
      $('.autocompleteinput').remove();
      $owner.append("<div class='showWodSearch btn'><span class='button_submit_Text'>Add wod</span></div>");
    }
  });
  return false;
}
function closeInput(isEsc) {
  if ($('.showWodSearch').length < 1 && isEsc && $(".autocompleteinput input").is(":focus")) {
    $('.autocompleteinput').remove();
    $("#WODRounds").append("<div class='showWodSearch btn'><span class='button_submit_Text'>Add wod</span></div>");
  }

  // If the input is empty
  if (!$('.autocompleteinput input').val()) {
    $('.autocompleteinput').remove();

    // If the button doesnt exist
    if ($('.showWodSearch').length < 1) {
      $(".scheduleWod").append("<div class='showWodSearch btn'><span class='button_submit_Text'>Add wod</span></div>");
    }
  }
};

$().ready(function() {
  $(".showWodSearch").live("click", function() {
    $(this).after("<div class='autocompleteinput'><span class='label'>Search wod:</span>  <input class='autocompleteAddWod' /></div>");
    $('.showWodSearch').remove();
    $('.autocompleteAddWod').focus();

    autoCompleteAddWod('.autocompleteAddWod');

    if ($('.autocompleteinput input').length > 0) {

      $(document).bind('keydown', function(e) {
        if (e.which == 27) {
          closeInput(true);
        }
      });

      $('.autocompleteinput input').blur(function() {
        if (!$('.autocompleteinput input').val()) {
          closeInput(false);
        }
      });

      $("html, body").click(function() {
        closeInput(false);
      });
    }
  });
});