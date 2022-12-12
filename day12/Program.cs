// Move from S(a) to E(z)
// - moving 1 space at a time
// - moving no more than 1 height

using Zenseless.PathFinder;
using Zenseless.PathFinder.Grid;

var lines = System.IO.File.ReadAllLines("input.txt");

// grid[col, row]

var grid = new int[lines[0].Length, lines.Length];


List<Coord> starts = new List<Coord>();

Coord end = Coord.Null;


int col = 0, row = 0;
foreach(var line in lines) {
    foreach(var pos in line.AsSpan()) {
        var height = (int)pos - 96;
        if (pos == 'S') {
            height = 1;
        } else if (pos == 'E') {
            end = new Coord(col, row);
            height = 26;
        }
        // Console.WriteLine($"{row},{col}: {height}");
        grid[col, row] = height;

        if (height == 1)
            starts.Add(new Coord(col, row));
        // Console.Write($" {height:00} ");
        col++;
    }
    // Console.WriteLine();
    row++;
    col = 0;
}

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

var shortestpath = 10000;
foreach (var start in starts)
{
    var path = Algorithms.Dijkstra<Coord>(start, end, walkableNeighbours, neigbourCost, Coord.Null);

    var pathlen = path.Last().Path.Count();

    shortestpath = pathlen > 0 && pathlen < shortestpath ? pathlen : shortestpath;
    Console.WriteLine($"Path length: {path.Last().Path.Count()}");

}

Console.WriteLine($"Shortest path: {shortestpath}");