using Zwei.Model;

namespace Zwei.Controller;

internal class TurnManager
{
    private GameManager _gameManager;
    public TurnManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void PlayTurn()
    {
        {

            var currentPlayer = _gameManager.GetCurrentPlayer();
            Console.WriteLine($"\nTurno de {currentPlayer.Name}");
            Console.WriteLine($"Mão atual: {string.Join(", ", currentPlayer.Hand)}");

            // Exibe a carta no topo da pilha de descarte
            Console.WriteLine($"Carta no topo da pilha de descarte: {_gameManager.GetDiscardPile().Peek()}");

            // Solicita ao jogador que escolha uma carta válida para jogar
            Console.WriteLine("Escolha a carta que deseja jogar (digite o índice da carta, começando de 0):");
            for (int i = 0; i < currentPlayer.Hand.Count; i++)
            {
                Console.WriteLine($"{i}: {currentPlayer.Hand[i]}");
            }

            int chosenIndex;
            bool validChoice = false;

            // Loop para garantir que a escolha do jogador seja válida
            while (!validChoice)
            {
                if (int.TryParse(Console.ReadLine(), out chosenIndex) && chosenIndex >= 0 && chosenIndex < currentPlayer.Hand.Count)
                {
                    Card chosenCard = currentPlayer.Hand[chosenIndex];

                    // Verifica se a carta escolhida é válida para jogar
                    if (chosenCard.Color == _gameManager.GetDiscardPile().Peek().Color || chosenCard.Value == _gameManager.GetDiscardPile().Peek().Value)
                    {
                        currentPlayer.PlayCard(chosenCard);
                        _gameManager.GetDiscardPile().Push(chosenCard);
                        Console.WriteLine($"{currentPlayer.Name} jogou {chosenCard}");
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Carta inválida. Escolha outra carta que combine com a cor ou valor.");
                    }
                }
                else
                {
                    Console.WriteLine("Escolha inválida. Por favor, digite um número válido.");
                }
            }

            NextPlayer(); // Passa para o próximo jogador
        }
    }

    private void NextPlayer()
    {
        if (_gameManager.ReverseOrder)
            _gameManager.CurrentPlayerIndex = (_gameManager.CurrentPlayerIndex - 1 + _gameManager.Players.Count) % _gameManager.Players.Count;
        else
            _gameManager.CurrentPlayerIndex = (_gameManager.CurrentPlayerIndex + 1) % _gameManager.Players.Count;
    }


    public void ApplySpecialCardEffects(Card specialCard)
    {


    }

}
