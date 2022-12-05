// file: 
// part 1: crate locations
// part 2: instructions
// move X from Y to Z


var test = false;
var part = 2;

var instructionSrc = "test-instructions.txt";
var stackRaw = """
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 
""";

if (!test)
{
    instructionSrc = "input-instructions.txt";
    stackRaw = """
[V]     [B]                     [F]
[N] [Q] [W]                 [R] [B]
[F] [D] [S]     [B]         [L] [P]
[S] [J] [C]     [F] [C]     [D] [G]
[M] [M] [H] [L] [P] [N]     [P] [V]
[P] [L] [D] [C] [T] [Q] [R] [S] [J]
[H] [R] [Q] [S] [V] [R] [V] [Z] [S]
[J] [S] [N] [R] [M] [T] [G] [C] [D]
 1   2   3   4   5   6   7   8   9 
""";
}



var stackInfo = stackRaw.ReplaceLineEndings().Split(System.Environment.NewLine).Reverse();
// first line is stackIds
var stackIds = stackInfo.First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
Stack<char>[] stacks = new Stack<char>[stackIds.Length+1];

Console.WriteLine(stackRaw);

// Parse crate structure into stacks
var stackCount = Int32.Parse(stackIds.Last());
foreach(var stackInfoLine in stackInfo.Skip(1)) {
    for (int i = 1; i <= stackCount; i++) {
        // initialise stack
        if (stacks[i] is null) stacks[i] = new Stack<char>();
        var stackInfoIndex = i == 1 ? 1 : (i - 1) * 4 + 1;
        var crateId = stackInfoLine[stackInfoIndex];
        // Console.WriteLine($"{(byte)crateId}");
        if (crateId > 32)
        {
            stacks[i].Push(crateId);
            // Console.WriteLine($"Read: {stackInfoIndex} Push: {crateId} to {i}");
        }
    }
}


if (part == 1)
{
    Console.WriteLine("Process instructions... (part 1)");

    var instructions = System.IO.File.ReadAllLines(instructionSrc);
    foreach (var instruction in instructions)
    {
        // Console.WriteLine(instruction);
        // split on spaces:
        // 1: count
        // 3: src
        // 5: dest
        var commands = instruction.Split(' ');

        for (int i = 0; i < Int32.Parse(commands[1]); i++)
        {
            var crate = stacks[Int32.Parse(commands[3])].Pop();
            stacks[Int32.Parse(commands[5])].Push(crate);
        }

    }


} else
{


    Console.WriteLine("Process instructions... (part 2)");

    // now the movements are all together, not one at a time


    var instructions = System.IO.File.ReadAllLines(instructionSrc);
    foreach (var instruction in instructions)
    {
        // Console.WriteLine(instruction);
        // split on spaces:
        // 1: count
        // 3: src
        // 5: dest
        var commands = instruction.Split(' ');

        var buffer = new Stack<char>();
        for (int i = 0; i < Int32.Parse(commands[1]); i++)
        {
            var crate = stacks[Int32.Parse(commands[3])].Pop();
            buffer.Push(crate);
        }

        while(buffer.TryPop(out char crate)) {
            stacks[Int32.Parse(commands[5])].Push(crate);
        }
    }

}
Console.WriteLine("Top of stacks...");
for (int i = 1; i <= stackCount; i++) {
    Console.Write($"{stacks[i].Peek()}");
}