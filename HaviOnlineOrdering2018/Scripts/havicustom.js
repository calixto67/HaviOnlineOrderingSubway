/*!
 * havicustom v1.0(http://getbootstrap.com)
 * Copyright 2019 Havi Logistics Philippines.
 * Developed by Calixto Gelarzo Jr.
 */



function totalrow(api, colno) {

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            parseFloat(i.replace(/[\₱,]/g, '')) * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    if (typeof $(api.column(colno).data()[0]).val() == 'undefined') {
        total = api
       .column(colno)
       .data()
       .reduce(function (a, b) {
           return parseFloat(intVal(a)) + parseFloat(intVal(b));
       }, 0);
    }
    else {
        var total = 0;
        $(api.column(colno).nodes()).find('input').each(function () {
            total += parseInt(isNaN(this.value) || this.value == '' ? 0 : this.value);
        })
    }
    return total;
}

function validateInputs(myfield) {

    var unitprice = Number($($(myfield).closest('td')).prev('td').html().replace(/[^0-9.-]+/g, ""));
    var qty = $(myfield).val();
    if (isNaN(qty) || qty == "") { qty = 0 };
    var ttp = parseFloat(unitprice) * parseFloat(qty);
    $($(myfield).closest('td')).next('td').html('₱' + ttp.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));

    UpdateTotalPrice($($(myfield).closest('tr')).index());
}


var digitsOnly = /[1234567890]/g;
var integerOnly = /[0-9\.]/g;
var alphaOnly = /[A-Za-z]/g;
var usernameOnly = /[0-9A-Za-z\._-]/g;
var mobilenumberonly = /^[0-9]{0,}$/g;


function IsValid(qtyfield, inner) {
    if (qtyfield.value >= inner) {
        var tooltip = document.getElementById("tooltip");
        tooltip.innerHTML = qtyfield.title;
        tooltip.style.display = "block";
        tooltip.style.top = qtyfield.offsetTop - tooltip.offsetHeight + "px";
        tooltip.style.left = qtyfield.offsetLeft + "px";
        qtyfield.value = "";
        qtyfield.classList.add("error");
        setTimeout(function () { qtyfield.focus(); }, 20);
        qtyfield.parentNode.firstElementChild.style.display = 'block';
    }
    else {
        qtyfield.classList.remove("error");
        qtyfield.parentNode.firstElementChild.style.display = 'none';
    }
}

function restrictInput(myfield, e, restrictionType, checkdot, IsMobileNo) {
    if (!e) var e = window.event
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    var character = String.fromCharCode(code);
    var charcode = e.charCode;
    // if user pressed esc... remove focus from field...
    if (code == 27) { this.blur(); return false; }

    // ignore if the user presses other keys
    // strange because code: 39 is the down key AND ' key...
    // and DEL also equals .

    if (IsMobileNo) {
        if (!e.ctrlKey && code != 9 && code != 8 && code != 46 && code != 37 && code != 39) {
            if (character.match(restrictionType)) {
                if (checkdot == "checkdot") {
                    return !isNaN(myfield.value.toString() + character);
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }
        else {
            if (e.ctrlKey && code == 97) {
                return true;
            }
            else {
                if (charcode != 0) { return false; }
            }
        }
    }
    else {
        if (!e.ctrlKey && code != 9 && code != 8 && code != 36 && code != 37 && code != 38 && (code != 39 || (code == 39 && character == "'")) && code != 40) {
            if (character.match(restrictionType)) {
                if (checkdot == "checkdot") {
                    return !isNaN(myfield.value.toString() + character);
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }
    }
}


if (typeof jQuery === 'undefined') {
    throw new Error('Bootstrap\'s JavaScript requires jQuery')
}

+function ($) {
    var version = $.fn.jquery.split(' ')[0].split('.')
    if ((version[0] < 2 && version[1] < 9) || (version[0] == 1 && version[1] == 9 && version[2] < 1)) {
        throw new Error('Bootstrap\'s JavaScript requires jQuery version 1.9.1 or higher')
    }



}(jQuery);



(function ($) {

    $.fn.isNullOrEmpty = function () {
        var returnValue = false;
        var str = this.val();
        if (!str
            || str == null
            || str === 'null'
            || str === ''
            || str === '{}'
            || str === 'undefined'
            || str.length === 0) {
            returnValue = true;
        }
        return returnValue;
    };

}(jQuery));

