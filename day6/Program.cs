// find position at end of first 4 unique chars:
using System.Linq;

var inputs = System.IO.File.ReadAllLines("input.txt");


var soplen = 4;
var somlen = 14;


foreach (var input in inputs)
{
    Span<char> data = input.ToCharArray();
    var sop = 0;
    var som = 0;
    for (int i = soplen; i <= data.Length; i++)
    {
        var sopcandidate = data.Slice(i - soplen, soplen);
        if (sop <= 0 && sopcandidate.ToArray().GroupBy(x => x).Count() == soplen)
        {
            sop = i;
        }

        if (i >= somlen)
        {
            var somcandidate = data.Slice(i - somlen, somlen);
            if (som <= 0 && somcandidate.ToArray().GroupBy(x => x).Count() == somlen)
            {
                som = i;
                // break;
            }
        }
    }
    Console.WriteLine($"SOP: {sop}, SOM: {som}");
}

