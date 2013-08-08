Easyfy = function () {

    SetArticleHeight();

    function SetArticleHeight() {
        var heighest = [];

        // Loop through the elements and push to array
        $(".container article .row-fluid .span4").each(function (i) {
            heighest.push($(".container article .row-fluid .span4 p")[i].clientHeight);
        });

        // Fetch the highest element
        var height = GetHighestValue(heighest);

        SetCommonHeight($(".container article .row-fluid .span4"), height);
    }

    // Affiliate Details header
    $("#AffiliateProfileHeaderArrow").css({ "margin-left": -$("#AffiliateProfileHeaderName").width() });

    $(function () {
        if ($("#AffiliateProfileHeaderArrow").is(":hidden")) {
            $("#AffiliateProfileHeader").hover(function () {
                $("#AffiliateProfileHeaderArrow").fadeIn();
                //$("<span class='muted'>View detailed information about the affiliate.</span>").insertAfter(this).fadeIn();
            }, function () {
                $("#AffiliateProfileHeaderArrow").stop().fadeOut();
            });
        }
    });

    $("#AffiliateDetailsContent").on(
    {
        mouseenter: function () {
            $(this).animate({ width: 150 }, 75)
        },
        mouseleave: function () {
            $(this).animate({ width: 43 }, 75);
        }
    }
    , "#ClosePageArrow, #ClosePageText");

    $("#AffiliateProfileHeader").click(function () {
        if ($("#ProfileAffiliateSection").is(":hidden")) {
            $("#ProfileAffiliateSection").slideDown(120);
            $("#AffiliateProfileHeaderArrow i").removeClass("icon-double-angle-down");
            $("#AffiliateProfileHeaderArrow i").addClass("icon-double-angle-up");
        } else {
            $("#ProfileAffiliateSection").slideUp(120);
            $("#AffiliateProfileHeaderArrow i").removeClass("icon-double-angle-up");
            $("#AffiliateProfileHeaderArrow i").addClass("icon-double-angle-down");
        }
    });


    // Returns the highest value of array 	
    function GetHighestValue(array) {
        return Math.max.apply(Math, array);
    }

    // Set element to common height
    function SetCommonHeight(elem, height) {
        $(elem).height(height + 125);
    }

    // Style and simulate fileupload-button
    $("div.fakeButton input[type=button]").click(function () {
        $(".fileUpload").css({ "display": "inline-block" });
        $(".fileUpload").click();
    });

    $(".fileUpload").change(function (click) {
        $(".fakeTextField").val(this.value); // Set fake filepath to textfield
    });

    // Hover-effects for links with icons
    $(function () {
        $("#editAthlete, .editAffiliate").hover(function () {
            $(this).find("i").css({ "color": "#555" })
        }, function () {
            $(this).find("i").css({ "color": "rgb(148, 148, 148)" })
        });
    });

    // Set ellipsis on elements where the text might overflow
    $(".affiliateBox p").ellipsis({ "live": true });


    /**
     * Create a dropdown menu of the nav element on
     * the fly if the width is less than or equals 500px
     **/
    $("nav select").change(function () {
        window.location = $(this).find("option:selected").val();
    });

    // Create the dropdown base
    $("<select />").appendTo("nav");

    // Create default option "Go to..."
    $("<option />", {
        "selected": "selected",
        "value": "",
        "text": "Go to..."
    }).appendTo("nav select");

    // Populate dropdown with menu items
    $("nav a").each(function () {
        var el = $(this);
        $("<option />", {
            "value": el.attr("href"),
            "text": el.text()
        }).appendTo("nav select");
    });


    // Disable form submit button on submit-event
    $("#CreateAffiliateForm").submit(function () {
        $(":submit", this).attr("disabled", "disabled");
    });

    // Hover-effect for "on-the-fly/partial"-elements
    $("div#ProfileSection").on(
    {
        mouseenter: function () {
            $("#ShowProfilePicture").show();
        },
        mouseleave: function () {
            $("#ShowProfilePicture").hide();
        }
    }
    , ".image, #ShowProfilePicture");

    $("#AffiliateDetailsContent").on(
    {
        mouseenter: function () {
            $(".userInfo", this).stop(true, true).animate({ marginTop: 0 });
        },
        mouseleave: function () {
            if ($(".userInfo", this).children().first().hasClass("btn-group open")) {
                $(".userInfo", this).children().first().removeClass("open");
                $(".userInfo", this).children().first().children().first().blur();
            }
            $(".userInfo", this).stop().animate({ marginTop: -25 });
        }
    }
    , ".profileSection");

    // Hover for icons under Affiliate athletes
    $("#AffiliateDetailsContent").on(
    {
        mouseenter: function () {
            $(this).find("i").css({ "color": "#555" })
        },
        mouseleave: function () {
            $(this).find("i").css({ "color": "rgb(148, 148, 148)" })
        }
    }
    , ".editAthlete");



    //----- Validation tooltips
    // Create affiliate
    $("div#CreatePartial").on("focus", "input, input, textarea", function () {
        $($(this).prev()).fadeIn();
    });

    $("div#CreatePartial").on("blur", "input, input, textarea", function () {
        $($(this).prev()).fadeOut(50);
    });

    // Create WOD
    $("div#AffiliateDetailsContent").on("focus", "input, textarea", function () {
        $($(this).prev()).fadeIn();
    });

    $("div#AffiliateDetailsContent").on("blur", "input, textarea", function () {
        $($(this).prev()).fadeOut(50);
    });

    // Login, Register email, Retrieve password
    $("#LoginForm div form fieldset input, #ExternalLoginConfirmationContent input, #RetrievePasswordContent form fieldset input, #inputAge, #inputEmail").focus(function () {
        $($(this).prev()).fadeIn();
    });

    $("#LoginForm div form fieldset input, #ExternalLoginConfirmationContent input, #RetrievePasswordContent form fieldset input, #inputAge, #inputEmail").blur(function () {
        $($(this).prev()).fadeOut(50);
    });

    $("#Content div form div div div input").focus(function () {
        $($(this).prev()).fadeIn();
    });

    $("#RetrievePasswordContent form fieldset input[type=text]").blur(function () {
        $($(this).prev()).fadeOut(50);
    });

    // ?
    $("div#ProfileSection").on("focus", "input[type=text], input[type=password]", function () {
        $($(this).prev()).fadeIn();
    });

    $("div#ProfileSection").on("blur", "input[type=text], input[type=password]", function () {
        $($(this).prev()).hide();
    });

    $("div#ProfileSection").on("focus", "#EditPartial form #Birth", function () {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: '-100:+0'
        });
    });

    // FIXA TILL
    $("#inputAge").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '-100:+0'
    });

    for (var i = 0; i < $(".athleteAffiliateConnections").length; i++) {
        var elem = $(".athleteAffiliateConnections")[i];
        var elemChildren = $(elem).children()[1];
        if ($(elemChildren).children().length > 5) {
            $(elemChildren).hide().end();
            $("<span class='numberOfAffiliates'>(" + $(elemChildren).children().length + ")</span>").appendTo($(elem).children().first());
        } else {

            $("<span class='numberOfAffiliates'>(" + $(elemChildren).children().length + ")</span>").appendTo($(elem).children().first()).hide();
        }
    }

    // Hover gym header under Athlete/Details
    $(function () {
        $(".AthleteGymHeaders").disableSelection();
        $(".AthleteGymHeaders").hover(function () {
            $(this).find("span").css({ "color": "rgba(255, 153, 0, .9)" })
        }, function () {
            $(this).find("span").css({ "color": "rgba(255, 153, 0, .6)" })
        });
    });

    // Toggle gyms under Athlete/Details
    $("#AthleteGyms h5").click(function () {
        $(this).find("span").fadeToggle(200);
        $(this).next(".content").toggle("blind", 200);
    });

    $(".athleteWOD h5").click(function () {
        $(this).next(".content").toggle("blind", 100);
    });

    $(function () {
        if (window.location.href.indexOf($(".myProfileMenuLink").attr("href")) >= 0) {
            $(".myProfileMenuLink").addClass("current");
        } else if (window.location.href.indexOf("/Affiliate") >= 0) {
            $(".affiliateMenuLink").addClass("current");
        } else if (window.location.href.indexOf("/Admin") >= 0) {
            $(".adminMenuLink").addClass("current");
        } else if (window.location.href.indexOf("/Athlete") >= 0) {
            // Do nothing
        } else if (window.location.href.indexOf("/Account") >= 0) {
            // Do nothing
        } else {
            $(".startpageMenuLink").addClass("current");
        }
    });



    /*** Ajax pages ***/
    // Get _CreateWOD-view
    $(function () {
        $(".profileMenuListLinks").on("click", ".affiliateDetailsLinks", function () {
            var caller = this;

            $(caller).siblings().prop("disabled", true);

            $(caller).button('loading');
            $.ajax({
                url: $(this).data('url'),
                type: 'GET',
                cache: false,
                success: function (result) {
                    $("#AffiliateDetailsContent").html(result).show("slide", { direction: "left" }, 200);
                    $(caller).button('reset');
                    $(caller).siblings().prop("disabled", false);
                    SetLinkActiveStatus(result);
                }
            });
        });
    });

    $(function () {
        $("#AffiliateDetailsContent").on("click", "#Content #ClosePageArrow", function () {
            var caller = this;

            $('html, body').animate({ scrollTop: 0 }, {
                queue: false,
                duration: 300,
                complete: function () {
                    $(caller).parent().slideUp(150, function () {
                        $(caller).parent().remove();

                    });
                    ResetLinkActiveStatus();
                }
            });
        });
    });
    

    // Link manipulation for Affiliate Details-view
    function SetLinkActiveStatus(data) {
        if ($("#affiliateMenuList ul li button").hasClass("active")) {
            $("#affiliateMenuList ul li button").removeClass("active");
        }

        $('html, body').animate({
            scrollTop: $("#affiliateMenuList ul li button").offset().top
        }, 500);

        var elem = $(data).find("h4")[0].innerHTML;

        if (elem == "Create WOD") {
            $("#CreateWODLink").addClass("active");
        }

        else if (elem == "Wods") {
            $("#WODSLink").addClass("active");
        }

        else if (elem == "Schedule wod") {
            $("#ScheduleWodsLink").addClass("active");
        }

        else if (elem.indexOf("Athletes in") >= 0) {
            $("#AffiliateMembersLink").addClass("active");
        }
    }

    function ResetLinkActiveStatus() {
        $("#CreateWODLink").removeClass("active");
        $("#WODSLink").removeClass("active");
        $("#AffiliateMembersLink").removeClass("active");
        $("#ScheduleWodsLink").removeClass("active");
    }
}

$(document).ready(function () {
    Easyfy();
});

