function vjsPrint(text) {
    var span = document.getElementById("spanForGet");

    span.innerHTML = text;
}

function jqPrint(text) {
    var span = $("#spanForGet2");

    span.html(text);
}

//https://www.w3schools.com/js/js_random.asp
function getRandomNumberInclusive(min, max) {
    var n = Math.floor(Math.random() * (max - min + 1) ) + min;

    return n;
}

//https://www.w3schools.com/jsref/coll_table_rows.asp
//https://www.w3schools.com/jsref/coll_table_cells.asp
function vjsGetSelectedValue() {
    var td = document.getElementById("tblBody").rows[1].cells[1];

    vjsPrint("value: " + td.innerHTML);
}

function jqGetSelectedValue() {
    var tbl = $("#tblBody tr:eq(1)").find("td:eq(1)");

    jqPrint("value: " + tbl.html());
}

function vjsUpdateCell() {
    var td = document.getElementById("tblBody").rows[1].cells[1];

    td.innerHTML = "C";

    vjsPrint('<span style="color:red;">Updated cell (1,1) with the letter C.</span>');
}

function jqUpdateCell() {
    var tbl = $("#tblBody tr:eq(1)").find("td:eq(1)");

    tbl.html("D");

    jqPrint('<span style="color:red;">Updated cell (1,1) with the letter D.</span>');
}

//https://www.w3schools.com/jsref/met_table_insertrow.asp
function vjsAddRow() {
    var tbody = document.getElementById("tblBody");
 
    var rIndex = tbody.rows.length;

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(rIndex);

    // Create new cells to fill the row
    var td0 = row.insertCell(0);
    var td1 = row.insertCell(1);
    var td2 = row.insertCell(2);

    // Add some text to the new cells:
    td0.innerHTML = "(" + rIndex + ",0) : E";
    td1.innerHTML = "(" + rIndex + ",1) : E";
    td2.innerHTML = "(" + rIndex + ",2) : E";

    vjsPrint('<span style="color:red;">Look at table on left.</span> Added row: ' + rIndex);
}

function jqAddRow() {
    var tbody = $("#tblBody");

    var rIndex = tbody.find("tr").length;

    var td = "<tr><td>(" + rIndex + ",0) : F</td><td>(" + rIndex + ",1) : F</td><td>(" + rIndex + ",2) : F</td></tr>";

    tbody.append(
        $(td)
    );

    jqPrint('<span style="color:red;">Look at table on left.</span> Added row: ' + rIndex);
}

//https://www.w3schools.com/jsref/met_table_deleterow.asp
function vjsRemoveRow() {
    var tbody = document.getElementById("tblBody");
 
    var rIndex = tbody.rows.length - 1;

    tbody.deleteRow(rIndex);

    vjsPrint('<span style="color:red;">Look at table on left.</span> Removed row: ' + rIndex);
}

function jqRemoveRow() {
    var tbody = $("#tblBody");

    //Just for getting the index number, not necessary otherwise
    var rIndex = tbody.find("tr").length - 1;
    
    //target the last child
    tbody.find("tr:last-child").remove();

    jqPrint('<span style="color:red;">Look at table on left.</span> Removed row: ' + rIndex);
}

//https://www.w3schools.com/jsref/met_tablerow_insertcell.asp
//The act of adding a new column is just adding a cell to every row
function vjsAddColumn() {
    var tbody = document.getElementById("tblBody");

    var cIndex = tbody.rows[0].cells.length;

    //This is focused only on the tbody, the same needs to happen to the thead and tfoot separately
    for(var r = 0; r < tbody.rows.length; r++) {
        var td = tbody.rows[r].insertCell(-1); //-1 is the end of the row
        
        td.innerHTML = "(" + r + "," + cIndex + ")";
    }

    vjsPrint('<span style="color:red;">Look at table on left.</span> Column added: ' + cIndex);
}

function jqAddColumn() {
    var rows = $("#tblBody tr");

    //Just for getting the index number, not necessary otherwise
    var cIndex = rows.first().find("td").length;
    
    rows.each(function(r) {
        $(this).append("<td>(" + r + "," + cIndex + ")</td>");
    });

    jqPrint('<span style="color:red;">Look at table on left.</span> Column added: ' + cIndex);
}

//https://www.w3schools.com/jsref/met_tablerow_deletecell.asp
function vjsRemoveColumn() {
    var tbody = document.getElementById("tblBody");

    var cIndex = tbody.rows[0].cells.length - 1;

    //This is focused only on the tbody, the same needs to happen to the thead and tfoot separately
    for(var r = 0; r < tbody.rows.length; r++) {
        tbody.rows[r].deleteCell(-1); //-1 is the end of the row
    }

    vjsPrint('<span style="color:red;">Look at table on left.</span> Column removed: ' + cIndex);
}

function jqRemoveColumn() {
    var rows = $("#tblBody tr");

    //Just for getting the index number, not necessary otherwise
    var cIndex = rows.first().find("td").length - 1;
    
    rows.each(function() {
        $(this).find("td").last().remove();
    });

    jqPrint('<span style="color:red;">Look at table on left.</span> Column removed: ' + cIndex);
}
