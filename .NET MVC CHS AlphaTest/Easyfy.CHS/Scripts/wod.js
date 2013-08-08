var prefix = "";

function autoCompleteExercise(selector) {
  var count = 0;
  var item = $(selector);
  if (item.data("autocomplete-er-event") == undefined) {
    item.autocomplete({
      source: function (request, response) {
        $.ajax({
          url: "/WOD/AllExercises",
          dataType: "json",
          data: { q: request.term, limit: 15 },
          success: function (data) {
            response($.map(data, function (item) {
              //var userText = $(".autocompleteinput input").val();
              //var autoCompleteText = item.name;
              //var autofillText = autoCompleteText.substring(userText.length);

              return {
                label: item.Name + " (" + item.ExerciseType + ")",
                value: item.ExerciseId
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
        createRound(ui.item.value, $(this).parent().parent());
        return false;
      }
    });
  }
}
function attachAutoCompleteEREvents(selector) {
  var count = 0;
  var item = $(selector);
  // check if event already is attached
  if (item.data("autocomplete-er-event") == undefined) {
    item.autocomplete({
      source: function (request, response) {
        $.ajax({
          url: "/WOD/AllExercises",
          dataType: "json",
          data: { q: request.term, limit: 15 },
          success: function (data) {
            response($.map(data, function (item) {
              return {
                label: item.Name + " (" + item.ExerciseType + ")",
                value: item.ExerciseId
              }
            }))
          }
        })
      },
      change: function (event, ui) {
        count++;
        if (count == 1) {
          $(".ui-menu-item").children().first().trigger("click");
        }
        return false;
      },
      focus: function (event, ui) {
        $(this).val(ui.item.label);
        return false;
      },
      select: function (event, ui) {
        count++;
        $(this).val(ui.item.label);
        createExcersiceRound(ui.item.value, $(this).parent().parent());
        return false;
      }
    });


  }
}

function checkPressedKey(input) {
  $(input).live("keypress", function (e) {
    var keyCode = e.keyCode || e.which;

    // Only trigger click if tab key is clicked
    if (keyCode == 9) {
      e.preventDefault();
      return true;
    } else {
      return false;
    }
  });
}

function createRound(val, $owner) {
  $.ajax({
    url: "/WOD/CreateRound?exerciseId=" + val,
    cache: false,
    success: function (html) {
      $owner.append(html); $('.autocompleteinput').remove();
      $owner.append("<div class='showRoundSearch btn'><span class='button_submit_Text'>New round</span></div>");
      $('html, body').animate({
        scrollTop: $(".ui-sortable .wodbase_round:last").offset().top
      }, 500);
    }
  });
  return false;
}

function createExcersiceRound(val, $owner) {
  //console.log($owner.find(".autocompleteExerciseRound").attr('value'));
  $.ajax({
    url: "/WOD/CreateExerciseRound?exerciseId=" + val,
    data: { "containerPrefix": prefixBtn.attr("data-containerPrefix") },
    cache: false,
    success: function (html) {
      $owner.append(html);
      $('.autocompleteERinput').remove();
      $owner.append(prefixBtn);
    }
  });
}

$("a.deleteRound").live("click", function () {
  $(this).parent().remove();
  return false;
});
$("a.deleteExercise").live("click", function () {
  $(this).parents("div.exRound:first").remove();
  return false;
});

function attachSortableRoundEvents(selector) {

  $(".round").sortable({
    revert: true,
    items: '.exRound',
    handle: '.handler',
    placeholder: 'droparea'
  });
}



function onIsRepsMax(val) {
  showHideRepsInput($(val));
}


function showHideRepsInput($item) {
  $item.parent().nextAll(".RepsTxtBox").toggle();
}

function exerciseTime_changed(selector) {
  $(selector).parents("div.Time:first").find(".showTimeOption").toggle();
}

function onIsTimed(val) {
  showHideTimeInput($("#" + val));
}

function showHideTimeInput($item) {
  if ($item.val() == 'yes') {

    $(".dd_er_Time").show();
    return false;
  } else {

    $(".dd_er_Time").hide();
    return true;
  }
}

function reportWODResult(val) {
  $(val).parents("div.ScheduledWod:first").hide("slow");
}

function onExtraWeightChanged(val) {
  $(val).parents("div.Weight:first").find(".WeightOptions").toggle();
}

function showHideExtraWeightInput($item) {
  if ($item.val() == 'yes') {
    console.log("yes");
    $item.parent().nextAll(".dd_er_Weight").show();
    return false;
  } else {
    console.log("no");
    $item.parent().nextAll(".dd_er_Weight").hide();
    return true;
  }
}

function showHideExtraBodyWeightInput($item) {
  if ($item.val() == 'bodyweight') {
    $item.parent().nextAll(".dd_er_BodyWeight").show();
    return false;
  } else {
    $item.parent().nextAll(".dd_er_BodyWeight").hide();
    return true;
  }

}

function onWeightChooseChanged(val) {
  if ($(val)[0].value == "Fix") {
    $(val).parents("div.Weight:first").find(".WeightOptions").show();
  } else if ($(val)[0].value == "Max") {
    $(val).parents("div.Weight:first").find(".WeightOptions").hide();
  } else if ($(val)[0].value == "Bodyweight") {
    $(val).parents("div.Weight:first").find(".WeightOptions").hide();
    $(val).parents("div.Weight:first").find(".BodyWeight").show();
  }

  function onWeightChooseChanged($item) {
    if ($item.val() == 'Bodyweight') {
      $item.parent().$(".BodyWeight").show();
      return false;
    }
    else if ($item.val() == 'Max') {
      $item.parent().$(".BodyWeight").hide();
      $item.parent().$(".WeightOptions").hide();
      return true;
    }
  }

  function showHideWeightInput($item) {
    if ($item.val() == 'fix') {
      $item.parent().nextAll(".dd_er_Weight").show();
      return false;
    } else {
      $item.parent().nextAll(".dd_er_Weight").hide();
      return true;
    }
  }

  function deleteItem(val) {
    $("#" + val).remove();
  }
};

$().ready(function () {
  $(".showRoundSearch").live("click", function () {
    $(this).after("<div class='autocompleteinput'><span class='label'>Search exercise:</span>  <input class='autocompleteRound' /></div>");
    $('.showRoundSearch').remove();
    $('.autocompleteRound').focus();

    autoCompleteExercise('.autocompleteRound');

    if ($('.autocompleteinput input').length > 0) {

      $(document).bind('keydown', function (e) {
        if (e.which == 27) {
          closeInput(true);
        }
      });

      $('.autocompleteinput input').blur(function () {
        if (!$('.autocompleteinput input').val()) {
          closeInput(false);
        }
      });

      $("html, body").click(function () {
        closeInput(false);
      });
    }
  });

  $(".showExerciseRoundSearch").live("click", function () {
    prefixBtn = $(".showExerciseRoundSearch");


    $(this).parent().append("<div class='autocompleteERinput'><span class='label'>Search exercise:</span>  <input class='autocompleteExerciseRound' /></div>");
    prefixBtn.remove();
    $('.autocompleteExerciseRound').focus();

    attachAutoCompleteEREvents('.autocompleteExerciseRound');

    if ($('.autocompleteExerciseRound').length > 0) {

      $(document).bind('keydown', function (e) {
        if (e.which == 27) {
          closeRoundInput(prefixBtn, true);
        }
      });

      $('.autocompleteExerciseRound').blur(function () {
        if (!$('.autocompleteExerciseRound').val()) {
          closeRoundInput(prefixBtn, false);
        }
      });

      $("html, body").click(function () {
        closeRoundInput(prefixBtn, false);
      });
    }
  });

  function closeRoundInput(prefixBtn, isEsc) {
    if ($('.showExerciseRoundSearch').length < 1 && isEsc && $('.autocompleteExerciseRound').is(":focus")) {
      $('.autocompleteERinput').remove();
      $("#WODRoundExercise").append(prefixBtn);
    }

    if (!$('.autocompleteExerciseRound').val()) {
      $('.autocompleteERinput').remove();

      if ($('.showExerciseRoundSearch').length < 1) {
        $("#WODRoundExercise").append(prefixBtn);
      }
    }
  };

  function closeInput(isEsc) {
    if ($('.showRoundSearch').length < 1 && isEsc && $(".autocompleteinput input").is(":focus")) {
      $('.autocompleteinput').remove();
      $("#WODRounds").append("<div class='showRoundSearch btn'><span class='button_submit_Text'>New round</span></div>");
    }

    // If the input is empty
    if (!$('.autocompleteinput input').val()) {
      $('.autocompleteinput').remove();

      // If the button doesnt exist
      if ($('.showRoundSearch').length < 1) {
        $("#WODRounds").append("<div class='showRoundSearch btn'><span class='button_submit_Text'>New round</span></div>");
      }
    }
  };

$("#term").keyup(function () {  
    refreshData($("#term").val());
  });
});

$(document).on("click", ".wodCheckboxFilter", function(e) {
  refreshData($("#term").val());
});

function refreshData(searchTerm) {
  var checked = [];
  $('.wodCheckboxFilter' + ':checked').each(function () {
    checked.push($(this).attr("data-key")+":"+$(this).val());
  });
  
  console.log(searchTerm, checked);

  $.ajax({
    url: "/Wod/SearchWod?term=" + searchTerm + "&checkboxValue=" + checked,
    success: function (data) {
      $("#searchResults").html(data);
    }
  });
}