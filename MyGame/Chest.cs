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
    class Chest : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Chest(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/chest.png");//create texture
            _sprite.Position = new Vector2f(pos.X+0, pos.Y-0);//pixels adjusted for chest pos compared to tile pos
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {

        }
    }
}
