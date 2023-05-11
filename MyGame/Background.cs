using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Background : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Background()
        {// background
            _sprite.Texture = Game.GetTexture("../../../Resources/Background.png");//sprite gets a texture and position and remains still
            _sprite.Position = new Vector2f(0, 0);
        }
        public override void Draw()
        {

            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            //still image of the background
        }
    }
}
