// Sum contents of input1.txt grouped by blank lines, find maximum group

var contents = System.IO.File.ReadAllLines("input1.txt");

var calorieCounts = new List<int>();

var currentMax = 0;
var currentSum = 0;
foreach(var calorie in contents) {
    if (calorie == String.Empty)
    {
        calorieCounts.Add(currentSum);
        currentSum = 0;
    }
    else {
        currentSum += Int32.Parse(calorie);
    }
}
currentMax = calorieCounts.Max();

Console.WriteLine($"Maximum Calorie Count: {currentMax}");

// Part 2: Sum the top N
var sumTop = 3;

var sumTopTotal = calorieCounts.OrderDescending().Take(sumTop).Sum();

Console.WriteLine($"Sum of top {sumTop}: {sumTopTotal}");