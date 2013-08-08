function Forms() {

    /*** Ajax forms ***/

    // Get WOD/WODReportResult
    $(function () {
        $("div#scheduledWODs").on("click", ".editLink .reportWOD", function () {
            var caller = this;
            $(caller).button('loading');
            $(caller).parent().parent().parent().children(".reportForm").hide("slide", { direction: "left" }, 100);
            $.ajax({
                url: $(this).data('url'),
                type: 'GET',
                cache: false,
                success: function (result) {
                    $(caller).hide("slide", { direction: "right" }, 200, function () {
                        //$(caller).parent().parent().parent().children(".reportForm").css("border-top", "1px dashed #ff9900");
                        $(caller).button('reset');
                        $(caller).parent().parent().parent().children(".reportForm").html(result).fadeIn(100);
                    }); 
                }
            });
        });
    });

    // Get Athlete/Edit
    var profileSectionHeight = "";
    $("div#EditPartial").hide();
    $(function () {
        $("div#PersonalInfo").on("click", ".editLink #editAthlete", function () {
            var caller = this;
            $(caller).button('loading');
            $.ajax({
                url: $(this).data('url'),
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('div#PersonalInfo').hide("slide", { direction: "left" }, 100);
                    $('div#ProfileSection').animate({ width: 200, height: 678 }, 300, function () {
                        $('div#EditPartial').html(result).show("slide", { direction: "left" }, 200);
                        $(caller).button('reset');
                    });
                }
            });
        });
    });

    // Post Athlete/Edit
    $(function () {
        $("div#ProfileSection").on("click", "#EditPartial form #EditSubmit", function () {
            if (!$("#form0").valid()) {
                return false;
            }
            $.ajax({
                url: $(this).data('url'),
                type: 'POST',
                cache: false,
                data: $("#form0").serialize(),
                success: function (result) {
                    $('div#EditPartial').hide("slide", { direction: "left" }, 200);
                    $('div#ProfileSection').animate({ width: 170 }, 500, function () {
                        $('div#ProfileSection').css('height', 'auto');
                        $('div#PersonalInfo').html(result).show("slide", { direction: "left" }, 100);
                    });
                }
            });
        });
    });

    // Cancel Athlete/Edit
    $(function () {
        $("div#ProfileSection").on("click", "#EditPartial form .cancelButtons", function () {
            $('div#EditPartial').hide("slide", { direction: "left" }, 200);
            $('div#ProfileSection').animate({ width: 170 }, 500, function () {
                $('div#ProfileSection').css('height', 'auto');
                $('div#PersonalInfo').show("slide", { direction: "left" }, 100);
            });
            return false;
        });
    });

    // GET Affiliate/Create
    $(function () {
        $('div.createButtonSection').on("click", "#CreateSubmit", function () {
            var caller = this;
            $(caller).attr("disabled", "disabled");
            $(caller).button('loading');

            $.ajax({
                url: $(this).data('url'),
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('div#CreatePartial').html(result);

                    $("#CreateSubmit").animate({ backgroundColor: "#FF9900" }, 700);
                    $('div#CreatePartial').animate({ width: 100 + "%" }, {
                        queue: false,
                        duration: 450,
                        complete: function () {
                            $("#CreateAffiliateForm").animate({ paddingTop: 10 });
                            $("div#CreatePartial").animate({ height: 400 }, {
                                queue: false,
                                duration: 300,
                                complete: function () {
                                    $(caller).removeAttr('disabled');
                                    $(caller).button("reset");
                                    $(caller).text("Cancel").attr("id", "CACancel");
                                }
                            })
                        }
                    })
                }
            });
        });
    });

    // Affiliate/Cancel
    $('div.createButtonSection').on("click", "#CACancel", function () {
        var caller = this;
        $(caller).attr("disabled", "disabled");
        $("#CACancel").animate({ backgroundColor: "rgb(43, 43, 43)" }, 700);
        $("#CreateAffiliateForm").animate({ paddingTop: 0 });

        $('#CreatePartial').animate({ height: 40 }, {
            queue: false,
            duration: 450,
            complete: function () {
                $('#CreatePartial').animate({ width: 92 }, {
                    queue: false,
                    duration: 300,
                    complete: function () {
                        $('#CreateAffiliateForm').remove();
                        $(caller).removeAttr('disabled');
                        $(caller).text("Create").attr("id", "CreateSubmit");
                    }
                });
            }
        })
    });

    // Get Affiliate/Edit
    var profileSectionHeight = "";
    $("div#EditPartial").hide();
    $(function () {
        $("div#AffiliateInfo").on("click", ".editLinks #editAffiliate", function () {
            //var height = SetResponsiveHeight();

            var caller = this;
            $(caller).button('loading');
            $.ajax({
                url: $(this).data('url'),
                type: 'GET',
                cache: false,
                success: function (result) {
                    //$('div#ProfileAffiliateSection').animate({  }, 200, function () {
                    $('div#AffiliateInfo').hide("slide", { direction: "left" }, 200, function () {
                        $('div#EditPartial').html(result).fadeIn();
                        $(caller).button('reset');
                    });
                    //});
                }
            });
        });
    });

    // Post Affiliate/Edit
    $(function () {
        $("div#ProfileAffiliateSection").on("click", ".span9 #EditPartial div form .span6 #EditSubmit", function () {
            if (!$("#form0").valid()) {
                return false;
            }
            $.ajax({
                url: $(this).data('url'),
                type: 'POST',
                cache: false,
                data: $("#form0").serialize(),
                success: function (result) {
                    $('div#EditPartial').hide("slide", { direction: "left" }, 200, function () {
                        $('div#AffiliateInfo').show("slide", { direction: "right" }, 200, function () {
                            $('div#ProfileAffiliateSection').css({ "height": "auto" });
                            $('div#AffiliateInfo').html(result);
                        });
                    });
                }
            });
        });
    });

    // Cancel Affiliate/Edit
    $(function () {
        $("div#ProfileAffiliateSection").on("click", ".span9 #EditPartial div form .span6 .cancelButtons", function () {
            $('div#EditPartial').hide("slide", { direction: "left" }, 200, function () {
                $('div#AffiliateInfo').show("slide", { direction: "right" }, 200, function () {
                    $('div#ProfileAffiliateSection').css({ "height": "auto" });
                });
            });
            return false;
        });
    });


    //// -----------------------------------------------------////

    // If height animations are being used
    function SetResponsiveHeight() {
        if ($(window).width() > 979) {
            return 400;
        }
        else if ($(window).width() < 980 && $(window).width() > 767) {
            return 400;
        }
        else if ($(window).width() < 767) {
            return 900;
        }
    }
}

$(document).ready(function () {
    Forms();
});
