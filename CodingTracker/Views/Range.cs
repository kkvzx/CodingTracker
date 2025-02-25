using System.Security.Cryptography;

namespace CodingTracker.Views;

public class Range(DateTime from, DateTime to)
{
    public DateTime From { get; } = from;
    public DateTime To { get; } = to;
}
