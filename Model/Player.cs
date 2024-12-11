namespace Zwei.Model;

internal class Player
{

    public string Name { get; set; }
    public List<Card> Hand { get; private set; } 

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
    }

    public void DrawCard(Card card)
    {
        Hand.Add(card);
    }

    public bool PlayCard(Card card)
    {
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{Name}'s hand: {string.Join(", ", Hand)}";
    }
}


