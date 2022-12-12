// Move from S(a) to E(z)
// - moving 1 space at a time
// - moving no more than 1 height

using Zenseless.PathFinder;
using Zenseless.PathFinder.Grid;

var lines = System.IO.File.ReadAllLines("input.txt");

// grid[col, row]

var grid = new int[lines[0].Length, lines.Length];


Coord start = Coord.Null, end = Coord.Null;


int col = 0, row = 0;
foreach(var line in lines) {
    foreach(var pos in line.AsSpan()) {
        var height = (int)pos - 96;
        if (pos == 'S') {
            start = new Coord(col, row);
            height = 1;
        } else if (pos == 'E') {
            end = new Coord(col, row);
            height = 26;
        }
        // Console.WriteLine($"{row},{col}: {height}");
        grid[col, row] = height;

        // Console.Write($" {height:00} ");
        col++;
    }
    // Console.WriteLine();
    row++;
    col = 0;
}

Console.WriteLine();
Console.WriteLine();

// for (row = 0; row < grid.GetLength(1); row++) {
//     for (col = 0; col < grid.GetLength(0); col++) {
//      Console.Write($" {grid[col, row]:00} ");   
//     }
//     Console.WriteLine();
// }

// starting at start, find path to end

var neigbourCost = (Coord a, Coord b) =>
{
    return 1.0f;
};

Func<Coord, IEnumerable<Coord>> walkableNeighbours = (Coord a) =>
{
    var height = grid[a.Column, a.Row];
    var neighbours = new List<Coord>();

    // left
    if (a.Column > 0 && grid[a.Column - 1, a.Row] <= height + 1)
        neighbours.Add(new Coord(a.Column - 1, a.Row));

    // right
    if (a.Column < grid.GetLength(0) - 1 && grid[a.Column + 1, a.Row] <= height + 1)
        neighbours.Add(new Coord(a.Column + 1, a.Row));

    // up
    if (a.Row > 0 && grid[a.Column, a.Row - 1] <= height + 1)
        neighbours.Add(new Coord(a.Column, a.Row - 1));

    // down
    if (a.Row < grid.GetLength(1) - 1 && grid[a.Column, a.Row + 1] <= height + 1)
        neighbours.Add(new Coord(a.Column, a.Row + 1));

    return neighbours;
};

var path = Algorithms.BreadthFirst<Coord>(start, end, walkableNeighbours, Coord.Null);

Console.WriteLine($"Path length: {path.Last().Path.Count()}");

// foreach (var info in path) {

//     Console.WriteLine($"Path: {info.Visited.Count()}");
// }

// for (row = 0; row < grid.GetLength(1); row++) {
//     for (col = 0; col < grid.GetLength(0); col++) {
//         if (end.Column == col && end.Row == row) {
//             Console.Write($" EE ");
//         }
//         else if (start.Column == col && start.Row == row) {
//             Console.Write($" SS ");
//         }
//         else if (path.Last().Path.Any(x => x.Column == col && x.Row == row)) {
//             Console.Write($" ## ");
//         } else
//         {
//             Console.Write($" {grid[col, row]:00} ");
//         }
//     }
//     Console.WriteLine();
// }
