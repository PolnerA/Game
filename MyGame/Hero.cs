﻿using GameEngine;
using SFML.Audio;
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
        //attack delay and attack timer for the cooldowns in between attacks (100ms)

        private const int attackdelay = 100;
        private int _attacktimer;
        
        //move delay and timer for movement (25 ms)
        private int _movetimer;
        private const int movedelay = 25;

        //music delay and timer and music for period in between movment (8 seconds)
        private readonly Sound music = new Sound();
        private const int musicdelay = 8000;
        private int musictimer = musicdelay;
        
        //sprite for the character and a position for the enemy to know where to move
        private readonly Sprite _sprite = new Sprite();
        private Vector2f pos;
        
        //tilespawner to spawn tiles
        private Tile_Spawner tilespawner = new Tile_Spawner();

        //direction for the spell's direction
        private int direction = -1;            // -1: nowehere 0:North 1:east 2:south 3:west 4:northwest 5: southwest 6:southeast 7: northeast

        //Function allows scene to get the heros position to then pass along to Enemy
        public Vector2f GetPos()
        {
            return pos;
        }

        //creates a hero at the position it is supposed to be at in GameScene
        public Hero(Vector2f pos)
        { // Game Object rendering depends on Y position
            _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
            _sprite.Position = pos;
            this.pos = pos;
            music.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortExploration.wav");
            //Sets up position, texture, and the sound buffer
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);

        }
        public bool IsMusicPlaying()
        { 
            return music.Status==SoundStatus.Playing;
        }
        public override void Update(Time elapsed)
        {
            musictimer++;
            GameScene scene = (GameScene)Game.CurrentScene;
            if (0<scene.GetNumOfEnemies())
            {
                music.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortCreeping.wav");
            }
            else
            {
                music.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortExploration.wav");
            }
            if (musicdelay<=musictimer)
            {
                music.Play();
                musictimer=0;
            }
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
                Vector2f floatclick = new Vector2f(intclick.X,intclick.Y);
                CursorClick cursorclick = new CursorClick(floatclick);
                
                Game.CurrentScene.AddUserInterface(cursorclick);
                for (int i = 0; i<middletile.Count; i++)
                {
                    Vector2f tile = middletile[i];
                    Vector2i midtile = new Vector2i((int)tile.X, (int)tile.Y);
                    Vector2i southtile = new Vector2i((int)tile.X+32, (int)tile.Y+16);
                    Vector2i westtile = new Vector2i((int)tile.X-32, (int)tile.Y+16);
                    Vector2i northtile = new Vector2i((int)tile.X-32, (int)tile.Y-16);
                    Vector2i easttile = new Vector2i((int)tile.X+32, (int)tile.Y-16);

                    //Console.WriteLine(inttile.X+","+inttile.Y);
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
                Tile redtile = new Tile(new Vector2f(x-22, y+32), "red");//make legitimate red and purple tile textures (current ones aren't pngs)
                Game.CurrentScene.AddTile(redtile);
                x -= 32;
                y -=16;
                tilespawner.SpawnThreetilesSouth(new Vector2f(x-22, y+32));
                Tile purpletile = new Tile(new Vector2f(x-22, y+32),"purple");
                Game.CurrentScene.AddTile(purpletile);
                _sprite.Texture = Game.GetTexture("../../../Resources/John North.png");
                direction =0;
                
            }
            if (left)
            { //movement west
                x -= 32;
                y +=16;
               
                _sprite.Texture = Game.GetTexture("../../../Resources/John West.png");
                direction =3;
            }
            if (down)
            { //movement south
                
                x += 32;
                y +=16;
                tilespawner.SpawnThreetilesNorth(new Vector2f(x - 22, y + 32));
               
                _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");
                direction =2;
               

            }
            if (right) 
            { //movement east
              
                x += 32;
                y -=16;
                tilespawner.SpawnThreetilesWest(new Vector2f(x - 22, y + 32));
               
                _sprite.Texture = Game.GetTexture("../../../Resources/John East.png");
                direction =1;
                
            }
            if (nowhere)
            {
                direction =-1;
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
                Spell spell = new Spell(pos, direction);
                Game.CurrentScene.AddCloud(spell);
                _attacktimer= 0;
            }
            if (scene.GetSpellBook())
            {
                //direction  -1: nowehere 0:North 1:east 2:south 3:west 4:northwest 5: southwest 6:southeast 7: northeast
                if (Keyboard.IsKeyPressed(Keyboard.Key.Q)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 0);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 7);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.E)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 1);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 4);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 6);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Z)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 3);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.X)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 5);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.C)&&attackdelay<=_attacktimer)
                {
                    Spell spell = new Spell(pos, 2);
                    Game.CurrentScene.AddCloud(spell);
                    _attacktimer= 0;
                }
            }
            _attacktimer++;
            SetPosition(new Vector2f(_sprite.Position.X-22,_sprite.Position.Y+32));
        }
    }
}
