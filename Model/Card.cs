using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace Zwei.Model;

internal class Card
{
    public string Color { get; set; }

    public CardValue Value { get; set; }

    public Card(string color, CardValue value)
    {
        Color = color;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Color} {Value}";
    }
}
