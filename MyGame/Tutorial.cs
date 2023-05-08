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
            Tutorial_Text tutorial_ = new Tutorial_Text();
            AddBackground(tutorial_);
        }
    }
}
