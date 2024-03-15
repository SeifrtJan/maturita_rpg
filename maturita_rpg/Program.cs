// See https://aka.ms/new-console-template for more information
using maturita_rpg;
using System.IO.MemoryMappedFiles;

Game game = new Game();
while (true)
{
    game.GameLogic();
}

