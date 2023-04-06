using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Hero: GameObject
    {
        private const float speed = 0.3f;
        private const int attackdelay = 300;
        private const int jumpdelay = 1000;
        private int _attacktimer;
        private int jumpduration = 0;
        private int _jumptimer;
        private int _movetimer;
        private int _milliseconds;
        private const int movedelay = 5;
        private readonly Sprite _sprite = new Sprite();

        public Hero(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
            _sprite.Position = pos;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);

        }
        public override void Update(Time elapsed)
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;
            bool nowhere = false;
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            Vector2f middletilepos = new Vector2f(x-22, y+32);
            float tilex = middletilepos.X;
            float tiley = middletilepos.Y;
            //Gets the pixels for the middle tile
            List<Vector2f> middletile = new List<Vector2f>();
            int Y = 1;
            for (int numofXpixels = 4; numofXpixels<=60; numofXpixels +=4)
            {//grid is a bit north east of the orginal tile (directly above)
                int X = (64-numofXpixels)/2;
                for (int Xvalues = X; X<=Xvalues+numofXpixels; X++)
                {
                    middletile.Add(new Vector2f(middletilepos.X+X,middletilepos.Y+ Y));
                }
                Y++;
            }
            for (int numofXpixels = 60; 4<=numofXpixels; numofXpixels -=4)
            {
                int X = (64-numofXpixels)/2;
                for (int Xvalues = X; X<=Xvalues+numofXpixels; X++)
                {
                    middletile.Add(new Vector2f(middletilepos.X +X,middletilepos.Y+ Y));
                }
                Y++;
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                //Console.WriteLine("Click");
                for (int i = 0; i<middletile.Count; i++)
                {
                    Vector2f tile = middletile[i];
                    Vector2i midtile = new Vector2i((int)tile.X, (int)tile.Y);
                    Vector2i southtile = new Vector2i((int)tile.X+32, (int)tile.Y+16);
                    Vector2i westtile = new Vector2i((int)tile.X-32, (int)tile.Y+16);
                    Vector2i northtile = new Vector2i((int)tile.X-32, (int)tile.Y-16);
                    Vector2i easttile = new Vector2i((int)tile.X+32, (int)tile.Y-16);

                    //Console.WriteLine(inttile.X+","+inttile.Y);
                    //Console.WriteLine(Mouse.GetPosition());//mouse position is on the screen not in game. to offset this I did put the window location to -11,-45 which should also get rid of the white bars on the edges
                    if (Mouse.GetPosition() == midtile)
                    {
                        nowhere = true;
                        break;
                    }
                    if (Mouse.GetPosition() == southtile)
                    {
                        down = true;
                        break;
                    }
                    if (Mouse.GetPosition() == westtile)
                    {
                        left = true;
                        break;
                    }
                    if (Mouse.GetPosition() == northtile)
                    {
                        up = true;
                        break;
                    }
                    if (Mouse.GetPosition() == easttile)
                    {
                        right = true;
                        break;
                    }
                }
            }
            if (up&&movedelay<=_movetimer)
            {//movement north 
                x -= 32;
                y -=16;
                if (-30<=y&&0<=x)
                {
                    Tile_Spawner spawntiles = new Tile_Spawner();
                    spawntiles.SpawnThreetilesSouth(new Vector2f(x-22, y+32));
                    _movetimer=0;
                    _sprite.Texture = Game.GetTexture("../../../Resources/John North.png");
                }
                else 
                {
                    x+=32;
                    y+=16;
                }
            }
            if (left&&movedelay<=_movetimer)
            { //movement west
                x -= 32;
                y +=16;
                if (0<=x&&y<=670)
                {
                    Tile_Spawner spawntiles = new Tile_Spawner();
                    spawntiles.SpawnThreetilesEast(new Vector2f(x-22, y+32));
                    _movetimer =0;
                    _sprite.Texture = Game.GetTexture("../../../Resources/John West.png");
                }
                else 
                {
                    x+=32;
                    y-=16;
                }
            }
            if (down&&movedelay<=_movetimer)
            { //movement south
                x += 32;
                y +=16;
                if (x<=1060&&y<=670)
                {
                    Tile_Spawner spawntiles = new Tile_Spawner();
                    spawntiles.SpawnThreetilesNorth(new Vector2f(x-22, y+32));
                    _movetimer = 0;
                    _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
                }
                else
                {
                    x-=32;
                    y-=16;
                }

            }
            if (right&&movedelay<=_movetimer) 
            { //movement east
                x += 32;
                y -=16;
                if (x<=1060&&-30<=y)
                {
                    Tile_Spawner spawntiles = new Tile_Spawner();

                    spawntiles.SpawnThreetilesWest(new Vector2f(x-22, y+32));
                    _movetimer =0;
                    _sprite.Texture = Game.GetTexture("../../../Resources/John East.png");
                }
                else
                { 
                    x-=32;
                    y+=16;
                }
            }
            if (nowhere&&movedelay<=_movetimer)
            {
                _sprite.Texture = Game.GetTexture("../../../Resources/John.png");
                _movetimer=0;
            }
            up = false;
            down = false;
            left = false;
            right= false;
            nowhere = false;
            _milliseconds++;
            _sprite.Position = new Vector2f(x, y);
            _movetimer++;
           /*
            if (0<jumpduration)
            {
                y-=2f;
                jumpduration-=1;
            }
            int msElapsed = elapsed.AsMilliseconds();
            
            if (y<320&&jumpduration==0)//if in the air and not jumping up 
            {
                float yincrease = 0.98f;
                y+= yincrease;//gravity -9.8m/s/s ->  100 pixels = 1 meter ms = 0.001 seconds | speed is pixels per millisecond gravity| speed down 980 pixels per second per second -> -0.98 pixels per millisecond per second
                if (elapsed.AsSeconds()%1 ==0)
                {
                    yincrease+=0.98f;
                }
            }
            if (_jumptimer >0) { _jumptimer -= msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _jumptimer <=0)
            {
                y-=2f;
                jumpduration=25;
                jumpduration -=1;
                _jumptimer = 1000;
            }
            _sprite.Position = new Vector2f(x, y);
           */
            /*
            if (-_attacktimer >0) { _attacktimer -= msElapsed; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _attacktimer <= 0)
            {
                _attacktimer = attackdelay;
                FloatRect bounds = _sprite.GetGlobalBounds();
                float laserX = x+bounds.Width/2.0f;
                float laserY = y+bounds.Height / 2.0f;
                float laser2Y = y+bounds.Height;
                float laser3y = y;
                Laser laser = new Laser(new Vector2f(laserX, laserY));
                Laser laser2 = new Laser(new Vector2f(laserX, laser2Y));
                Laser laser3 = new Laser(new Vector2f(laserX, laser3y));
                Game.CurrentScene.AddGameObject(laser);
                Game.CurrentScene.AddGameObject(laser2);
                Game.CurrentScene.AddGameObject(laser3);
            }
            */
        }
    }
}
