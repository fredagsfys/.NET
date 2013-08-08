$().ready(function () {
  $("#affiliateTerm").keyup(function () {
    refreshAffiliateData($("#affiliateTerm").val());
  });
});

$(document).on("click", ".affiliateCheckboxFilter", function (e) {
  refreshAffiliateData($("#affiliateTerm").val());
});

function refreshAffiliateData(searchTerm) {
  var checked = [];
  $('.affiliateCheckboxFilter' + ':checked').each(function () {
    checked.push($(this).attr("data-key") + ":" + $(this).val());
  });
  console.log(searchTerm);
  $.ajax({
    url: "/Affiliate/SearchAffiliate?affiliateTerm=" + searchTerm + "&checkboxValue=" + checked,
    success: function (data) {
      $("#affiliateSearchResults").html(data);
    }
  });
}