// Moving Head, Chasing Tail
HashSet<Pos> visitedHead = new();
HashSet<Pos> visitedTail = new();


var knotCount = 2;

// Pos head = new(0,0);
// Pos tail = new(0,0);
Pos[] knots = Enumerable.Repeat(new Pos(0, 0), knotCount).ToArray();


// movements
var moves = System.IO.File.ReadAllLines("test.txt");


// move knots

foreach (var move in moves)
{
    var direction = move.Split(' ')[0];
    var distance = int.Parse(move.Split(' ')[1]);
    for (var moved = 0; moved < distance; moved++)
    {
        knots[0] = MoveKnot(knots[0], direction);
        var leader = knots[0];
        for (int follower = 1; follower < knots.Length; follower++)
        {
            var knot = knots[follower];
            knots[follower] = MoveFollowingKnot(leader, knot);
            leader = knots[follower];
        }
        visitedHead.Add(knots[0]);
        visitedTail.Add(knots[knots.Length - 1]);
    }
    RenderMove(move, knots);

}

Console.WriteLine($"Tail visited: {visitedTail.Count} positions");
Console.WriteLine($"Head visited: {visitedHead.Count} positions");

static void RenderMove(string move, Pos[] knots){
    Console.WriteLine();
    Console.WriteLine($"== {move} ==");
    Console.WriteLine();

    // foreach(var knot in knots) {
    //     Console.WriteLine(knot);
    // }

    for (int y = 6; y > -6; y--) {
        for (int x = 6; x > -6; x--) {
            var foundKnot = false;
            for (int k = 0; k < knots.Length; k++) {
                var knot = knots[k];
                if (knot.X == x && knot.Y == y) {
                    Console.Write(k == 0 ? "H" : k.ToString());
                    foundKnot = true;
                    break;
                }
            }
            if (!foundKnot) Console.Write('.');
        }
        Console.WriteLine();
    }


}

static Pos MoveKnot(Pos head, string direction)
{

    // Move the head
    if (direction == "U")
    {
        // UP y++
        head = new(head.X, head.Y + 1);
    }
    else if (direction == "D")
    {
        // DOWN y--
        head = new(head.X, head.Y - 1);
    }
    else if (direction == "L")
    {
        // LEFT x--
        head = new(head.X + 1, head.Y);
    }
    else
    {
        // RIGHT: x++
        head = new(head.X - 1, head.Y);
    }

    return head;
}

static Pos MoveFollowingKnot(Pos head, Pos tail)
{

    // Update position of tail
    var distX = Math.Abs(head.X - tail.X);
    var distY = Math.Abs(head.Y - tail.Y);
    if ((distX <= 1) && (distY <= 1))
    {
        // do not move tail
    }
    else
    {
        // Console.WriteLine($"Dist: {distX},{distY}");
        if (distX > 1 && distY == 0)
        {
            // catch up horizonally
            tail = new Pos(tail.X + (tail.X > head.X ? -1 : 1), tail.Y);
        }
        else if (distX == 0 && distY > 1)
        {
            // catch up vertically
            tail = new Pos(tail.X, tail.Y + (tail.Y > head.Y ? -1 : 1));
        }
        else if (distX > 1 && distY == 1)
        {
            // catch up diagonally
            tail = new Pos(tail.X + (tail.X > head.X ? -1 : 1), head.Y);
        }
        else if (distX == 1 && distY > 1)
        {
            // catch up diagonally
            tail = new Pos(head.X, tail.Y + (tail.Y > head.Y ? -1 : 1));
        }
    }

    return tail;

}

public record Pos(int X, int Y);