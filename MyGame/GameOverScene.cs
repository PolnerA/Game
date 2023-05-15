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
        public GameOverScene(int score,int tilesplaced,bool spellbook)//gets the data from scene 
        {
            GameOverMessage gameOverMessage = new GameOverMessage(score,tilesplaced,spellbook);//displays score and tiles placed in the game over message text, spellbook gets passed back to gamescene when enter is pressed
            AddBackground(gameOverMessage);//adds game over message as a background to the scene
        }
    }
}
