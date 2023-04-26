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
        private int _movetimer;
        private const int movedelay = 50;//35
        private readonly Sprite _sprite = new Sprite();//make loot sprites spell sprites make clicks twice as big (now 4 times| half the size)
        private List<Vector2f> placedtiles = new List<Vector2f>();
        public Enemy(Vector2f pos,List<Vector2f> tiles)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");
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
            float hx = heropos.X;//enemy goes to the hero spawn pos
            float hy = heropos.Y;

            if (movedelay<=_movetimer)
            {
                /*if (hx < x&&hy<y)
                {
                    //movement north 
                    x -= 32;
                    y -=16;
                    bool move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                        {
                            if (-30<=y&&0<=x)
                            {
                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                            }
                        }
                    }
                    if (!move)
                    {
                        x+=32;
                        y+=16;
                    }
                }
                if (hx<x&&y<hy)
                {
                    //movement west
                    x -= 32;
                    y +=16;
                    bool move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                        {
                            if (0<=x&&y<=1030)
                            {

                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                            }
                        }
                    }
                    if (!move)
                    {
                        x+=32;
                        y-=16;
                    }
                }
                if (x<hx&&y<hy)
                {
                    //movement south
                    x += 32;
                    y +=16;
                    bool move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                        {
                            if (x<=1900&&y<=1030)
                            {

                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");//facing to the south
                            }

                        }
                    }
                    if (!move)
                    {


                        x-=32;
                        y-=16;

                    }

                }

                if (x < hx&&hy<y)
                {
                    //movement east
                    x += 32;
                    y -=16;
                    bool move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                        {
                            if (x<=1900&&-30<=y)
                            {
                                move=true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy east.png");//facing to the east
                            }
                        }
                    }
                    if (!move)
                    {
                        x-=32;
                        y+=16;
                    }




                }
                if (hx ==x&&hy!=y)
                {
                    if (hy<y)
                    {
                        //movement north
                        x -= 32;
                        y -=16;
                        bool move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                            {
                                if (-30<=y&&0<=x)
                                {
                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                                }
                            }
                        }
                        if (!move)
                        {
                            x+=32;
                            y+=16;
                        }
                        //movement east

                    }
                    if (y<hy)
                    {
                        //movement south
                        //movement west
                        x -= 32;
                        y +=16;
                        bool move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                            {
                                if (0<=x&&y<=1030)
                                {

                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                                }
                            }
                        }
                        if (!move)
                        {
                            x+=32;
                            y-=16;
                        }
                    }
                }
                if (hy==y && hx !=x)
                {
                    if (hx<x)
                    {
                        //movement north
                        x -= 32;
                        y -=16;
                        bool move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                            {
                                if (-30<=y&&0<=x)
                                {
                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                                }
                            }
                        }
                        if (!move)
                        {
                            x+=32;
                            y+=16;
                        }
                        //movement west
                        x -= 32;
                        y +=16;
                        move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                            {
                                if (0<=x&&y<=1030)
                                {

                                    move = true;
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                                }
                            }
                        }
                        if (!move)
                        {
                            x+=32;
                            y-=16;
                        }
                    }
                    if (x<hx)
                    { 
                        //movement south
                        //movement east
                    }
                }
                if (hy==y&&hx==x)
                {
                    MakeDead();
                    scene.DecreaseLives();
                  
                }
                */
                float dx = 0f;
                float dy = 0f;
                if (x<hx)
                {
                    dx = hx -x;
                }
                else if (hx<x)
                {
                    dx = x-hx;
                }
                if (y<hy)
                {
                    dy = hy -y;
                }
                else if (hy<y)
                {
                    dy = hy-y;
                }
                float fdx = 0f;
                float fdy = 0f;
                //movement north 
                x -= 32;
                y -=16;
                bool move = false;
                for (int num = 0; num<placedtiles.Count; num++)
                {
                    float ptx = placedtiles[num].X;
                    float pty = placedtiles[num].Y;
                    if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                    {
                        if (-30<=y&&0<=x)
                        {
                            if (x<hx)
                            {
                                fdx = hx -x;
                            }
                            else if (hx<x)
                            {
                                fdx = x-hx;
                            }
                            if (y<hy)
                            {
                                fdy = hy -y;
                            }
                            else if (hy<y)
                            {
                                fdy = hy-y;
                            }
                            if (fdx<dx&&fdy<dy)
                            {
                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                            }
                        }
                    }
                }
                if (!move)
                {
                    x+=32;
                    y+=16;
                }
                //movement west
                x -= 32;
                y +=16;
                move = false;
                for (int num = 0; num<placedtiles.Count; num++)
                {
                    float ptx = placedtiles[num].X;
                    float pty = placedtiles[num].Y;
                    if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                    {
                        if (0<=x&&y<=1030)
                        {
                            if (x<hx)
                            {
                                fdx = hx -x;
                            }
                            else if (hx<x)
                            {
                                fdx = x-hx;
                            }
                            if (y<hy)
                            {
                                fdy = hy -y;
                            }
                            else if (hy<y)
                            {
                                fdy = hy-y;
                            }
                            if (fdx<dx&&fdy<dy)
                            {
                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                            }
                        }
                    }
                }
                if (!move)
                {
                    x+=32;
                    y-=16;
                }
                //movement south
                x += 32;
                y +=16;
                move = false;
                for (int num = 0; num<placedtiles.Count; num++)
                {
                    float ptx = placedtiles[num].X;
                    float pty = placedtiles[num].Y;
                    if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                    {
                        if (x<=1900&&y<=1030)
                        {
                            if (x<hx)
                            {
                                fdx = hx -x;
                            }
                            else if (hx<x)
                            {
                                fdx = x-hx;
                            }
                            if (y<hy)
                            {
                                fdy = hy -y;
                            }
                            else if (hy<y)
                            {
                                fdy = hy-y;
                            }
                            if (fdx < dx && fdy < dy)
                            {
                                move = true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");//facing to the south
                            }
                        }

                    }
                }
                if (!move)
                {


                    x-=32;
                    y-=16;

                }
                //movement east
                x += 32;
                y -=16;
                move = false;
                for (int num = 0; num<placedtiles.Count; num++)
                {
                    float ptx = placedtiles[num].X;
                    float pty = placedtiles[num].Y;
                    if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)
                    {
                        if (x<=1900&&-30<=y)
                        {
                            if (x<hx)
                            {
                                fdx = hx -x;
                            }
                            else if (hx<x)
                            {
                                fdx = x-hx;
                            }
                            if (y<hy)
                            {
                                fdy = hy -y;
                            }
                            else if (hy<y)
                            {
                                fdy = hy-y;
                            }
                            if (fdx < dx && fdy < dy)
                            {
                                move=true;
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy east.png");//facing to the east
                            }
                        }
                    }
                }
                if (!move)
                {
                    x-=32;
                    y+=16;
                }
                _movetimer =0;

            }
            
             
               _sprite.Position = new Vector2f(x, y);
               _movetimer++;
        }
        
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("spell"))
            {
                otherGameObject.MakeDead();
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.IncreaseScore();
            }
            MakeDead();
        }

    }
}
