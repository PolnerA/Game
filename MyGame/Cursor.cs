using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Cursor : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public override void Update(Time elapsed)//cursor needs to be changed to a sprite which follows the cursor, then on click if it intersects with a tile the character moves in that direction
        {//probably wont work as tile intersections would need to not count the blank pixels
            
        }
    }
}
