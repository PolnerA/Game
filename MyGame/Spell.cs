using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Spell : GameObject
    {
        private const float Speed = 0.3f;
        private readonly Sprite _sprite = new Sprite();
        Spell(Vector2f pos,int direction)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/laser.png");
            _sprite.Position = pos;
        }
        public override void Update(Time elapsed)
        {

        }
    }
}
