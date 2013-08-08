$().ready(function() {
  $("#athleteTerm").keyup(function () {
    athleteRefreshData(1);
  });
});

$(document).on("click", ".athleteCheckboxFilter", function (e) {
  athleteRefreshData(1);
});

$(document).on("change", ".athletePaging", function (e) {
  athleteRefreshData(1);
});

function athleteRefreshData(page) {

  searchTerm = $("#athleteTerm").val();

  var checked = [];
  $('.athleteCheckboxFilter' + ':checked').each(function () {
    checked.push($(this).attr("data-key") + ":" + $(this).val());
  });

  pageSize = $('input[name=athletePaging]:checked').val();

  $.ajax({
    url: "/Athlete/SearchAthlete?athleteTerm=" + searchTerm + "&athleteCheckbox=" + checked + "&page=" + page + "&pageSize=" + pageSize,
    success: function (data) {
      $("#athleteSearchResults").html(data);
    }
  });
}

//Infinite scrolling på results.
var wrapPage = 0;
var _inCallback = false;

function loadResults(url, appendSelector) {
    if (wrapPage > -1 && !_inCallback) {
        _inCallback = true;
        wrapPage++;
        $('#loadingResult').html('<img src="/Content/img/loading.gif"/>');
        $.get(url + wrapPage, function(data) {
            if (data != '') {
                $(appendSelector).append(data);
            } else {
                wrapPage = -1;
            }
            _inCallback = false;
            $('#loadingResult').empty();
        });
    }
    return wrapPage;
}

$(document).on("click", ".loadResults", function(e) {
    var wrapper = $(this).closest(".dynamic-load");
    var url = wrapper.attr("data-url");
    
    var appendSelector = wrapper.attr("data-append");
    wrapPage = loadResults(url, appendSelector);
    wrapper.attr("data-page", wrapPage);
    
});

//$(window).scroll(function() {
//  if ($(window).scrollTop() == $(document).height() - $(window).height()) {
//    loadResults();
//    event.preventDefault();
//  }
//})