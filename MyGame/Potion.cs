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
    class Potion : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Potion(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/potion.png");//create texture
            _sprite.Position = new Vector2f(pos.X+25, pos.Y+2);//pixels adjusted for loot pos compared to tile pos
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f heropos = scene.GetHeroPos();
            heropos = new Vector2f(heropos.X+3, heropos.Y+34);
            if (heropos == _sprite.Position)
            {
                scene.IncreaseLives();
                MakeDead();
            }
        }
    }
}
