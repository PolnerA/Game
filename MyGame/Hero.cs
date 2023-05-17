using GameEngine;
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

        private const int attackdelay = 1000;
        private int _attacktimer=attackdelay;
        
        //move delay and timer for movement (25 ms)
        private int _movetimer;
        private const int movedelay = 100;

        //music delay and timer and music for period in between movment (8 seconds)
        private readonly Sound music = new Sound();
        private const int musicdelay = 8000;
        private int musictimer;
        
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
        public override void Draw()//when asked to draw it draws the sprite into the renderwindow
        {
            Game.RenderWindow.Draw(_sprite);

        }
        public bool IsMusicPlaying()//when requested it can return true or not if the music plays
        { 
            return music.Status==SoundStatus.Playing;
        }
        public override void Update(Time elapsed)
        {
            int mselapsed = elapsed.AsMilliseconds();
            if (0<musictimer)
            {
                musictimer-=mselapsed;
            }
            GameScene scene = (GameScene)Game.CurrentScene;
            if (0<scene.GetNumOfEnemies())//if there are enemies on the scene it changes the music playing
            {
                music.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortCreeping.wav");
            }
            else
            {
                music.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortExploration.wav");
            }
            if (musictimer<=0)//if it has been 8000 milliseconds since the last music it plays and restarts count
            {
                music.Play();
                musictimer=musicdelay;
            }
            //boolean values for deciding the direction
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;
            bool nowhere = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))//if escape is pressed it closes the renderwindow and subsequently the game
            {
                Game.RenderWindow.Close();
            }    
            Vector2f pos = _sprite.Position;//the sprites position is broken down to 2 floats x & y
            float x = pos.X;
            float y = pos.Y;
            Vector2f middletilepos = new Vector2f(x-22, y+32);//gets the tile the hero is standing on
            //Gets the pixels for the middle tile
            List<Vector2f> middletile = new List<Vector2f>();
            
            //Top half of tile
            
            int Y = 1;//starts the y position of the tile at 1
            for (int numofXpixels = 4; numofXpixels<=60; numofXpixels +=4)//tiles are 64 X 32 meaning a 2 to one ratio, the green pixels start at 4 and increase to 60 pixels, (excluding the 2 black pixels on both sides)
            {
                int X = (64-numofXpixels)/2;//x is  the offset compared to the position of the tile
                for (int Xvalues = X; X<=Xvalues+numofXpixels; X++) //gets every x value at the 1 Y position then 2 etc...
                {
                    middletile.Add(new Vector2f(middletilepos.X+X,middletilepos.Y+ Y));//adds the pixel to the middle tile list which contains all the pixels
                }
                Y++;//y increases as it goes down the tile
            }
            
            //Bottom half of tile
            
            for (int numofXpixels = 60; 4<=numofXpixels; numofXpixels -=4)//does the same thing as the top except it starts at the highest point of 60 going down while y continues to increase 
            {
                int X = (64-numofXpixels)/2;
                for (int Xvalues = X; X<=Xvalues+numofXpixels; X++)
                {
                    middletile.Add(new Vector2f(middletilepos.X +X,middletilepos.Y+ Y));
                }
                Y++;
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&_movetimer<=0)//if mouse is pressed, and the character is allowed to move
            {
                Vector2i intclick = Mouse.GetPosition();//gets the mouses position at the location of the click
                Vector2f floatclick = new Vector2f(intclick.X,intclick.Y);//typcasts it  into a float (to start the click animation at that position)
                
                CursorClick cursorclick = new CursorClick(floatclick);//starts click animation
                Game.CurrentScene.AddUserInterface(cursorclick);//adds it to UI above everything
                
                for (int i = 0; i<middletile.Count; i++)//goes through every pixel in the middle tile vector2f list 
                {
                    Vector2f tile = middletile[i];//gets the first tile
                    Vector2i midtile = new Vector2i((int)tile.X, (int)tile.Y);//makes a vector with 2 int values for the tile to compare to mouse.getposition
                    Vector2i southtile = new Vector2i((int)tile.X+32, (int)tile.Y+16);//offsets the pixel by adding x and y values in accordance to the south tile to align with the grid
                    Vector2i westtile = new Vector2i((int)tile.X-32, (int)tile.Y+16);//does it for the west, north and east tiles as well
                    Vector2i northtile = new Vector2i((int)tile.X-32, (int)tile.Y-16);
                    Vector2i easttile = new Vector2i((int)tile.X+32, (int)tile.Y-16);

                    //Console.WriteLine(inttile.X+","+inttile.Y);
                    //if the mouses position matches any one of the pixels in any of the tiles then it turns on a direction boolean and breaks out of the loop (as it will only be on one pixel)
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
                _movetimer = movedelay;//move timer is reset and starts counting down again
            }
            if (up)
            {//movement north 
                Tile redtile = new Tile(new Vector2f(x-22, y+32), "red");//makes the tile moved from red
                Game.CurrentScene.AddTile(redtile);
                x -= 32;//changes the postion of the sprite to be on a tile that is up
                y -=16;
                tilespawner.SpawnThreetilesSouth(new Vector2f(x-22, y+32));//spawns three tiles around it excluding the south one (coming from a southern direction)
                Tile purpletile = new Tile(new Vector2f(x-22, y+32),"purple");//makes the tile moved to purple
                Game.CurrentScene.AddTile(purpletile);
                _sprite.Texture = Game.GetTexture("../../../Resources/John North.png");//changes the sprite to be the texture facing north
                direction =0;//direction is indicated for an attack
                
            }
            if (left)
            { //movement west
                Tile redtile = new Tile(new Vector2f(x-22, y+32), "red");//makes the tile moved from red
                Game.CurrentScene.AddTile(redtile);
                x -= 32;//moves to western tile
                y +=16;
                tilespawner.SpawnThreetilesEast(new Vector2f(x-22, y+32));//comes from east, spawns three tiles excluding the east
                Tile purpletile = new Tile(new Vector2f(x-22, y+32), "purple");//makes the tile moved to purple
                Game.CurrentScene.AddTile(purpletile);
                _sprite.Texture = Game.GetTexture("../../../Resources/John West.png");//changes the texture to the one where he's facing west
                direction =3;//direction is indicated for a spell
            }
            if (down)
            { //movement south
                Tile redtile = new Tile(new Vector2f(x-22, y+32), "red");//makes the tile moved from red
                Game.CurrentScene.AddTile(redtile);
                x += 32;//moves
                y +=16;
                tilespawner.SpawnThreetilesNorth(new Vector2f(x - 22, y + 32));//comes from the north spawns three tiles excluding the north
                Tile purpletile = new Tile(new Vector2f(x-22, y+32), "purple");//makes the tile moved to purple
                Game.CurrentScene.AddTile(purpletile);
                _sprite.Texture = Game.GetTexture("../../../Resources/John South.png");//faces south
                direction =2;//indicates direction for an attack
               

            }
            if (right) 
            { //movement east
                Tile redtile = new Tile(new Vector2f(x-22, y+32), "red");//makes the tile moved from red
                Game.CurrentScene.AddTile(redtile);
                x += 32;//moves there
                y -=16;
                tilespawner.SpawnThreetilesWest(new Vector2f(x - 22, y + 32));//comes from the west spawns three tiles excluding the west (already a tile there)
                Tile purpletile = new Tile(new Vector2f(x-22, y+32), "purple");//makes the tile moved to purple
                Game.CurrentScene.AddTile(purpletile);
                _sprite.Texture = Game.GetTexture("../../../Resources/John East.png");//facing to the east texture
                direction =1;//direction for spell
                
            }
            if (nowhere)
            {
                direction =-1;//direction for spell (Rather lack thereov)
            }
            _sprite.Position = new Vector2f(x, y);//updates the sprite position for the rendering
            this.pos = _sprite.Position;//updates the position for enemy targeting
            if (0<_movetimer&&!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _movetimer-=mselapsed;
            }//move timer counts down the time to next move
            if (_attacktimer<=0)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))//if space is pressed
                {
                    //direction  -1: nowehere 0:North 1:east 2:south 3:west 4:northwest 5: southwest 6:southeast 7: northeast
                    Spell spell = new Spell(pos, direction);//creates a new spell with the indicated direction it's travelling and the position it's starting at
                    Game.CurrentScene.AddCloud(spell);//spell is above the game objects (up in the clouds)
                    _attacktimer= attackdelay;//attack tiemr is reset
                }
                if (scene.GetSpellBook())//if the scene indicates that the spellbook is on 
                {
                    //direction  -1: nowehere 0:North 1:east 2:south 3:west 4:northwest 5: southwest 6:southeast 7: northeast
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Q))//checks if the q key is pressed and if the attack is possible
                    {
                        Spell spell = new Spell(pos, 0);// makes a new spell going north (q is north of s) (compass is slanted)
                        Game.CurrentScene.AddCloud(spell);//adds the spell as a cloud
                        _attacktimer= attackdelay;//resets attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.W))//checks if the w key is pressed and if the attack is possible
                    {
                        Spell spell = new Spell(pos, 7);//creates a new spell going northeast (w is northeast of s)
                        Game.CurrentScene.AddCloud(spell);//adds the spell as a cloud
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.E))//checks if the e key is pressed and if the attack is possible
                    {
                        Spell spell = new Spell(pos, 1);//makes a spell going east (e is east of s)
                        Game.CurrentScene.AddCloud(spell);//adds the spell as a cloud above game objects
                        _attacktimer= attackdelay;//attack timer is reset
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A))//checks if the a key is pressed and if the attack is possible
                    {
                        Spell spell = new Spell(pos, 4);//makes a spell going northwest (a is northwest of s)
                        Game.CurrentScene.AddCloud(spell);//adds the spell as a cloud above the game objects
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D))//checks if the d key is pressed and if the attack is possible (more time in between attacks than the 100)
                    {
                        Spell spell = new Spell(pos, 6);//makes a spell going southeast (d is southeast of s)
                        Game.CurrentScene.AddCloud(spell);//adds it as a cloud
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Z))//checks if the z key is pressed and if the attack is possible (more time in between attacks than the 100)
                    {
                        Spell spell = new Spell(pos, 3);//makes a spell going west (z is west of s)
                        Game.CurrentScene.AddCloud(spell);//adds it as a cloud
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.X))//checks if the x key is pressed and if the attack is possible (more time in between attacks than the 100)
                    {
                        Spell spell = new Spell(pos, 5);//makes a spell going southwest (x is southwest of s)
                        Game.CurrentScene.AddCloud(spell);//adds it as a cloud
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.C))//checks if the c key is pressed and if the attack is possible (more time in between attacks than the 100)
                    {
                        Spell spell = new Spell(pos, 2);//makes a spell going south (c is south of s)
                        Game.CurrentScene.AddCloud(spell);//adds it as a cloud
                        _attacktimer= attackdelay;//resets the attack timer
                    }
                }
            }
            if (0<_attacktimer&&!Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                _attacktimer-=mselapsed;//counts down time till next attack
            }
            SetPosition(new Vector2f(_sprite.Position.X-22,_sprite.Position.Y+32));//sets the position for rendering at the tile position that the sprite is standing on
        }
    }
}
