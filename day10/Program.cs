// during execution of asm program
// record value of X register every N cycles

var lookup = new Dictionary<string, int>()
{
    { "noop", 1 },
    { "addx", 2 }
};


int X = 1;
int ctr = 0;
int during = 0;
var program = System.IO.File.ReadAllLines("input.txt");
var sum = 0;

foreach(var line in program) {
    var inst = line.Split(' ')[0];
    var arg = line.Contains(' ') ? int.Parse(line.Split(' ')[1]) : 0;
    var latency = lookup.Single(x => x.Key == inst).Value;
    // Console.WriteLine($"{inst},{arg}");
    for (int i = 0; i < latency; i++) {
        during++;
        if (during == 20 || (during - 20) % 40 == 0) {
            var strength = X * during;
            // Console.WriteLine($"Cycle: {during}, X: {X}, Strength: {strength}");
            sum += strength;
        }

        if (i == latency - 1) {
            switch (inst) {
                case "addx":
                    X += arg;
                    break;
            }
        }



        ctr = during;


        // Console.WriteLine($"{ctr}... {X}");
    }


}


Console.WriteLine($"X = {X}, Sum of strength: {sum}");