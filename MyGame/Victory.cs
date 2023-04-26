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
            VictoryMessage victorymessage = new VictoryMessage(score, tilesplaced);
            AddGameObject(victorymessage);
        }
    }
}
