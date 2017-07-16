$(function () {
    $('#FormStatus').change(function () {
        if ($(this).val() === "Approved"){
            displayFor.style.display = 'none';
        }
        else if ($(this).val() === "Rejected"){
            displayFor.style.display = 'none';
        }
        else if ($(this).val() === "") {
            displayFor.style.display = 'inline-block';
        }
        else if ($(this).val() === "Forward") {
            displayFor.style.display = 'inline-block';
        }
    });
});

$(function () {
    $('#Reason').change(function () {
        if ($(this).val() === "Other") {
            displayFor.style.display = 'inline-block';
        }
        else if ($(this).val() === "other") {
            displayFor.style.display = 'inline-block';
        }
        else if ($(this).val() === "Personal") {
            displayFor.style.display = 'none';
        }
        else if ($(this).val() === "Finance") {
            displayFor.style.display = 'none';
        }
    });
});


Skype.ui({
    "name": "call",
    "element": "SkypeButton_Call",
    "participants": ["fak.samuel"],
    "imageSize": 23,
    "imageHeight": 20,
    "imageColor": "blue",
    "listParticipants": true
});