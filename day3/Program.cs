// Files, divide line, find common letter
// Calculate priority of duplicates:
// a-z == 1-26
// A-Z == 27-52
// a-z: 97-122 (-96)
// A-Z: 65-90 (-38)
Console.WriteLine($"a-z: {Convert.ToByte('a')}-{Convert.ToByte('z')}");
Console.WriteLine($"A-Z: {Convert.ToByte('A')}-{Convert.ToByte('Z')}");

var bags = System.IO.File.ReadAllLines("input.txt");

var sum = 0;
foreach(var bag in bags) {
    var first = bag.AsSpan().Slice(0, bag.Length / 2);
    var second = bag.AsSpan().Slice(bag.Length / 2);
    var common = second[second.IndexOfAny(first)];
    var commonPriority = common switch
    {
        ( < 'a' ) => Convert.ToByte(common) - 38,
        _ => Convert.ToByte(common) - 96
    };
    // Console.WriteLine($"{common} - {commonPriority}");
    sum += commonPriority;

}

Console.WriteLine($"Sum of priority: {sum}");


// Part 2
sum = 0;
for (int bag = 0; bag < bags.Length - 2; bag+=3)
{
    var bag1 = bags[bag].AsSpan();
    var bag2 = bags[bag + 1].AsSpan();
    var bag3 = bags[bag + 2].AsSpan();
    // Console.WriteLine($"{bag1}");
    // Console.WriteLine($"{bag2}");
    // Console.WriteLine($"{bag3}");
    foreach(var item in bag1) {
        if (bag2.IndexOf(item) >= 0 && bag3.IndexOf(item) >= 0) {
            // Console.WriteLine($"{item}");
            sum += item switch
            {
                ( < 'a' ) => Convert.ToByte(item) - 38,
                _ => Convert.ToByte(item) - 96
            };
            break;
        }
    }

}

Console.WriteLine($"Badge Priority: {sum}");