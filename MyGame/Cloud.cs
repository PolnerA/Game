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
    class Cloud : GameObject
    {
        private const float Speed = 0.3f;
        private readonly Sprite _sprite = new Sprite();
        public Cloud(Vector2f pos)
        {
            //clouds
            _sprite.Texture = Game.GetTexture("../../../Resources/cloud.png");//cloud gets a postion at random by cloud_spawner and goes across the screen 
            _sprite.Position = pos;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();//goes across the screen if the x is less than the negative value of the width it removes it from _cloud
            Vector2f pos = _sprite.Position;
            if (pos.X < _sprite.GetGlobalBounds().Width * -1)
            {
                MakeDead();
            }
            else
            {
                _sprite.Position = new Vector2f(pos.X - Speed * msElapsed, pos.Y);//if it is still in it changes the position by the speed
            }
        }
    }
}
