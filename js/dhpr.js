//var dhpr = "http://api.hres.ca/dhpr/controller/dhprController.ashx?";
var dhpr = "./controller/dhprController.ashx?";

$("summary").addClass("wb-toggle well well-sm");
$("summary").attr("data-toggle", "{\"persist\": \"session\"}");

var jsonbread = [{ title: "Home", href: "http://www.canada.ca/" }, { title: "All Services", href: "http://www.canada.ca/en/services/index.html" },
        { title: "Health", href: "http://healthycanadians.gc.ca/index-eng.php" },
        { title: "Drugs, health & consumer products", href: "http://healthycanadians.gc.ca/drugs-products-medicaments-produits/index-eng.php" },
        { title: "Drug Product Search" }];

var term = getParameterByName("term");

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
     return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));    
}

function goDhprUrl(lang, pType,term) {
    var searchUrl = dhpr + "term=" + term + "&pType=" + pType + "&lang=" + lang;
    return searchUrl;
}


function goDhprLangUrl(lang, pType,term, pageName) {
    var langSwitch = lang == 'en' ? "fr" : "en";
    var langUrl = pageName + '.html';
        langUrl += "?term=" + term + "&pType=" + pType + "&lang=" + langSwitch;
    return langUrl;
}

function goDhprUrlByID(lang, pType) {
    var linkID = getParameterByName("linkID");
    var searchUrl = dhpr + "linkID=" + linkID + "&pType=" + pType + "&lang=" + lang;
    //console.log(searchUrl);
    return searchUrl;
}
function goDhprLangUrlByID(lang, pType, pageName) {
    var linkID = getParameterByName("linkID");
    var langSwitch = lang == 'en' ? "fr" : "en";
    var langUrl = pageName + '.html';
         langUrl += "?linkID=" + linkID + "&pType=" + pType + "&lang=" + langSwitch;
    return langUrl;
}


function OnFail(result) {
    window.location.href = "./genericError.html";
}

function formatedISMedicalDevice(IsMd) {
    var returnMd = "";
    if(IsMd)
    {
        returnMd = "Yes";
    }
    else {
        returnMd = "No";
    }
    return returnMd;
}
function formatedContact(contactName, contactUrl) {
    if ($.trim(contactName) == '')
    {
        return "&nbsp;";
    }
    return '<a href='+ contactUrl + '>' + contactName + '</a>';
}

function formatedMedIngredientDesc(medIngredient, strength, dosageform) {
    return  medIngredient + ", " + strength + "," + dosageform;
}
function formatedClinBasisDesc(basisOne, basisTwo, basisThree) {
    if ($.trim(basisThree) == '') {
        return  basisOne + "<br/>" + basisTwo;
    }
    return basisOne + "<br/>" + basisTwo + "<br/>" + basisThree;
}


function formatedDate(data) {
    if ($.trim(data) == '') {
            return "";
        }
        var data = data.replace("/Date(", "").replace(")/", "");
        if (data.indexOf("+") > 0) {
            data = data.substring(0, data.indexOf("+"));
        }
        else if (data.indexOf("-") > 0) {
            data = data.substring(0, data.indexOf("-"));
        }
        var date = new Date(parseInt(data, 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
}

function formatedList(data) {
    var list;
    if ($.trim(data) == '') {
        return "";
    }
    $.each(data, function (index, record) {
        list +=  record + "<br />";
    });

    if (list != '') {
        list = list.replace("undefined", "");
        list = list.replace(/^<br\s*\/?>|<br\s*\/?>$/g, '');
        return list;
    }
    return "";
}
function formatedDinWithOthers(data, medIngredient, routeAdmin) {
    var list;
    if ($.trim(data) == '') {
        return "";
    }
    $.each(data, function (index, record) {
        list += record + "," + medIngredient + "," + routeAdmin + "<br />";
    });

    if (list != '') {
        list = list.replace("undefined", "");
        list = list.replace(/^<br\s*\/?>|<br\s*\/?>$/g, '');
        return list;
    }
    return "";
}


function formatedTombstoneList(data) {
    if (data.length == 0) {
        return "&nbsp;";
    }
    //console.log(data.length);
    var txt = "";
    var i;
    for (i = 0; i < data.length; i++) {
        txt += (data[i].med_ingredient) + ", " + (data[i].strength) + ", " + (data[i].dosageform) + ", " + (data[i].route_admin) + "; <br />";
    }

    if (txt != '') {
        txt = txt.replace("undefined", "");
        txt = txt.replace(/^<br\s*\/?>|<br\s*\/?>$/g, '');
        return txt;
    }
    return "&nbsp;";
}


function displayMilestoneList(data) {
    if (data.length == 0) {
        return "";
    }
    // console.log(data.length);
    var txt = "";
    var i;
    for (i = 0; i < data.length; i++) {
        txt += "<tr><td>" + (data[i].milestone) + "</td>";
        txt += "<td>" + formatedDate(data[i].completed_date);
        if ($.trim(data[i].separator) != '') {
            txt += " " + data[i].separator;
        }
        if ($.trim(data[i].completed_date2) != '') {
            txt += " " + formatedDate(data[i].completed_date2);
        }
        txt += "</td></tr>"
    }

    if (txt != '') {
        txt = txt.replace("undefined", "");
        return txt;
    }
    return "&nbsp;";
}

function displayAppMilestoneList(data){
    if (data.length == 0) {
        return "";
    }

    var txt = "";
    var i;
    for (i = 0; i < data.length; i++) {
        txt += "<tr><td>" + (data[i].application_milestone) + "</td>";
        txt += "<td>" + formatedDate(data[i].milestone_date);
        if ($.trim(data[i].separator) != '') {
            txt += " " + data1[i].separator;
        }
        if ($.trim(data[i].milestone_date2) != '') {
            txt += " " + formatedDate(data[i].milestone_date2);
        }
        txt += "</td></tr>"
    }

    if (txt != '') {
        txt = txt.replace("undefined", "");
        return txt;
    }
    return "&nbsp;";


}


function formatedBulletList(data, type) {
    var list = "";
    if (data.length == 0) {
        return "";
    }

    $.grep(data, function (e) {
        list += "<li>" + e.bullet + "</li>";
    });

    if (list != '') {
        list = list.replace("undefined", "");
        list = list.replace(/"/g, "");

        if (type == '1')
        {
            return "<ol>" + list + "</ol>";
        }
        else if (type == '2')
        {
            return "<ol type='a'>" + list + "</ol>";
        }
        else {
            return "<ul>" + list + "</ul>";
        }
    }
    return "";
}


function ExportTableToCSV($table, filename) {

    var $rows = $table.find('tr:has(td),tr:has(th)'),
        tmpColDelim = String.fromCharCode(11), // vertical tab character
        tmpRowDelim = String.fromCharCode(0), // null character

        // actual delimiter characters for CSV format
        colDelim = '","',
        rowDelim = '"\r\n"',

        // Grab text from table into CSV formatted string
        csv = '"' + $rows.map(function (i, row) {
            var $row = $(row), $cols = $row.find('td,th');

            return $cols.map(function (j, col) {
                var $col = $(col), text = $col.text();
                return text.replace(/"/g, '""'); // escape double quotes

            }).get().join(tmpColDelim);

        }).get().join(tmpRowDelim)
            .split(tmpRowDelim).join(rowDelim)
            .split(tmpColDelim).join(colDelim) + '"',
        // Data URI
        csvData = 'data:application/csv;charset=utf-8,' + encodeURIComponent(csv);
       //console.log(csv);

    if (window.navigator.msSaveBlob) { // IE 10+
        //alert('IE' + csv);
        window.navigator.msSaveOrOpenBlob(new Blob([csv], { type: "text/plain;charset=utf-8;" }), filename)
    }
    else {
        $(this).attr({ 'download': filename, 'href': csvData, 'target': '_blank' });
    }
}


function msieversion() {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If Internet Explorer, return true
    {
        return true;
    } else { // If another browser,
        return false;
    }
    return false;
}


   

