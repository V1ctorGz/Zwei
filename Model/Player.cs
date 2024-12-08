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

    // Adiciona uma carta à mão do jogador
    public void DrawCard(Card card)
    {
        Hand.Add(card);
    }

    // Remove uma carta específica da mão
    public bool PlayCard(Card card)
    {
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            return true; // Carta foi jogada
        }
        return false; // Carta não estava na mão
    }

    // Exibe a mão do jogador como texto
    public override string ToString()
    {
        return $"{Name}'s hand: {string.Join(", ", Hand)}";
    }
}


