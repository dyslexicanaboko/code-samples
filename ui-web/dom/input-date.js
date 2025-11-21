function getDateValue() {
  const date = document.getElementById("date");

  setCellValue("tdValue", date.value);
  setCellValue("tdValueAsNumber", date.valueAsNumber);
  setCellValue("tdValueAsDate", date.valueAsDate);
}

function setCellValue(id, value) {
  const output = document.getElementById(id);
  output.innerHTML = value;
}
