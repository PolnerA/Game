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
        private List<Vector2f> placedtiles = new List<Vector2f>() { new Vector2f(100,520), new Vector2f(132, 536), new Vector2f(132, 504), new Vector2f(68, 504), new Vector2f(68, 536) };
        Random rng = new Random();
        private Vector2f position;
        private bool music=false;
        public Tile_Spawner()
        {
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
        public void SpawnThreetilesNorth(Vector2f pos)
        {
            position = pos;
            bool down=true;
            bool left = true;
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
               if (tilepos == new Vector2f(pos.X-32, pos.Y+16))
               {
                   left=false;
               }
                
            }
            if (down)
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
            }
            music=false;
            scene.SetTilesPlaced(placedtiles.Count());
        }
        public void SpawnThreetilesWest(Vector2f pos)
        {
            position = pos;
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
            }
            music=false;

        }
        public void SpawnThreetilesSouth(Vector2f pos)
        {
            position = pos;
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
            }
            music=false;
        }
        public void SpawnThreetilesEast(Vector2f pos)
        {
            position = pos;
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
                        Scene scene = (GameScene)Game.CurrentScene;
                        Game.CurrentScene.AddGameObject(enemy);//gets current amount of game objects and puts it behind by the an amount of tiles placed to put it behind the score
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
            GameScene scene = (GameScene)Game.CurrentScene;
            foreach (Vector2f tile in placedtiles)
            {
                foreach (Vector2f tile2 in scene.GetIsoGrid())
                { 
                    //if all tiles in iso grid are met game. set victory
                }
            }
        }
    }
}
