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
        private readonly Sprite _sprite = new Sprite();//creates a sprite for the loot
        public Loot(Vector2f pos)
        {//Game Object rendered based off of y position
            _sprite.Texture = Game.GetTexture("../../../Resources/loot.png");
            //gives the sprite a texture and position based off the tile it's supposed to be on
            _sprite.Position = new Vector2f(pos.X, pos.Y);
        }
        public override void Draw()
        {//when asked to draw it draws the sprite
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            //gets the heros position
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f heropos = scene.GetHeroPos();
            //if the hero is on the tile fo the loot
            heropos = new Vector2f(heropos.X-22, heropos.Y+32);
            if (heropos == _sprite.Position)
            {
                //increases the score by 10
                for (int i = 0; i<10; i++)
                {
                    scene.IncreaseScore();
                }
                MakeDead();//removes it from the scene with makedead
            }
            //sets the position to the tile it's on
            SetPosition(_sprite.Position);
        }
    }
}
