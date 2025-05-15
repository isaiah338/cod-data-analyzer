using System.IO;
using System.Text;
using Parsing;
using Microsoft.VisualBasic.FileIO;

// See https://aka.ms/new-console-template for more information
//string hello = "Hello, World!";
//Console.WriteLine(hello);

// --- // 

// csv parser
/// https://stackoverflow.com/questions/2081418/parsing-csv-files-in-c-with-header

// --- // 

// date time in unix timestamp
/// https://briancaos.wordpress.com/2022/02/24/c-datetime-to-unix-timestamps/
// --- // 
// Get the offset from current time in UTC time
DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
// Get the unix timestamp in seconds
string unixTime = dto.ToUnixTimeSeconds().ToString();
// Get the unix timestamp in seconds, and add the milliseconds
string unixTimeMilliSeconds = dto.ToUnixTimeMilliseconds().ToString();
int initTime = (int)Int64.Parse(unixTimeMilliSeconds)*-1;
Console.WriteLine(initTime * -1);
/**
 * open csv file
 * create array to hold csv file lines
 * read line by line, putting each line in new array
 * print array size
 */

// Get the offset from current time in UTC time
dto = new DateTimeOffset(DateTime.UtcNow);
// Get the unix timestamp in seconds
unixTime = dto.ToUnixTimeSeconds().ToString();
// Get the unix timestamp in seconds, and add the milliseconds
unixTimeMilliSeconds = dto.ToUnixTimeMilliseconds().ToString();
int parseTimeStart = (int)Int64.Parse(unixTimeMilliSeconds) * -1;
MultiplayerDataParser dataParser = new MultiplayerDataParser();
string[][] values = dataParser.ParseMatchDataCsv("C:\\Users\\isaia\\source\\repos\\cod-data-analyzer\\Parsing\\TestCsvSmall.csv");
// Get the offset from current time in UTC time
dto = new DateTimeOffset(DateTime.UtcNow);
// Get the unix timestamp in seconds
unixTime = dto.ToUnixTimeSeconds().ToString();
// Get the unix timestamp in seconds, and add the milliseconds
unixTimeMilliSeconds = dto.ToUnixTimeMilliseconds().ToString();
int parseTimeEnd = (int)Int64.Parse(unixTimeMilliSeconds) * -1;



Console.WriteLine(MultiplayerDataParser.DATA_COLUMN_HEADERS.Length); // Output: 36

foreach (var value in values)
{
    var i = 0;
    foreach (var v in value)
        Console.WriteLine(MultiplayerDataParser.DATA_COLUMN_HEADERS[i++] + ": " + v);
    Console.WriteLine("\n---\n");    
}



// Get the offset from current time in UTC time
dto = new DateTimeOffset(DateTime.UtcNow);
// Get the unix timestamp in seconds
unixTime = dto.ToUnixTimeSeconds().ToString();
// Get the unix timestamp in seconds, and add the milliseconds
unixTimeMilliSeconds = dto.ToUnixTimeMilliseconds().ToString();
var endTime = (int)Int64.Parse(unixTimeMilliSeconds)*-1;
var totalTime = (initTime - endTime);
var totalParseTime = (parseTimeStart - parseTimeEnd);
Console.WriteLine(
    "start time:\t"+initTime+"\n"
    + "end time:\t"+endTime+"\n"
    + "parsing time:\t"+ totalParseTime + " miliseconds\n"
    + "\t\t"+(totalParseTime/1000)+" seconds\n"
    + "total elapsed:\n\t"+ totalTime + " miliseconds\n"
    + "\t\t"+(totalTime/1000)+" seconds\n\n"
    + "lines in file:\t"+values.Length
    );


// convert string to datetime
// https://stackoverflow.com/questions/919244/converting-a-string-to-datetime
string dateTimeStr = "2025-03-20 15:56:04";
DateTime dateTime = DateTime.Parse(dateTimeStr);
Console.WriteLine($"string DateTime:\t{dateTimeStr}\nConverted DateTime:\t{dateTime.ToString()}");




