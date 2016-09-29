var dhpr = "https://api.hres.ca/dhpr/Controller/dhprController.ashx?";

$("summary").addClass("wb-toggle well well-sm");
$("summary").attr("data-toggle", "{\"persist\": \"session\"}");

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
    var searchUrl = dhpr + "linkID=" + linkID +  "&pType=" + pType + "&lang=" + lang;
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
function displayMilestoneList(data) {
    if (data.length == 0) {
        return "";
    }
    // console.log(data.length);
    var txt = "";
    var i;
    for (i = 0; i < data.length; i++) {
        txt += "<tr><td>" + (data[i].Milestone) + "</td>";
        txt += "<td>" + formatedDate(data[i].CompletedDate);
        if ($.trim(data[i].Separator) != '') {
            txt += " " + data[i].Separator;
        }
        if ($.trim(data[i].CompletedDate2) != '') {
            txt += " " + formatedDate(data[i].CompletedDate2);
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
        txt += "<tr><td>" + (data[i].ApplicationMilestone) + "</td>";
        txt += "<td>" + formatedDate(data[i].MilestoneDate);
        if ($.trim(data[i].Separator) != '') {
            txt += " " + data1[i].Separator;
        }
        if ($.trim(data[i].MilestoneDate2) != '') {
            txt += " " + formatedDate(data[i].MilestoneDate2);
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
        list += "<li>" + e.Bullet + "</li>";
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

//function ExportExcel(JSONData, ReportTitle, ShowLabel) {
//    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
//    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData.data) : JSONData.data;
//    var CSV = '';
//    //Set Report title in first row or line

//    CSV += ReportTitle + '\r\n\n';

//    //This condition will generate the Label/Header
//    if (ShowLabel) {
//        var row = "";
//        //This loop will extract the label from 1st index of on array
//        for (var index in arrData[0]) {
//            //Now convert each value to string and comma-seprated
//            row += index + ',';
//        }

//        row = row.slice(0, -1);
//        //append Label row with line break
//        CSV += row + '\r\n';
//    }

//    //1st loop is to extract each row
//    for (var i = 0; i < arrData.length; i++) {
//        var row = "";

//        //2nd loop will extract each column and convert it in string comma-seprated
//        for (var index in arrData[i]) {            
//            if (index == 'DateDecision' || index == 'CreatedDate' || index == 'DateIssued') {
//                row += '"' + formatedDate(arrData[i][index]) + '",';
//            }
//            else {
//                row += '"' + arrData[i][index] + '",';
//            }          
//        }

//        row.slice(0, row.length - 1);
//        //add a line break after each row
//        CSV += row + '\r\n';
//    }

//    if (CSV == '') {
//        alert("Invalid data");
//        return;
//    }

//    //Generate a file name
//    var fileName = ReportTitle.replace(/ /g, "_");
//    //this will remove the blank-spaces from the title and replace it with an underscore
//   // fileName += ReportTitle.replace(/ /g, "_");
//    //Initialize file format you want csv or xls
//    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

//    // Now the little tricky part.
//    // you can use either>> window.open(uri);
//    // but this will not work in some browsers
//    // or you will not get the correct file extension    

//    //this trick will generate a temp <a /> tag
//    var link = document.createElement("a");
//    link.href = uri;

//    //set the visibility hidden so it will not effect on your web-layout
//    link.style = "visibility:hidden";
//    link.download = fileName + ".csv";

//    //this part will append the anchor tag and remove it after automatic click
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
//}

//function ExportJson(el, JSONData) {
//    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
//    var data = "text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(arrData));
//    el.setAttribute("href", "data:" + data);
//    el.setAttribute("download", "RDSResult.json");
//}

////Export Xml
//function ExportXml(el) {
//    var JSONData = $('#txt').val();
//    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
//    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
//    var xml = json2xml(arrData, 'items');
//    xml = '<?xml version="1.0" encoding="utf-8"?>' + xml;    
//    var data = "Application/octet-stream," + encodeURIComponent(xml);
//    el.setAttribute("href", "data:" + data);
//    el.setAttribute("download", "RDSResult.xml");
//}

//var arrayCount = 0;
//var json2xml = (function (undefined) {
//    "use strict";
//    var tag = function (name, closing) {
//       // console.log("name:" + name + "arrayCount:" + arrayCount);
//        if (name == arrayCount) {
//            name = "item";
//        }
//        return "<" + (closing ? "/" : "") + name + ">";
//    };
//    return function (obj, rootname) {
//        var xml = "";
//        for (var i in obj) {

//            if (obj.hasOwnProperty(i)) {
//                var value = obj[i],
//                    type = typeof value;
//                if (value instanceof Array && type == 'object') {
//                    for (var sub in value) {
//                        xml += json2xml(value[sub]);
//                    }
//                } else if (value instanceof Object && type == 'object') {
//                    xml += tag(i) + json2xml(value) + tag(i, 1);
//                    arrayCount++;                   
//                } else {
//                    if (i == 'DateDecision' || i == 'CreatedDate' || i == 'DateIssued') {
//                        xml += tag(i) + formatedDate(value) + tag(i, {
//                            closing: 1
//                        });
//                    }
//                    else
//                    {
//                        xml += tag(i) + value + tag(i, {
//                            closing: 1
//                        });
//                    }
//                }
                
//            }
//        }

//        return rootname ? tag(rootname) + xml + tag(rootname, 1) : xml;
//    };
//})(json2xml || {});



   

