﻿using GameEngine;
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
        //sprite for the info related to the potion
        private readonly Sprite _sprite = new Sprite();
        public Potion(Vector2f pos)
        {//S Game Object
            _sprite.Texture = Game.GetTexture("../../../Resources/potion.png");//create's texture and position
            _sprite.Position = new Vector2f(pos.X+25, pos.Y+2);//pixels adjusted for loot pos compared to tile pos
        }
        public override void Draw()
        {//when asked to draw it draws the sprite
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            //gets the hero's position
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f heropos = scene.GetHeroPos();
            heropos = new Vector2f(heropos.X+3, heropos.Y+34); //sets the heropos to the sprite pos
            if (heropos == _sprite.Position)
            {//if the hero is at the same place as the potion
                scene.IncreaseLives();
                //increases lives
                MakeDead();
                //stops showing the potion
            }
            SetPosition(new Vector2f(_sprite.Position.X-25,_sprite.Position.Y-2));
            //sets the sprites position for rendering  onto the tile that it's on
        }
    }
}
