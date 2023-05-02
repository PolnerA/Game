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
            _sprite.Position = new Vector2f(pos.X, pos.Y);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f heropos = scene.GetHeroPos();
            heropos = new Vector2f(heropos.X-22, heropos.Y+32);
            if (heropos == _sprite.Position)
            {
                for (int i = 0; i<10; i++)
                {
                    scene.IncreaseScore();
                }
                MakeDead();
            }
        }
    }
}
