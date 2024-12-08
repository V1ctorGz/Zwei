using System;
using System.Collections.Generic;
using System.Linq;

namespace Zwei.Model;

internal class GameManager
{

    private Deck Deck; // O baralho do jogo
    private List<Player> Players; // Lista de jogadores
    private Stack<Card> DiscardPile; // Pilha de descarte
    private int CurrentPlayerIndex; // Índice do jogador atual
    private bool ReverseOrder; // Controla se a ordem está invertida

    public GameManager(List<string> playerNames)
    {
        Deck = new Deck();
        Deck.Shuffle(); // Embaralha o baralho
        Players = playerNames.Select(name => new Player(name)).ToList();
        DiscardPile = new Stack<Card>();
        CurrentPlayerIndex = 0; // O primeiro jogador começa
        ReverseOrder = false; // Ordem inicial: normal
        StartGame();
    }

    // Inicializa o jogo distribuindo cartas aos jogadores
    private void StartGame()
    {
        // Cada jogador compra 7 cartas
        for (int i = 0; i < 7; i++)
        {
            foreach (var player in Players)
            {
                player.DrawCard(Deck.Cards.Last());
                Deck.Cards.RemoveAt(Deck.Cards.Count - 1);
            }
        }

        // Coloca a primeira carta do baralho na pilha de descarte
        Card firstCard = Deck.Cards.Last();
        Deck.Cards.RemoveAt(Deck.Cards.Count - 1);
        DiscardPile.Push(firstCard);

        Console.WriteLine($"Primeira carta da pilha de descarte: {firstCard}");
    }

    // Retorna o jogador atual
    private Player GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }

    // Move para o próximo jogador
    private void NextPlayer()
    {
        if (ReverseOrder)
            CurrentPlayerIndex = (CurrentPlayerIndex - 1 + Players.Count) % Players.Count;
        else
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
    }

    // Realiza o turno do jogador atual
    public void PlayTurn()
    {
        {
            var currentPlayer = GetCurrentPlayer();
            Console.WriteLine($"\nTurno de {currentPlayer.Name}");
            Console.WriteLine($"Mão atual: {string.Join(", ", currentPlayer.Hand)}");

            // Exibe a carta no topo da pilha de descarte
            Console.WriteLine($"Carta no topo da pilha de descarte: {DiscardPile.Peek()}");

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
                    if (chosenCard.Color == DiscardPile.Peek().Color || chosenCard.Value == DiscardPile.Peek().Value)
                    {
                        currentPlayer.PlayCard(chosenCard);
                        DiscardPile.Push(chosenCard);
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
 

    // Verifica se há um vencedor
    public Player CheckWinner()
    {
        return Players.FirstOrDefault(player => player.Hand.Count == 0);
    }
}

