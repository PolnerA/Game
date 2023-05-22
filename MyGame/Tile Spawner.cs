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
using static System.Formats.Asn1.AsnWriter;

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
        public void SpawnThreetiles(bool north, bool south, bool west, bool east,Vector2f pos)
        {
            GameScene scene = (GameScene)Game.CurrentScene;//sets an instance of gamescene to add tiles
            if (north)//if the direction is indicated as true, the tile is created and added to placed tiles, tilehas is also called to see what spawns with it.
            {
                Tile _north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                scene.AddTile(_north);
                tilehas(new Vector2f(pos.X-32, pos.Y-16));
                music= true;
            }
            if (south)
            {
                Tile _south = new Tile(new Vector2f(pos.X+32, pos.Y+16));
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y+16));
                scene.AddTile(_south);
                tilehas(new Vector2f(pos.X + 32, pos.Y + 16));
                music=true;
            }
            if (west)
            {
                Tile _west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));

                scene.AddTile(_west);
                tilehas(new Vector2f(pos.X - 32, pos.Y + 16));
                music=true;
            }
            if (east)
            {
                Tile _east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                scene.AddTile(_east);
                tilehas(new Vector2f(pos.X + 32, pos.Y - 16));
                music=true;
            }//music is set to true to play a sound effect every single time you discover a new tile (bool is used to not overlap audio 1 new tile is the same as 2 3 etc..)
        }
        public void SpawnTiles(Vector2f pos)
        {
            bool north=true, south=true, east=true, west = true;//all the tiles are set to spawn
            GameScene scene = (GameScene)Game.CurrentScene;//sets an instance of game scene to set the tiles placed displayed to sync with the list
            for (int i=0;i<placedtiles.Count();i++)
            {
                Vector2f tilepos = placedtiles[i];
                //goes through the list and if the tileposition in the list is similar to one that will be spawned it's set to false
                //as there is already a tile there
                if (tilepos == new Vector2f(pos.X+32, pos.Y+16))
                {
                    south=false;
                }
                if (tilepos == new Vector2f(pos.X-32, pos.Y+16))
                {
                    west=false;
                }
                if (tilepos == new Vector2f(pos.X+32, pos.Y-16))
                {
                    east=false;
                }
                if (tilepos == new Vector2f(pos.X-32, pos.Y-16))
                {
                    north=false;
                }
            }
            SpawnThreetiles(north,south ,west ,east, pos);//spawns the four tiles if they haven't been set to false
            scene.SetTilesPlaced(placedtiles.Count());//sets the tiles placed to the correct value
            if (music)//if at least one tile is spawned music will play
            {
                sound.Play();
            }
            music=false;//music is reset
        }
        public void tilehas(Vector2f spawnpos)
        {

            int integer = rng.Next(4);
            // one in four chance for the tile to have something
            if (integer == 0)
            {
                int integer2 = rng.Next(4);
                // one in four for the thing to be a potion or loot/coins
                // the remaining half of a chance is for an enemy to be on that tile
                switch (integer2)
                {
                    default:
                        //spawn enemy using the position of the tile
                        Enemy enemy = new Enemy(spawnpos,placedtiles);//pos is currently stood on tile
                        GameScene scene = (GameScene)Game.CurrentScene;//1 in 8 chance
                        //gets the current gamescene to increase the number of enemies in it and to add the enemy
                        scene.IncreaseEnemyNum();
                        scene.AddGameObject(enemy);
                        break;
                    case 0:
                        Loot loot = new Loot(spawnpos);// 1 in 16 chance
                        //adds loot to the gamescene using the position of the tile
                        Game.CurrentScene.AddGameObject(loot);
                        break;
                    case 1:
                        Potion potion = new Potion(spawnpos);// 1 in 16 chance
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
