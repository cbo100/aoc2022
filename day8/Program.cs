var tmp = System.IO.File.ReadAllLines("input.txt");

var data = new List<Tree>();

int x = 0, y = 0;


foreach(var line in tmp) {
    var heights = tmp[y].ToCharArray();
    x = 0;
    foreach(var height in heights) {
        data.Add(new Tree(x, y, Int16.Parse(height.ToString())));
        x++;
    }
    y++;
}


var visible = 0;

var maxVertical = data.Max(v => v.Y);
var maxHorizontal = data.Max(h => h.X);
var maxScenic = 0;

Console.WriteLine($"Max X: {maxHorizontal}, Max Y: {maxVertical} ");

foreach(var tree in data) {

    var row = data.Where(x => x.Y == tree.Y);
    var column = data.Where(x => x.X == tree.X);

    var up = column.Where(x => x.Y < tree.Y).OrderByDescending(x => x.Y);
    var down = column.Where(x => x.Y > tree.Y).OrderBy(x => x.Y);

    var left = row.Where(x => x.X < tree.X).OrderByDescending(x => x.X);
    var right = row.Where(x => x.X > tree.X).OrderBy(x => x.X);

    var visibleUp = up.All(t => t.Height < tree.Height);
    var visibleDown = visibleUp || down.All(t => t.Height < tree.Height);
    var visibleLeft = visibleDown || left.All(t => t.Height < tree.Height);
    var visibleRight = visibleLeft || right.All(t => t.Height < tree.Height);
    if (visibleUp || visibleDown || visibleLeft || visibleRight) visible++;



    var viewUpLimit = up.FirstOrDefault(x => x.Height >= tree.Height)?.Y ?? 0;
    var viewUp = tree.Y - viewUpLimit;

    var viewDownLimit = down.FirstOrDefault(x => x.Height >= tree.Height)?.Y ?? maxVertical;
    var viewDown = viewDownLimit - tree.Y;


    var viewLeftLimit = left.FirstOrDefault(x => x.Height >= tree.Height)?.X ?? 0;
    var viewLeft = tree.X - viewLeftLimit;

    var viewRightLimit = right.FirstOrDefault(x => x.Height >= tree.Height)?.X ?? maxHorizontal;
    var viewRight = viewRightLimit - tree.X;

    var scenic = viewUp * viewDown * viewLeft * viewRight;
    maxScenic = Math.Max(scenic, maxScenic);
}

Console.WriteLine($"{visible} trees");
Console.WriteLine($"{maxScenic} scenic");

record Tree(int X, int Y, int Height);