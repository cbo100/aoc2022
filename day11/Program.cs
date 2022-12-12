// simulate monkeys evaluating items based on rules N times

// parse the input
using System.Numerics;

var input = System.IO.File.ReadAllLines("test.txt");


var monkeys = new List<Monkey>();

Monkey current = null!;
for (var line = 0; line < input.Length; line += 7)
{
    // Console.WriteLine(input[line]);
    current = new Monkey();
    current.Id = int.Parse(input[line].Substring(input[line].LastIndexOf(' '), 2).Trim());

    current.Items.AddRange(input[line + 1].Split(':')[1].Split(',', StringSplitOptions.TrimEntries).Select(x => long.Parse(x)));

    var modifier = input[line + 2].Substring(input[line + 2].LastIndexOf(' ')).Trim();
    current.Modifier = int.Parse(modifier == "old" ? "-1" : modifier);
    current.ModifierIsMultipler = input[line + 2].Contains('*');

    current.TestDenominator = int.Parse(input[line + 3].Substring(input[line + 3].LastIndexOf(' ')).Trim());

    current.TrueTarget = int.Parse(input[line + 4].Substring(input[line + 4].LastIndexOf(' ')).Trim());
    current.FalseTarget = int.Parse(input[line + 5].Substring(input[line + 5].LastIndexOf(' ')).Trim());


    monkeys.Add(current);

    // Console.WriteLine($"Monkey {current.Id} has {current.Items.Count} items, modifier {current.Modifier}");
}


var phase = 2;

var rounds = 20;
Func<long, long> process = (x) => x / 3; // phase 1 reduces by 3 after inspection

if (phase == 2)
{
    rounds = 10_000;
    long product = monkeys.Aggregate(1L, (acc, x) => acc * x.TestDenominator);
    process = (x) => x % product; // reduce size of numbers
    Console.WriteLine(product);
}



checked
{
    for (var round = 0; round < rounds; round++)
    {
        if (round % 1000 == 0) //== 1 || round == 20 || round == 1000 || round == 5000)
        {
            Console.WriteLine($"==== after round {round} {DateTime.Now}=====");
            foreach (var monkey in monkeys)
            {
                Console.WriteLine($"Monkey {monkey.Id} inspected {monkey.InspectionCount} items");
            }
        }
        foreach (var monkey in monkeys)
        {
            for (int i = 0; i < monkey.Items.Count; i++)
            {
                var item = monkey.Items[i];
                monkey.InspectionCount++;


                var modifier = monkey.Modifier == -1L ? item : monkey.Modifier;
                if (monkey.ModifierIsMultipler)
                    item = item * modifier;
                else
                    item = item + modifier;

                item = process(item);

                if (item % monkey.TestDenominator == 0)
                    monkeys[monkey.TrueTarget].Items.Add(item);
                else
                    monkeys[monkey.FalseTarget].Items.Add(item);

             
            }
            monkey.Items.Clear();
        }
    }

    Console.WriteLine($"==== after round {rounds} ====");
    foreach (var monkey in monkeys)
    {
        Console.WriteLine($"Monkey {monkey.Id} inspected {monkey.InspectionCount} items");
    }

    var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectionCount).Take(2);
    Console.WriteLine($"Monkey business: {monkeyBusiness.First().InspectionCount * monkeyBusiness.Last().InspectionCount}");

}
public class Monkey
{
    public int Id { get; set; }

    public List<long> Items { get; set; } = new List<long>();
    public long Modifier { get; set; }
    public bool ModifierIsMultipler { get; set; }

    public long TestDenominator { get; set; }

    public int TrueTarget { get; set; }
    public int FalseTarget { get; set; }

    public long InspectionCount { get; set; } = 0;

}