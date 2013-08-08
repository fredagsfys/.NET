Exercise = function () {
    attachEditEvents();
    attachDeleteEvents();

    $("#createExerciseForm").validate({
        rules: {
            exerciseName:
            {
                required: true,
                maxlength: 50
            },
            exerciseLength:
            {
                required: function() {
                    return ($('#dt_Metabolic').css('display') != 'none');
                },
                maxlength: 10,
                digits: true
            }
        },
        messages: {
            exerciseName: "Don't forget the name",
            exerciseLength: "Don't forget the length, only digits"
        }
    });

    $('#create_exercise_link').click(function() {

        if ($("#createExerciseForm").valid()) {
            $('#create_exercise_link').after("<span id='create_exercise_link_update'><img class='updateimg' src='/content/img/fbspinner.gif' alt='Updating...' /> Updating...</span>");
            $('#create_exercise_link').hide();

            var f = $("#createExerciseForm").serialize();
            $.ajax({
                url: "/exercises/addexercise/",
                type: "POST",
                data: f,
                success: function(msg) {
                    if (msg == "ok") {
                        $('#create_exercise_link_update').remove();
                        $('#create_exercise_link').show();

                        //$('#createMessage').html("Exercise Added");
                        clear_form_elements("#createExerciseForm");
                        ReloadExercises();
                        //attachDeleteEvents();
                    } else {
                        $('#create_exercise_link_update').remove();
                        $('#create_exercise_link').show();
                        $('#createMessage').html("<label class='error'>" + msg + "</label>");
                    }
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    $('#create_exercise_link_update').remove();
                    $('#create_exercise_link').show();

                    $('#createMessage').html("<label class='error'>Error big time =).</label>");
                }
            });
        }
        return false;
    });

    $("#createExerciseForm").keydown(function(e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            $('#create_exercise_link').click();
            return false;
        } else {
            return true;
        }
    });
    
    //Remove Errormessage
    $("input").focus(function() {
        $('#createMessage').html("");
    });

    function clear_form_elements(ele) {

        $(ele).find(':input').each(function() {
            switch (this.type) {
                case 'password':
                case 'select-multiple':
                case 'select-one':
                case 'text':
                case 'textarea':
                    $(this).val('');
                    break;
                case 'checkbox':
                case 'radio':
                    this.checked = false;
            }
        });

        $("#exerciseType option[value='Metabolic']").attr('selected', 'selected');
        $("#exerciseMetabolicUnit option[value='meters']").attr('selected', 'selected');

        showHideInput('Metabolic');
    }

    function showHideInput(value) {
        if (value == 'Metabolic') {
            $(".dd_Metabolic").show();
            return false;
        } else {
            $(".dd_Metabolic").hide();
            return true;
        }
    }

    $("#exerciseType").change(function () {
        showHideInput($('#exerciseType').val());
    });

    function deleteExerciseItem(id) {
        if (confirm('Are you sure you want to delete exercise')) {
            $('#e_' + id).hide();
            $('#e_' + id).after("<span id='s" + id + "'><img class='updateimg' src='/content/img/fbspinner.gif' alt='Deleting...' /> Deleting...</span>");
            $.get("/exercises/removeexercise/" + id, "", function () {
                ReloadExercises();
            });
        }
    }

    function ReloadExercises() {
        $('#divExerciseList').after("<span id='divExerciseList_update'><img class='updateimg' src='/content/img/fbspinner.gif' alt='Updating...' /> Updating...</span>");
        $('#divExerciseList').hide();
        $.get(
          "/exercises/getexercises",
          function (data) {
              $("#divExerciseList").html(data);
              $('#divExerciseList_update').remove();
              $('#divExerciseList').show();
              attachEditEvents();
              attachDeleteEvents();
          });
    }

    function attachDeleteEvents() {
        // Unbind default click events
        $("a.deleteExercise").unbind("click");

        $("a.deleteExercise").click(function () {
            deleteExerciseItem(this.id.substring(2));
            return false;
        });

        $("a.deleteExercise").attr("href", "#");
    }

    function attachEditEvents() {
        // Unbind default click events
        $(".editGymnasticExercise").unbind("click");
        $(".editMetabolicExercise").unbind("click");
        $(".editWeightLiftExercise").unbind("click");
        $(".editRestPeriodExercise").unbind("click");

        $(".editGymnasticExercise").click(function () {
            var strId = $(this).attr("id");
            strId = strId.slice(2);
            var strEditText = $(this).text();
            $(this).next(".deleteExercise").hide();
            $(this).after("<span id='w" + strId + "' class='inEditW'><input class='edit' id='i" + strId + "' type='text' value='" + strEditText + "' /><a onclick=\"saveExercise('" + strId + "');\" class='editSave'>Save</a> <a onclick=\"cancelUpdateExercise('" + strId + "');\" class='editCancel'>Cancel</a></span>");
            $(this).hide();
            return false;
        });

        $(".editMetabolicExercise").click(function () {
            var strId = $(this).attr("id");
            strId = strId.slice(2);
            var strEditText = $("#eN_" + strId).text();
            var strEditLength = $("#eL_" + strId).text();
            var strEditLengthUnit = $("#eM_" + strId).text();

            var selectedMeter = "";
            if (strEditLengthUnit == "m") {
                selectedMeter = "selected";
            }
            var selectedKm = "";
            if (strEditLengthUnit == "km") {
                selectedKm = "selected";
            }
            var selectedMile = "";
            if (strEditLengthUnit == "mile") {
                selectedMile = "selected";
            }
            var selectedFoot = "";
            if (strEditLengthUnit == "feet") {
                selectedFoot = "selected";
            }

            $(this).next(".deleteExercise").hide();
            $(this).after("<span id='w" + strId + "' class='inEditW'><input class='edit' id='i" + strId + "' type='text' value='" + strEditText + "' /><input class='edit' id='i2" + strId + "' type='text' value='" + strEditLength + "' /><select id='i3" + strId + "'><option value='m' " + selectedMeter + ">m</option><option value='km' " + selectedKm + ">km</option><option value='feet' " + selectedFoot + ">feet</option><option value='mile' " + selectedMile + ">mile</option></select><a onclick=\"saveMetabolicExercise('" + strId + "');\" class='editSave'>Save</a><a onclick=\"cancelUpdateExercise('" + strId + "');\" class='editCancel'>Cancel</a></span>");
            $(this).hide();
            return false;
        });

        $(".editWeightLiftExercise").click(function () {
            var strId = $(this).attr("id");
            strId = strId.slice(2);
            var strEditText = $(this).text();
            $(this).next(".deleteExercise").hide();
            $(this).after("<span id='w" + strId + "' class='inEditW'><input class='edit' id='i" + strId + "' type='text' value='" + strEditText + "' /><a onclick=\"saveExercise('" + strId + "');\" class='editSave'>Save</a><a onclick=\"cancelUpdateExercise('" + strId + "');\" class='editCancel'>Cancel</a></span>");
            $(this).hide();
            return false;

        });

        $(".editRestPeriodExercise").click(function () {
            var strId = $(this).attr("id");
            strId = strId.slice(2);
            var strEditText = $(this).text();
            $(this).next(".deleteExercise").hide();
            $(this).after("<span id='w" + strId + "' class='inEditW'><input class='edit' id='i" + strId + "' type='text' value='" + strEditText + "' /><a onclick=\"saveExercise('" + strId + "');\" class='editSave'>Save</a><a onclick=\"cancelUpdateExercise('" + strId + "');\" class='editCancel'>Cancel</a></span>");
            $(this).hide();
            return false;

        });
    }
}

function cancelUpdateExercise(intid) {
    $('#e_' + intid).show();
    $('#d_' + intid).show();
    $('#w' + intid).remove();
}

function saveExercise(intid) {
    $('#w' + intid).after("<span id='s" + intid + "'><img class='updateimg' src='/content/img/fbspinner.gif' alt='Updating...' /> Updating...</span>");
    $('#w' + intid).hide();

    var valueName = $("#i" + intid).val()
    $.ajax({
        url: "/exercises/updateexercise",
        type: "POST",
        data: {
            id: intid,
            name: valueName
        },
        success: function (msg) {
            if (msg == "ok") {
                $('#s' + intid).remove();
                $('#e_' + intid).html(valueName);
                $('#e_' + intid).show();
                $('#w' + intid).remove();
                $('#d_' + intid).show();
            } else {
                $('#createMessage').html("<label class='error'>" + msg + "</label>");
                $('#s' + intid).remove();
                $('#w' + intid).show();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#createMessage').html("<label class='error'>Error big time =).</label>");
        }
    });
}

function saveMetabolicExercise(intid) {
    $('#w' + intid).after("<span id='s" + intid + "'><img class='updateimg' src='/content/img/fbspinner.gif' alt='Updating...' /> Updating...</span>");
    $('#w' + intid).hide();

    var valueName = $("#i" + intid).val();
    var valueLength = $("#i2" + intid).val();
    var valueLengthUnit = $("#i3" + intid).val();
    $.ajax({
        url: "/exercises/updatemetabolicexercise",
        type: "POST",
        data: {
            id: intid,
            name: valueName,
            length: valueLength,
            lengthunit: valueLengthUnit
        },
        success: function (msg) {
            if (msg == "ok") {
                $('#s' + intid).remove();
                $('#e_' + intid).html("<span id='eN_" + intid + "'>" + valueName + "</span> <span id='eL_" + intid + "'>" + valueLength + "</span> <span id='eM_" + intid + "'>" + valueLengthUnit + "</span>");
                $('#e_' + intid).show();
                $('#d_' + intid).show();
                $('#w' + intid).remove();
            } else {
                $('#createMessage').html("<label class='error'>" + msg + "</label>");
                $('#s' + intid).remove();
                $('#w' + intid).show();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#createMessage').html("<label class='error'>Error big time =).</label>");
        }
    });
}

$(document).ready(function () {
    Exercise();
});