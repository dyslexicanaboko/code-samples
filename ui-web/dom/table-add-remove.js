function vjsPrint(text) {
    var span = document.getElementById("spanForGet");

    span.innerHTML = text;
}

function jqPrint(text) {
    var span = $("#spanForGet2");

    span.html(text);
}
/*
    This is a compound control
        input text box is used to add new items
        table row buttons are used to remove target items
        this version does not perform input validation, so duplicates are not prevented
*/

//Keep track of the ids issued for new items. Don't bother reusing id numbers
var _global = {
    ItemCount: 0
};

function btnAddItemOnClick() {
    var txt = document.getElementById("txtAddItem");
    var tbody = document.getElementById("tbodyItems");

    var rIndex = tbody.rows.length;

    //New rows are different from existing, denoted by the trExistingX versus trNewX
    var rowId = "trNew" + _global.Items;

    _global.Items++; //Incrememnt the count for the ids issued

    // Create an empty <tr> element and add it to the end of the table
    var row = tbody.insertRow(rIndex);
    row.id = rowId;

    // Create new cells to fill the row
    var tdItem = row.insertCell(0); //Item cell
    var tdX = row.insertCell(1); //Remove button cell

    //Create the button
    var btn = document.createElement("button");
    btn.textContent = "X"; //X to denote removal
    btn.addEventListener("click", function(){ btnRemoveItemOnClick(rowId); }); //Wire up to remove event with element Id set already

    tdItem.innerHTML = txt.value; //Item value (string text)
    tdX.appendChild(btn); //Inserting the button element into the cell
}

//Since the id is provided up front, removal is straight forward, no search needed
function btnRemoveItemOnClick(rowId) {
    document.getElementById(rowId).remove();
}