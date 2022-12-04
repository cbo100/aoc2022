// find all lines where one range contains the other entirely
// 2-4, 5-7 // no overlap
// 2-6, 3-5 // fully contains

var pairs = System.IO.File.ReadAllLines("input.txt");

var contains = 0;
var overlaps = 0;
foreach(var pair in pairs) {
    var data = pair.Split(',');

    var first = Int32.Parse(data[0].Split('-')[0]);
    var second = Int32.Parse(data[0].Split('-')[1]);

    var firstRange = Enumerable.Range(first, second - first + 1).ToArray().AsSpan();

    var third = Int32.Parse(data[1].Split('-')[0]);
    var fourth = Int32.Parse(data[1].Split('-')[1]);

    var secondRange = Enumerable.Range(third, fourth - third + 1).ToArray().AsSpan();

    // // part 1: fully contains
    if ((firstRange.Contains(secondRange[0]) && firstRange.Contains(secondRange[^1]))
     || (secondRange.Contains(firstRange[0])) && secondRange.Contains(firstRange[^1]))
    {
        contains++;
    } 

    // part 2: overlaps
    if ((firstRange.Contains(secondRange[0]) || firstRange.Contains(secondRange[^1]))
     || (secondRange.Contains(firstRange[0])) || secondRange.Contains(firstRange[^1]))
    {
        overlaps++;
    } 
}

Console.WriteLine($"Fully Contains: {contains}");
Console.WriteLine($"Overlaps: {overlaps}");
