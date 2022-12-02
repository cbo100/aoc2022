// Two column file
// A = X = Rock = 1
// B = Y = Paper = 2
// C = Z = Scissors = 3
// Them , Me
// Win = 6
// Draw = 3
// Lose = 0

var games = System.IO.File.ReadAllLines("test.txt");
// var games = System.IO.File.ReadAllLines("input.txt");

/// Part 1
var score = 0;
foreach(var game in games) {
    var results = game.Split(' ');
    
    var them = results[0] switch {
        "A" => 1,
        "B" => 2,
        "C" => 3,
        _ => throw new Exception()
    };
    var us = results[1] switch {
        "X" => 1,
        "Y" => 2,
        "Z" => 3,
        _ => throw new Exception()
    };

    score += us;
    score += new GameResult(them, us) switch {
        { Them: var a, Us: var b} when a == b => 3,
        { Them: var a, Us: var b} when b == 1 && a == 3 => 6,
        { Them: var a, Us: var b} when a == 1 && b == 3 => 0,
        { Them: var a, Us: var b} when b > a => 6,
        _ => 0
    };

    // Console.WriteLine($"Current score: {them} {us} {score}");

}

Console.WriteLine($"Your part 1 score is: {score}");


/// part 2
/// 
// Two column file
// A = Rock = 1
// B = Paper = 2
// C = Scissors = 3
// X = Lose
// Y = Draw
// Z = Win
// Them , Me
// Win = 6
// Draw = 3
// Lose = 0

score = 0;
foreach(var game in games) {
    var results = game.Split(' ');
    
    var them = results[0] switch {
        "A" => 1,
        "B" => 2,
        "C" => 3,
        _ => throw new Exception()
    };

    var us = results[1] switch {
        "X" when them == 1 => 3, // lose
        "X" => them - 1, // lose
        "Y" => them, // draw
        "Z" when them == 3 => 1, // win
        "Z" => them + 1, // win
        _ => throw new Exception()
    };

    score += us;
    score += new GameResult(them, us) switch {
        { Them: var a, Us: var b} when a == b => 3,
        { Them: var a, Us: var b} when b == 1 && a == 3 => 6,
        { Them: var a, Us: var b} when a == 1 && b == 3 => 0,
        { Them: var a, Us: var b} when b > a => 6,
        _ => 0
    };

    // Console.WriteLine($"Current score: {them} {us} {score}");

}

Console.WriteLine($"Your part 1 score is: {score}");


public record GameResult(int Them, int Us);
