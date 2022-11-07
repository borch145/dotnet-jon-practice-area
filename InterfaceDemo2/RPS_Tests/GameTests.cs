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
        [InlineData(Choice.paper, Choice.paper, GameResult.Tie)]
        [InlineData(Choice.paper, Choice.rock, GameResult.Player1Win)]
        [InlineData(Choice.paper, Choice.scissors, GameResult.Player2Win)]
        [InlineData(Choice.rock, Choice.paper, GameResult.Player2Win)]
        [InlineData(Choice.rock, Choice.rock, GameResult.Tie)]
        [InlineData(Choice.rock, Choice.scissors, GameResult.Player1Win)]
        [InlineData(Choice.scissors, Choice.paper, GameResult.Player1Win)]
        [InlineData(Choice.scissors, Choice.rock, GameResult.Player2Win)]
        [InlineData(Choice.scissors, Choice.scissors, GameResult.Tie)]
        public void Game_GetResult_CanGetCorrectResult(Choice p1Choice, Choice p2Choice, GameResult expectedResult)
        {
            Game game = new Game();
            GameResult actualResult = game.GetGameResult(p1Choice, p2Choice);

            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData(Choice.paper, Choice.paper, GameResult.Tie, $"P1: {Choice.paper} P2: {Choice.paper} \n{Choice.paper} matches {Choice.paper}. It's a Tie!")]
        [InlineData(Choice.paper, Choice.rock, GameResult.Player1Win, $"P1: {Choice.paper} P2: {Choice.rock} \n{Choice.paper} beats {Choice.rock}. Player 1 Wins!"]
        [InlineData(Choice.paper, Choice.scissors, GameResult.Player2Win, $"P1: {Choice.paper} P2: {Choice.scissors} \n{Choice.scissors} beats {Choice.paper}. Player 2 Wins!"]
        [InlineData(Choice.rock, Choice.paper, GameResult.Player2Win, $"P1: {Choice.rock} P2: {Choice.paper} \n{Choice.paper} beats {Choice.rock}. Player 2 Wins!"]
        [InlineData(Choice.rock, Choice.rock, GameResult.Tie, $"P1: {Choice.rock} P2: {Choice.rock} \n{Choice.rock} matches {Choice.rock}. It's a Tie!"]
        [InlineData(Choice.rock, Choice.scissors, GameResult.Player1Win, $"P1: {Choice.rock} P2: {Choice.scissors} \n{Choice.rock} beats {Choice.scissors}. Player 1 Wins!")]
        [InlineData(Choice.scissors, Choice.paper, GameResult.Player1Win, $"P1: {Choice.scissors} P2: {Choice.paper} \n{Choice.scissors} beats {Choice.paper}. Player 1 Wins!")]
        [InlineData(Choice.scissors, Choice.rock, GameResult.Player2Win, $"P1: {Choice.scissors} P2: {Choice.rock} \n{Choice.rock} beats {Choice.scissors}. Player 2 Wins!")]
        [InlineData(Choice.scissors, Choice.scissors, GameResult.Tie, $"P1: {Choice.scissors} P2: {Choice.scissors} \n{Choice.scissors} matches {Choice.scissors}. It's a Tie!")]
        public void Game_GetResultMessage(Choice p1Choice, Choice p2Choice, GameResult result, string expectedString)
        {
            Game game = new Game();
            string actualString = game.GetResultMessage(result, p1Choice, p2Choice);

            Assert.Equal(expectedString, actualString);



        }
    }
   
}
