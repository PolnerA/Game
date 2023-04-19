using GameEngine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Security.Cryptography;

namespace MyGame
{
    class Enemy : GameObject
    {
        private const float Speed = 0.3f;
        private int _movetimer;
        Random rng = new Random();
        private const int movedelay = 100;
        private readonly Sprite _sprite = new Sprite();//make enemy sprites, and the loot
        private List<Vector2f> placedtiles = new List<Vector2f>();
        public Enemy(Vector2f pos,List<Vector2f> tiles)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/Enemy.png");
            _sprite.Position = new Vector2f(pos.X+22,pos.Y-32);//same way of standing on tile as hero
            placedtiles=tiles;
            AssignTag("Enemy");
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f pos = _sprite.Position;
            Vector2f heropos = scene.GetHeroPos();
            float x = pos.X;
            float y = pos.Y;
            float hx = heropos.X;
            float hy = heropos.Y;

            if (movedelay<=_movetimer)
            {
                int direction = rng.Next(4);
                //if (x < hx&&y<hy)
                //{
                //movement north 
                switch (direction)
                {
                    case 0:
                    x -= 32;
                    y -=16;
                        bool move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            if (placedtiles[num]==new Vector2f(x-22, y+32))
                            {
                                if (-30<=y&&0<=x)
                                {
                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/Enemy.png");//facing to the north texture
                                }
                            }
                        }
                        if (!move)
                        {
                            x+=32;
                            y+=16;
                        }
                        break;
                    //}
                    //if (hx<x&&y<hy)
                    //{
                    //movement west
                    case 1:
                    x -= 32;
                    y +=16;
                    move = false;
                    for(int num=0;num<placedtiles.Count;num++)
                    {
                        if (placedtiles[num]==new Vector2f(x-22, y+32))
                        {
                            if (0<=x&&y<=1030)
                            {

                                    move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/Enemy.png");//facing to the west texture
                            }
                        }
                    }
                        if (!move)
                        {
                            x+=32;
                            y-=16;
                        }
                    break;
                //}
                //if (x<hx&&y<hy)
                //{
                //movement south
                    case 2:
                    x += 32;
                    y +=16;
                         move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            if (placedtiles[num]==new Vector2f(x - 22, y + 32))
                            {
                                if (x<=1900&&y<=1030)
                                {

                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/Enemy.png");//facing to the south
                                }
                                
                            }
                        }
                        if (!move)
                        {
                        
                       
                                x-=32;
                                y-=16;
                        
                        }
                      
                        break;
                    //}

                //if (hx < x&&hy<y)
                //{
                //movement east
                    case 3:
                        x += 32;
                        y -=16;
                        move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            if (placedtiles[num]==new Vector2f(x - 22, y + 32))
                            {
                                if (x<=1900&&-30<=y)
                                {
                                    move=true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/Enemy.png");//facing to the east
                                }
                            }
                        }
                        if (!move)
                        {
                            x-=32;
                            y+=16;
                        }
                        break;

                }

                      
                   //}
                   
                   _movetimer =0;
             }
               _sprite.Position = new Vector2f(x, y);
               _movetimer++;
        }

        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
    }
}
