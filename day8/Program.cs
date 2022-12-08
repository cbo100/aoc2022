var tmp = System.IO.File.ReadAllLines("input.txt");

var data = new List<Tree>();

int vertical = 0, horizontal = 0;


foreach(var line in tmp) {
    var heights = tmp[vertical].ToCharArray();
    horizontal = 0;
    foreach(var height in heights) {
        data.Add(new Tree(vertical, horizontal, Int16.Parse(height.ToString())));
        horizontal++;
    }
    vertical++;
}


var visible = 0;

var maxVertical = data.Max(v => v.Vertical);
var maxHorizontal = data.Max(h => h.Horizontal);
var maxScenic = 0;

foreach(var tree in data) {

    var row = data.Where(x => x.Vertical == tree.Vertical);
    var column = data.Where(x => x.Horizontal == tree.Horizontal);

    var up = column.Where(x => x.Vertical < tree.Vertical).OrderByDescending(x => x.Vertical);
    var down = column.Where(x => x.Vertical > tree.Vertical).OrderBy(x => x.Vertical);

    var left = row.Where(x => x.Horizontal < tree.Horizontal).OrderByDescending(x => x.Horizontal);
    var right = row.Where(x => x.Horizontal > tree.Horizontal).OrderBy(x => x.Horizontal);

    var visibleUp = up.All(t => t.Height < tree.Height);
    var visibleDown = visibleUp || down.All(t => t.Height < tree.Height);
    var visibleLeft = visibleDown || left.All(t => t.Height < tree.Height);
    var visibleRight = visibleLeft || right.All(t => t.Height < tree.Height);
    if (visibleUp || visibleDown || visibleLeft || visibleRight) visible++;



    var viewUpLimit = up.FirstOrDefault(x => x.Height >= tree.Height)?.Vertical ?? 0;
    var viewUp = tree.Vertical - viewUpLimit;

    var viewDownLimit = down.FirstOrDefault(x => x.Height >= tree.Height)?.Vertical ?? maxVertical;
    var viewDown = viewDownLimit - tree.Vertical;


    var viewLeftLimit = left.FirstOrDefault(x => x.Height >= tree.Height)?.Horizontal ?? 0;
    var viewLeft = tree.Horizontal - viewLeftLimit;

    var viewRightLimit = right.FirstOrDefault(x => x.Height >= tree.Height)?.Horizontal ?? maxHorizontal;
    var viewRight = viewRightLimit - tree.Horizontal;

    var scenic = viewUp * viewDown * viewLeft * viewRight;
    maxScenic = Math.Max(scenic, maxScenic);
}

Console.WriteLine($"{visible} trees");
Console.WriteLine($"{maxScenic} scenic");

record Tree(int Vertical, int Horizontal, int Height);