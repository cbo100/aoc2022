// Moving Head, Chasing Tail
HashSet<Pos> visitedHead = new();
HashSet<Pos> visitedTail = new();


var knotCount = 10;

// Pos head = new(0,0);
// Pos tail = new(0,0);
Pos[] knots = Enumerable.Repeat(new Pos(0, 0), knotCount).ToArray();


// movements
var moves = System.IO.File.ReadAllLines("input.txt");


// move knots
var debugFrom = 0;
var moveCount = 0;
var debugUntil = 0;
foreach (var move in moves)
{
    if (debugFrom < moveCount && moveCount < debugUntil)
    {
        Console.WriteLine();
        Console.WriteLine($"== {move} ==");
        Console.WriteLine();
    }

    var direction = move.Split(' ')[0];
    var distance = int.Parse(move.Split(' ')[1]);
    for (var moved = 0; moved < distance; moved++)
    {
        knots[0] = MoveKnot(knots[0], direction);
        var leader = 0;
        for (int follower = 1; follower < knots.Length; follower++)
        {
            var newPos = MoveFollowingKnot(knots[leader], knots[follower]);
            // Console.WriteLine($"Moving {follower} from {knots[follower]} to {leader} at {newPos}");
            knots[follower] = newPos;
            leader = follower;
        }

        visitedHead.Add(knots[0]);
        visitedTail.Add(knots[knots.Length - 1]);
        if (debugFrom < moveCount && moveCount < debugUntil) RenderGrid(knots);
    }
    moveCount++;
}

Console.WriteLine($"Tail visited: {visitedTail.Count} positions");
Console.WriteLine($"Head visited: {visitedHead.Count} positions");

static void RenderGrid(Pos[] knots){

    // foreach(var knot in knots) {
    //     Console.WriteLine(knot);
    // }

    for (int y = 6; y > -6; y--) {
        for (int x = 6; x > -6; x--) {
            var foundKnot = false;
            for (int k = 0; k < knots.Length; k++) {
                var knot = knots[k];
                // Console.WriteLine($"Checking knot {k}");
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
    Console.WriteLine();


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
    // Console.WriteLine(head);
    // Console.WriteLine(tail);
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
        else if (distX == 2 && distY > 1)
        {   
            // with lots of knots the tail can disconnect entirely
            tail = new Pos(tail.X + (tail.X > head.X ? -1 : 1), tail.Y + (tail.Y > head.Y ? -1 : 1));
        }
    }

    return tail;

}

public record Pos(int X, int Y);