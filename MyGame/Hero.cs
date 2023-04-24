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
    {//ToDo make click animation make spell cast sprite, enemy and loot spawns and background
        //draw enemies, potion, chests, background, and a wand with spells and a click animation
        private const int attackdelay = 100;
        private int _attacktimer;
        private int _movetimer;
        private const int movedelay = 25;
        private readonly Sprite _sprite = new Sprite();
        private Vector2f pos;
        private Tile_Spawner tilespawner = new Tile_Spawner();
        public Vector2f GetPos()
        {
            return pos;
        }
        public Hero(Vector2f pos)
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
            _sprite.Position = pos;
            this.pos = pos;
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Game.RenderWindow.Close();
            }    
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
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&movedelay<=_movetimer)
            {
                //Console.WriteLine("Click");
                Vector2i intclick = Mouse.GetPosition();
                Vector2f floatclick = new Vector2f((float)intclick.X,(float)intclick.Y);
                CursorClick cursorclick = new CursorClick(floatclick);
                Game.CurrentScene.AddGameObject(cursorclick);
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
                _movetimer=0;
            }
            if (up)
            {//movement north 
                x -= 32;
                y -=16;
                if (-30<=y&&0<=x)
                {
                    tilespawner.SpawnThreetilesSouth(new Vector2f(x-22, y+32));
                    
                    _sprite.Texture = Game.GetTexture("../../../Resources/John North.png");
                }
                else 
                {
                    x+=32;
                    y+=16;
                }
            }
            if (left)
            { //movement west
                x -= 32;
                y +=16;
                if (0<=x&&y<=1030)
                {
                    tilespawner.SpawnThreetilesEast(new Vector2f(x-22, y+32));
                    
                    _sprite.Texture = Game.GetTexture("../../../Resources/John West.png");
                }
                else 
                {
                    x+=32;
                    y-=16;
                }
            }
            if (down)
            { //movement south
                x += 32;
                y +=16;
                if (x<=1900&&y<=1030)
                {
                    tilespawner.SpawnThreetilesNorth(new Vector2f(x - 22, y + 32));
                    
                    _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
                }
                else
                {
                    x-=32;
                    y-=16;
                }

            }
            if (right) 
            { //movement east
                x += 32;
                y -=16;
                if (x<=1900&&-30<=y)
                {
                    tilespawner.SpawnThreetilesWest(new Vector2f(x - 22, y + 32));
                    _sprite.Texture = Game.GetTexture("../../../Resources/John East.png");
                }
                else
                { 
                    x-=32;
                    y+=16;
                }
            }
            if (nowhere)
            {
                //nothing happens
            }
            up = false;
            down = false;
            left = false;
            right= false;
            nowhere = false;
            _sprite.Position = new Vector2f(x, y);
            this.pos = _sprite.Position;
            _movetimer++;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)&&attackdelay<=_attacktimer)
            { 
                
                
                _attacktimer= 0;
            }
            _attacktimer++;
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
