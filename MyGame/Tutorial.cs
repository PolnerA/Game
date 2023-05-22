using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Tutorial : Scene
    {
        public Tutorial()
        {
            //when the scene is set to the tutorial
            Tutorial_Text tutorial_ = new Tutorial_Text();
            AddBackground(tutorial_);
            //creates tutorial text in the background which teaches the user how to play the game
        }
    }
}
