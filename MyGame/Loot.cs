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
    class Loot : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Loot(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/loot.png");//create texture
            _sprite.Position = new Vector2f(pos.X+0, pos.Y-0);//pixels adjusted for loot pos compared to tile pos
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
