using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace MyGame
{
    
    class Tile : GameObject
    {
        private readonly Sprite tile = new Sprite();//tile
        private const int revertdelay= 500;
        private int reverttimer=revertdelay;//1000 ms until tile is reverted
        public Tile(Vector2f pos)
        {
            tile.Texture = Game.GetTexture("../../../Resources/64X32tile.png");
            tile.Position = pos;
        }
        public Tile(Vector2f pos, string type)
        {
            if (type == "red")
            {
                tile.Texture = Game.GetTexture("../../../Resources/64X32Redtile.png");
            }
            else 
            {
                tile.Texture = Game.GetTexture("../../../Resources/64X32Purpletile.png");
            }
            tile.Position= pos;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(tile);
           
        }
        public override void Update(Time elapsed)
        {
            int mselapsed = elapsed.AsMilliseconds();
            if(tile.Texture!= Game.GetTexture("../../../Resources/64X32tile.png")&& reverttimer<=0)
            {
                MakeDead();
            }

            if (0<reverttimer)
            {
                reverttimer-=mselapsed;
            }
        }
    }
}
