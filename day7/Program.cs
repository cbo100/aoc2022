// parse console output
// construct tree size(s)
// calculate all directory sizes
var commands = System.IO.File.ReadAllLines("input.txt");

Node root = new Node(null, 'd', "", 0);
Node curr = root;
List<Node> all = new List<Node>();

// start at second command, the first is `cd /`
for (int i = 1; i < commands.Length; i++) {
    var command = commands[i];
    // Console.WriteLine(command);
    if (command.StartsWith("$ cd ")) {
        // going to directory
        var parts = command.Split(' ');
        if (parts[2] == "..")
        {
            curr = curr.Parent ?? throw new Exception($"{curr.Name} has no parent");
        } else {
            curr = curr.Children.Single(x => x.Name == parts[2]);
        }

    } else if (command == "$ ls") {
        // listing children
        // do nothing...
    } else {
        // node definition
        var type = 'f';
        if (command.StartsWith("dir "))
            type = 'd';

        var parts = command.Split(' ');
        var node = new Node(curr,
                                   type,
                                   parts[1],
                                   type == 'd' ? 0 : long.Parse(parts[0]));
        curr.Children.Add(node);
        all.Add(node);
    }
}

Console.WriteLine($"Root size: {root.SizeIncludingChildren()}");


// part 1: find sum of directories with size < 100_000
var selected = all
    .Where(x => x.SizeIncludingChildren() < 100_000 && x.Type == 'd')
    .Sum(x => x.SizeIncludingChildren());

Console.WriteLine($"Sum of selected: {selected}");

// part 2: find size of smallest directory to delete
//          to free up 30_000_000
var requiredSpace = 30_000_000;
var totalSpace = 70_000_000;
var usedSpace = root.SizeIncludingChildren();

var spaceToFree = requiredSpace - (totalSpace - usedSpace);
Console.WriteLine($"Need to free {spaceToFree}");

var deletionCandidate = all
    .Where(x => x.SizeIncludingChildren() >= spaceToFree)
    .OrderBy(x => x.SizeIncludingChildren())
    .First();

Console.WriteLine($"Delete {deletionCandidate.Name} with size {deletionCandidate.SizeIncludingChildren()}");


public class Node {
    public Node(Node? parent, char type, string name, long size) {
        Parent = parent;
        Type = type;
        Name = name;
        Size = size;
        // Console.WriteLine($"Creating {Name} with Size: {Size} within {Parent?.Name ?? "root"}");
    }
    public char Type { get; set; }
    public Node? Parent { get; set; }
    public long Size { get; set; } = 0;
    public string Name { get; set; } = null!;
    public List<Node> Children { get; } = new List<Node>();

    public long SizeIncludingChildren() {
        var sizeOfChildren = Children.Sum(x => x.SizeIncludingChildren());
        // Console.WriteLine($"Checking size of {Name}: {sizeOfChildren} + {Size}");
        return sizeOfChildren + Size;
    }
}