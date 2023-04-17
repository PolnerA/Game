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
        private const float Speed = 0.3f;
        private readonly Sprite _sprite = new Sprite();
        private readonly Sprite _sprite2 = new Sprite();
        public Background()
        {//make tile background and remodel the animation to be at the top with clouds
            _sprite.Texture = Game.GetTexture("../../../Resources/background.png");
            _sprite2.Texture = Game.GetTexture("../../../Resources/background.png");
            _sprite2.Position = new Vector2f(1080, 0);
            _sprite.Position = new Vector2f(0, 0);
        }
        public override void Draw()
        {

            Game.RenderWindow.Draw(_sprite);
            Game.RenderWindow.Draw(_sprite2);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.Position;
            Vector2f pos2 =_sprite2.Position;
            
            
            float x2 = pos2.X;
            float y2 = pos2.Y;
            float x = pos.X;
            float y = pos.Y;
                     
            
            
            int msElapsed = elapsed.AsMilliseconds();
            
            
            
            
            
             
             
            x -= Speed * msElapsed;
            x2 -= Speed * msElapsed;
            if (x < -1080)
            {
                x+=2160;
            }
            if (x2 <-1080)
            {
                x2+=2160;
            }
            if (1080<x)
            {
                x -= 2160;
            }
            if (1080<x2)
            {
                x2-= 2160;
            }
            _sprite2.Position = new Vector2f(x2,y2);
            _sprite.Position = new Vector2f(x, y);
        }
    }
}
