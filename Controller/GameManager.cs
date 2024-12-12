using System;
using System.Collections.Generic;
using System.Linq;
using Zwei.Model;

namespace Zwei.Controller;

internal class GameManager
{

    internal Deck Deck; // O baralho do jogo
    internal List<Player> Players; // Lista de jogadores
    internal Stack<Card> DiscardPile; // Pilha de descarte
    internal int CurrentPlayerIndex; // Índice do jogador atual
    internal bool ReverseOrder; // Controla se a ordem está invertida
    internal TurnManager _turnmanager;

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

    private void StartGame()
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (var player in Players)
            {
                player.DrawCard(Deck.Cards.Last());
                Deck.Cards.RemoveAt(Deck.Cards.Count - 1);
            }
        }

        Card firstCard = Deck.Cards.Last();
        Deck.Cards.RemoveAt(Deck.Cards.Count - 1);
        DiscardPile.Push(firstCard);

        Console.WriteLine($"Primeira carta da pilha: {firstCard}");
    }

    // Retorna o jogador atual
    public Player GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }

    // Verifica se há um vencedor
    public Player CheckWinner()
    {
        return Players.FirstOrDefault(player => player.Hand.Count == 0)!;
    }

    public Stack<Card> GetDiscardPile()
    {
        return DiscardPile;
    }
}

