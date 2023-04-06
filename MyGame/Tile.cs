using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace MyGame
{
    
    class Tile : GameObject
    {
        private int _timer;
        private readonly Sprite tile = new Sprite();
        public Tile(Vector2f pos)
        {
            tile.Texture = Game.GetTexture("../../../Resources/64X32tile.png");
            tile.Position = pos;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(tile);
           
        }
        public override void Update(Time elapsed)
        {
            
        }
    }
}
