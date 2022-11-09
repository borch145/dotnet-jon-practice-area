using System;
using System.Xml.Serialization;
using Xunit;
using InterfaceDemo2;
using InterfaceDemo2.Models;
using System.Security.Cryptography.X509Certificates;

namespace RPS_Tests
{
    public class GameTests
    {
        [Theory]
        [InlineData(Choice.Paper, Choice.Paper, GameResult.Tie)]
        [InlineData(Choice.Paper, Choice.Rock, GameResult.Player1Win)]
        [InlineData(Choice.Paper, Choice.Scissors, GameResult.Player2Win)]
        [InlineData(Choice.Rock, Choice.Paper, GameResult.Player2Win)]
        [InlineData(Choice.Rock, Choice.Rock, GameResult.Tie)]
        [InlineData(Choice.Rock, Choice.Scissors, GameResult.Player1Win)]
        [InlineData(Choice.Scissors, Choice.Paper, GameResult.Player1Win)]
        [InlineData(Choice.Scissors, Choice.Rock, GameResult.Player2Win)]
        [InlineData(Choice.Scissors, Choice.Scissors, GameResult.Tie)]
        public void Game_GetResult_CanGetCorrectResult(Choice p1Choice, Choice p2Choice, GameResult expectedResult)
        {
            Game game = new Game();
            GameResult actualResult = game.GetGameResult(p1Choice, p2Choice);

            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData(Choice.Paper, Choice.Paper, GameResult.Tie, "P1: Paper P2: Paper \nPaper matches Paper. It's a Tie!")]
        [InlineData(Choice.Paper, Choice.Rock, GameResult.Player1Win, "P1: Paper P2: Rock \nPaper beats Rock. Player 1 Wins!")]
        [InlineData(Choice.Paper, Choice.Scissors, GameResult.Player2Win, "P1: Paper P2: Scissors \nScissors beats Paper. Player 2 Wins!")]
        [InlineData(Choice.Rock, Choice.Paper, GameResult.Player2Win, "P1: Rock P2: Paper \nPaper beats Rock. Player 2 Wins!")]
        [InlineData(Choice.Rock, Choice.Rock, GameResult.Tie, "P1: Rock P2: Rock \nRock matches Rock. It's a Tie!")]
        [InlineData(Choice.Rock, Choice.Scissors, GameResult.Player1Win, "P1: Rock P2: Scissors \nRock beats Scissors. Player 1 Wins!")]
        [InlineData(Choice.Scissors, Choice.Paper, GameResult.Player1Win, "P1: Scissors P2: Paper \nScissors beats Paper. Player 1 Wins!")]
        [InlineData(Choice.Scissors, Choice.Rock, GameResult.Player2Win, "P1: Scissors P2: Rock \nRock beats Scissors. Player 2 Wins!")]
        [InlineData(Choice.Scissors, Choice.Scissors, GameResult.Tie, "P1: Scissors P2: Scissors \nScissors matches Scissors. It's a Tie!")]
        public void Game_GetResultMessage(Choice p1Choice, Choice p2Choice, GameResult result, string expectedString)
        {
            Game game = new Game();
            string actualString = game.GetResultMessage(result, p1Choice, p2Choice);

            Assert.Equal(expectedString, actualString);



        }
    }
   
}
