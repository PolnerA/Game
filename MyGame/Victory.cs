using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Victory : Scene
    {
        public Victory(int score, int tilesplaced) 
        {
            //when the game sets the scene to victory
            VictoryMessage victorymessage = new VictoryMessage(score, tilesplaced);
            AddBackground(victorymessage);
            //creates the victory message with the players score and amount of tiles placed
        }
    }
}
