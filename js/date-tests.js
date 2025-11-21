//var jsonString = '{"dateTime": "2022-04-13T14:00:00"}'; // JSON string representing 2022-04-13T14:00:00 ET - in my local time which is Eastern Time
var jsonString = '{"dateTime": "2022-04-13T18:00:00Z"}'; // JSON string representing 2022-04-13T18:00:00Z UTC - this is UTC/GMT time

// Parsing the JSON string to an object
var jsonObject = JSON.parse(jsonString);

// Converting the date string to a Date object
var dateObject = new Date(jsonObject.dateTime);

// Output the Date object
console.log("toString()          ", dateObject.toString()); //           Wed Apr 13 2022 14:00:00 GMT-0400 (Eastern Daylight Time)
console.log("toISOString()       ", dateObject.toISOString()); //        2022-04-13T18:00:00.000Z
console.log("toLocaleDateString()", dateObject.toLocaleDateString()); // 4/13/2022
console.log("toLocaleString()    ", dateObject.toLocaleString()); //     4/13/2022, 2:00:00 PM
console.log("toLocaleTimeString()", dateObject.toLocaleTimeString()); // 2:00:00 PM
console.log("toTimeString()      ", dateObject.toTimeString()); //       14:00:00 GMT-0400 (Eastern Daylight Time)
console.log("toUTCString()       ", dateObject.toUTCString()); //        Wed, 13 Apr 2022 18:00:00 GMT
