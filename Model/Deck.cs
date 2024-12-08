namespace Zwei.Model;

internal class Deck
{
    public List<Card> Cards { get; private set; }

    public Deck()
    {
        Cards = new List<Card>();
        InitializeDeck();
    }

    // Gera todas as cartas do UNO e as adiciona ao baralho
    private void InitializeDeck()
    {
        string[] colors = { "Red", "Blue", "Green", "Yellow" };
        foreach (var color in colors)
        {
            
            for (int i = 0; i <= 9; i++)
            {
                Cards.Add(new Card(color, (CardValue)i));
                if (i != 0) Cards.Add(new Card(color, (CardValue)i));
            }

            
            Cards.Add(new Card(color, CardValue.PlusTwo));
            Cards.Add(new Card(color, CardValue.PlusTwo));
            Cards.Add(new Card(color, CardValue.Block));
            Cards.Add(new Card(color, CardValue.Block));
            Cards.Add(new Card(color, CardValue.Invert));
            Cards.Add(new Card(color, CardValue.Invert));
        }

        
        for (int i = 0; i < 4; i++)
        {
            Cards.Add(new Card("Wild", CardValue.PlusFour));
            Cards.Add(new Card("Wild", CardValue.Joker));
        }
    }

    
    public void Shuffle()
    {
        Random rng = new Random();
        Cards = Cards.OrderBy(_ => rng.Next()).ToList();
    }
}

