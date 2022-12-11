// during execution of asm program
// record value of X register every N cycles

using System.Text;

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
var crt = 0;

var output = new StringBuilder();

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


        // part 2 move CRT back to start of row
        if (ctr % 40 == 0) {
            // reset CRT
            output.Append($"{Environment.NewLine} {during:000} --> ");
            crt = 0;
        }




        // Part 2: render a # or . depending on if the pixel centred on X
        // lines up with the CRT position
        if (crt >= X -1 && crt <= X + 1) {
            output.Append('#');
        }else{
            output.Append('.');
        }


        if (i == latency - 1) {
            switch (inst) {
                case "addx":
                    X += arg;
                    break;
            }
        }

        ctr = during;
        crt++;

    }


}

Console.WriteLine(output.ToString());

Console.WriteLine($"X = {X}, Sum of strength: {sum}");