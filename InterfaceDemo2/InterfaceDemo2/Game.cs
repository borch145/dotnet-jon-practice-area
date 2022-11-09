using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using InterfaceDemo2.ChoiceGetters;
using InterfaceDemo2.Models;

namespace InterfaceDemo2
{
    public class Game
    {
       


        public void Play(IChoiceGetter player1, IChoiceGetter player2)
        {
            Choice p1Choice = player1.GetChoice();
            Choice p2Choice = player2.GetChoice();
            GameResult result = GetGameResult(p1Choice, p2Choice);
            string resultMessage = GetResultMessage(result, p1Choice, p2Choice);
            Console.WriteLine(resultMessage);
            Console.ReadKey();
        }

        public GameResult GetGameResult(Choice p1Choice, Choice p2choice)
        {
            GameResult result;

            if (p1Choice == p2choice)
            {
                result = GameResult.Tie;
            }
            else if (
                    (p1Choice == Choice.Rock && p2choice == Choice.Scissors) ||
                    (p1Choice == Choice.Paper && p2choice == Choice.Rock) ||
                    (p1Choice == Choice.Scissors && p2choice == Choice.Paper)
                    )
            {
                result = GameResult.Player1Win;
            }


            else
            {
                result = GameResult.Player2Win;
            }
            return result;
        }

        public string GetResultMessage(GameResult result, Choice p1Choice, Choice p2Choice)
        {
            string message = $"P1: {p1Choice} P2: {p2Choice} \n";

            switch (result)
            {
                case GameResult.Tie:
                     message += $"{p1Choice} matches {p2Choice}. It's a Tie!" ;
                    break;
                case GameResult.Player1Win:
                    message += $"{p1Choice} beats {p2Choice}. Player 1 Wins!";
                    break;
                case GameResult.Player2Win:
                    message += $"{p2Choice} beats {p1Choice}. Player 2 Wins!";
                    break;
                default:
                    throw new Exception("Error in AnnounceResult: didn't hit a known GameResult case.");
            }
            return message;

        }
    }
}
