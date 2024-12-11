using System;
using System.Collections.Generic;
using Zwei.Controller;
using Zwei.Model;

namespace Zwei
{
    class Program
    {
        static void Main(string[] args)
        {
            // Solicita os nomes dos jogadores
            Console.WriteLine("Digite os nomes dos jogadores, separados por vírgula (ex: João, Maria, Pedro):");
            var playerNames = Console.ReadLine()?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (playerNames == null || playerNames.Length < 2)
            {
                Console.WriteLine("É necessário pelo menos 2 jogadores para iniciar o jogo.");
                return;
            }

            // Cria uma lista de nomes de jogadores
            List<string> playersList = new List<string>();
            foreach (var name in playerNames)
            {
                playersList.Add(name.Trim());
            }

            // Inicializa o GameManager com a lista de jogadores
            GameManager gameManager = new GameManager(playersList);
            TurnManager turnManager = new TurnManager(gameManager);
            
            Player winner = null;
            while (winner == null)
            {
                turnManager.PlayTurn();
                winner = gameManager.CheckWinner();

                if (winner != null)
                {
                    Console.WriteLine($"\nParabéns! {winner.Name} venceu o jogo!");
                    break;
                }
            }

            Console.WriteLine("Jogo encerrado. Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}