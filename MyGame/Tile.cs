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
        private readonly Sprite tile = new Sprite();//tile sprite
        private const int revertdelay= 500;//delay until the tile is removed (if it has irregular textures)
        private int reverttimer=revertdelay;//500 ms until tile is reverted
        public Tile(Vector2f pos)
        {
            tile.Texture = Game.GetTexture("../../../Resources/64X32tile.png");//if tile is spawned with jsut a position the tile is normal
            tile.Position = pos;
        }
        public Tile(Vector2f pos, string type)//if the tile is spawned with the parameters of a position and a type specified by a string it's an ireggular tile
        {
            if (type == "red")//if the type is indicated as red it spawns a red tile
            {
                tile.Texture = Game.GetTexture("../../../Resources/64X32Redtile.png");
            }
            else //otherwise it's purple
            {
                tile.Texture = Game.GetTexture("../../../Resources/64X32Purpletile.png");
            }
            //type is either red or purple
            tile.Position= pos;//either way the position is indicated by the vector2f pos 
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(tile);
           //when called to draw it shows the tle
        }
        public override void Update(Time elapsed)
        {
            //uses elapsed to get the milliseconds that passed
            int mselapsed = elapsed.AsMilliseconds();
            if(tile.Texture!= Game.GetTexture("../../../Resources/64X32tile.png")&& reverttimer<=0)
            {//if the tile isn't a normal green tile and 500ms have passed through the revert timer
                MakeDead();
                //kills the tile as it was overlapping either way
            }

            if (0<reverttimer)//if the revert timer is still greater than 0
            {// the amount of milliseconds that elapsed are removed from it
                reverttimer-=mselapsed;
            }
        }
    }
}
