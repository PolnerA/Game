using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
namespace MyGame
{
    class GameOverScene : Scene
    {
        public GameOverScene(int score,int tilesplaced,bool spellbook)
        {
            GameOverMessage gameOverMessage = new GameOverMessage(score,tilesplaced,spellbook);
            AddBackground(gameOverMessage);
        }
    }
}
