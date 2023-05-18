using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Tile_Spawner : GameObject
    {
        //tiles that are palced at the beginning of the game are added initially to the list of placedtile.
        private List<Vector2f> placedtiles = new List<Vector2f>() { new Vector2f(100,520), new Vector2f(132, 536), new Vector2f(132, 504), new Vector2f(68, 504), new Vector2f(68, 536) };
        Random rng = new Random();//rng for the spawn of enemies and potions and loot with the tiles
        private bool music=false;//plays when a new tile is spawned
        public Sound sound = new Sound();
        public Tile_Spawner()
        {//sound file for how it will sound
            sound.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortlock.wav");
        }
        public void SpawnThreetiles(Vector2f pos, Vector2f previouspos)
        {
            float x = pos.X;
            float y = pos.Y;
            float previousx = previouspos.X;
            float previousy = previouspos.Y;
            if (previousx+32==x) 
            {
                if (previousy+16==y)//south direction
                {//spawn south,east,west
                    Tile south = new Tile(new Vector2f(x+32,y+16));
                    Tile east = new Tile(new Vector2f(x+32, y-16));
                    Tile west = new Tile(new Vector2f(x-32, y-16));
                }
                else //east direction
                { //spawn south,north,west
                    Tile south = new Tile(new Vector2f(x+32, y+16));
                    Tile north = new Tile(new Vector2f(x-32, y+16));
                    Tile west = new Tile(new Vector2f(x-32, y-16));
                }
            }
            if (previousx-32==x) 
            {
                if (previousy+16==y)//north direction
                {//spawn south,east,west
                    Tile south = new Tile(new Vector2f(x+32, y+16));
                    Tile east = new Tile(new Vector2f(x+32, y-16));
                    Tile west = new Tile(new Vector2f(x-32, y-16));
                }
                else//west direction
                {//spawn south,east,north
                    Tile south = new Tile(new Vector2f(x+32, y+16));
                    Tile east = new Tile(new Vector2f(x+32, y-16));
                    Tile north = new Tile(new Vector2f(x-32, y+16));
                }
            }
        }
        public void SpawnThreetilesNorth(Vector2f pos)//spawns from the 4 tiles around the character except for the one north, so it only spwans three
        {
            bool down=true;
            bool left = true;
            bool right = true;//all the tiles are set to spawn
            GameScene scene = (GameScene)Game.CurrentScene;//sets an instance of game scene to add tiles
            for (int i = 0; i<placedtiles.Count; i++)
            { //goes through the list and if the tileposition in the list is similar to one that will be spawned it's set to false
               Vector2f tilepos = placedtiles[i];
               if (tilepos == new Vector2f(pos.X+32, pos.Y+16))
               {
                   down=false;
               }
               if (tilepos == new Vector2f(pos.X+32, pos.Y-16))
               {
                   right=false;
               }
               if (tilepos == new Vector2f(pos.X-32, pos.Y+16))
               {
                   left=false;
               }
                
            }
            if (down)//if the direction is indicated as true, the tile is created and added to placed tiles, tilehas is also called to see what spawns with it.
            {
               
                Tile south = new Tile(new Vector2f(pos.X+32, pos.Y+16));
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y+16));
                scene.AddTile( south);
                tilehas(new Vector2f(pos.X + 32, pos.Y + 16));
                music=true;
            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                scene.AddTile( east);
                tilehas(new Vector2f(pos.X + 32, pos.Y - 16));
                music=true;
            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));

                scene.AddTile(west);
                tilehas(new Vector2f(pos.X - 32, pos.Y + 16));
                music=true;
            }
            if (music)
            {
                sound.Play();
            }
            music=false;
            scene.SetTilesPlaced(placedtiles.Count());
        }
        public void SpawnThreetilesWest(Vector2f pos)
        {
            bool down = true;
            bool up = true;
            bool right = true;
            GameScene scene = (GameScene)Game.CurrentScene;
            for (int i = 0; i<placedtiles.Count; i++)
            {
                Vector2f tilepos = placedtiles[i];
                if (tilepos == new Vector2f(pos.X+32, pos.Y+16))
                {
                    down=false;
                }
                if (tilepos == new Vector2f(pos.X+32, pos.Y-16))
                {
                    right=false;
                }
                if (tilepos == new Vector2f(pos.X-32, pos.Y-16))
                {
                    up=false;
                }
            }
            if (down)
            {
                Tile south = new Tile(new Vector2f(pos.X+32, pos.Y+16));
                scene.AddTile(south);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y+16));
                tilehas(new Vector2f(pos.X + 32, pos.Y + 16));
                music=true;
            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                scene.AddTile(east);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                tilehas(new Vector2f(pos.X+32, pos.Y-16));
                music=true;
            }
            if (up)
            {
                Tile north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                scene.AddTile(north);
                tilehas(new Vector2f(pos.X-32, pos.Y-16));
                music= true;
            }
            scene.SetTilesPlaced(placedtiles.Count());
            if (music)
            {
                sound.Play();
            }
            music=false;

        }
        public void SpawnThreetilesSouth(Vector2f pos)
        {
            bool left = true;
            bool up = true;
            bool right = true;
            GameScene scene = (GameScene)Game.CurrentScene;
            for (int i = 0; i<placedtiles.Count(); i++)
            {
                Vector2f tilepos = placedtiles[i];
                if (tilepos == new Vector2f(pos.X-32, pos.Y+16))
                {
                    left=false;
                }
                if (tilepos == new Vector2f(pos.X+32, pos.Y-16))
                {
                    right=false;
                }
                if (tilepos == new Vector2f(pos.X-32, pos.Y-16))
                {
                    up=false;
                }
            }
            if (up)
            {
                Tile north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                scene.AddTile(north);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                tilehas(new Vector2f(pos.X-32, pos.Y-16));
                music=true;
            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                scene.AddTile(east);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                tilehas(new Vector2f(pos.X+32, pos.Y-16));
                music=true;
            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                scene.AddTile(west);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));
                tilehas(new Vector2f(pos.X-32, pos.Y+16));
                music=true;
            }
            scene.SetTilesPlaced(placedtiles.Count());
            if (music)
            {
                sound.Play();
            }
            music=false;
        }
        public void SpawnThreetilesEast(Vector2f pos)
        {
            bool left = true;
            bool up = true;
            bool down = true;
            GameScene scene = (GameScene)Game.CurrentScene;
            for (int i = 0; i<placedtiles.Count; i++)
            {
                Vector2f tilepos = placedtiles[i];
                if (tilepos == new Vector2f(pos.X-32, pos.Y+16))
                {
                    left=false;
                }
                if (tilepos == new Vector2f(pos.X+32, pos.Y+16))
                {
                    down=false;
                }
                if (tilepos == new Vector2f(pos.X-32, pos.Y-16))
                {
                    up=false;
                }
            }
            if (down)
            {
                Tile south = new Tile(new Vector2f(pos.X+32, pos.Y+16));
                scene.AddTile(south);
                placedtiles.Add(new Vector2f(pos.X+32,pos.Y+16));
                tilehas(new Vector2f(pos.X+32, pos.Y+16));
                music=true;
            }
            if (up)
            {
                Tile north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                scene.AddTile(north);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                tilehas(new Vector2f(pos.X-32, pos.Y-16));
                music=true;
            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                scene.AddTile(west);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));
                tilehas(new Vector2f(pos.X-32,pos.Y+16));
                music=true;
            }
            scene.SetTilesPlaced(placedtiles.Count());
            if (music)
            {
                sound.Play();
            }
            music=false;
        }
        public void tilehas(Vector2f spawnpos)
        {

            int integer = rng.Next(4);
            if (integer == 0)
            {
                int integer2 = rng.Next(4);
                switch (integer2)
                {
                    default:
                        //spawn enemy using the position of the tile
                        Enemy enemy = new Enemy(spawnpos,placedtiles);//pos is currently stood on tile
                        GameScene scene = (GameScene)Game.CurrentScene;
                        scene.IncreaseEnemyNum();
                        scene.AddGameObject(enemy);
                        break;
                    case 0:
                        Loot loot = new Loot(spawnpos);
                        Game.CurrentScene.AddGameObject(loot);
                        break;
                    case 1:
                        Potion potion = new Potion(spawnpos);
                        Game.CurrentScene.AddGameObject(potion);
                        break;
                }

            }
        }
        public override void Update(Time elapsed)
        {
        }
    }
}
