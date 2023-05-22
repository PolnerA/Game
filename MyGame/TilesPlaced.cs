using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class TilesPlaced : GameObject
    {
        private readonly Text _text = new Text();
        //an instance of text for the info related to how the placed tiles will be displayed
        public TilesPlaced(Vector2f pos) //UI
        {
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            //the font is set to times new roman
            _text.Position = pos;
            //the position of the text is set to the paramenter
            _text.CharacterSize = 40;
            _text.FillColor = Color.Yellow;
            //character size is set to 40 and the color is yellow
            AssignTag("tiles_placed");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            //when asked to draw it will display the text
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString = "Tiles Placed: " + scene.GetTilesplaced();
            //displays the tiles placed from the scene
            if (2247<=scene.GetTilesplaced())
            {//if the max amount of tiles is reached
                scene.GameWon();
                //win condition is met, and game is set to a victory scene
            }
        }
    }
}
