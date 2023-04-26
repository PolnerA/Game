using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Tile_Spawner :GameObject
    {
        private List<Vector2f> isogrid= new List<Vector2f>();
        private List<Vector2f> placedtiles = new List<Vector2f>();//creates a new instance of tilespawner each move so, placed tiles needs to be in gamescene or pass the hero classes tilespawner's to use the one in the gamescene
        Random rng = new Random();
        private const int SpawnDelay = 1000;
        private int _timer;
        private Vector2f position;
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
                Game.CurrentScene.AddGameObject(1, south);
                tilehas(new Vector2f(pos.X + 32, pos.Y + 16));
            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                Game.CurrentScene.AddGameObject(1, east);
                tilehas(new Vector2f(pos.X + 32, pos.Y - 16));
            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));

                Game.CurrentScene.AddGameObject(1, west);
                tilehas(new Vector2f(pos.X - 32, pos.Y + 16));
            }
        }
        public void SpawnThreetilesWest(Vector2f pos)
        {
            position = pos;
            bool down = true;
            bool up = true;
            bool right = true;
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
                Game.CurrentScene.AddGameObject(1, south);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y+16));
                tilehas(new Vector2f(pos.X + 32, pos.Y + 16));

            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                Game.CurrentScene.AddGameObject(1, east);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                tilehas(new Vector2f(pos.X+32, pos.Y-16));

            }
            if (up)
            {
                Tile north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                Game.CurrentScene.AddGameObject(1, north);
                tilehas(new Vector2f(pos.X-32, pos.Y-16));
            }

        }
        public void SpawnThreetilesSouth(Vector2f pos)
        {
            position = pos;
            bool left = true;
            bool up = true;
            bool right = true;
           
            for (int i = 0; i<placedtiles.Count; i++)
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
                Game.CurrentScene.AddGameObject(1, north);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                tilehas(new Vector2f(pos.X-32, pos.Y-16));

            }
            if (right)
            {
                Tile east = new Tile(new Vector2f(pos.X+32, pos.Y-16));
                Game.CurrentScene.AddGameObject(1, east);
                placedtiles.Add(new Vector2f(pos.X+32, pos.Y-16));
                tilehas(new Vector2f(pos.X+32, pos.Y-16));

            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                Game.CurrentScene.AddGameObject(1, west);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));
                tilehas(new Vector2f(pos.X-32, pos.Y+16));

            }

        }
        public void SpawnThreetilesEast(Vector2f pos)
        {
            position = pos;
            bool left = true;
            bool up = true;
            bool down = true;
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
                Game.CurrentScene.AddGameObject(1, south);
                placedtiles.Add(new Vector2f(pos.X+32,pos.Y+16));
                tilehas(new Vector2f(pos.X+32, pos.Y+16));

            }
            if (up)
            {
                Tile north = new Tile(new Vector2f(pos.X-32, pos.Y-16));
                Game.CurrentScene.AddGameObject(1, north);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y-16));
                tilehas(new Vector2f(pos.X-32, pos.Y-16));

            }
            if (left)
            {
                Tile west = new Tile(new Vector2f(pos.X-32, pos.Y+16));
                Game.CurrentScene.AddGameObject(1, west);
                placedtiles.Add(new Vector2f(pos.X-32, pos.Y+16));
                tilehas(new Vector2f(pos.X-32,pos.Y+16));

            }

        }
        public void tilehas(Vector2f spawnpos)
        {

            int integer = rng.Next(4);
            if (integer == 0)
            {
                int integer2 = rng.Next(3);
                switch (integer2)
                {
                    case 0:
                        //spawn enemy using the position of the tile
                        Enemy enemy = new Enemy(spawnpos,placedtiles);//pos is currently stood on tile
                        Game.CurrentScene.AddGameObject(enemy);

                        break;
                    case 1:
                        //spawn chest using the position of the tile
                        break;
                    case 2:
                        //spawn potion using the position of the tile
                        break;
                }

            }
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            scene.SetTilesPlaced(placedtiles.Count());
            Console.WriteLine(placedtiles.Count());//set at 0
        }
    }
}
