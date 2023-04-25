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
        public TilesPlaced(Vector2f pos) 
        {
            _text.Font = Game.GetFont("Resources/Courneuf-Regular.ttf");
            _text.Position = pos;
            _text.CharacterSize = 20;
            _text.FillColor = Color.Yellow;
            AssignTag("tiles_placed");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString = "Tiles Placed: " + scene.GetTilesplaced();
        }
    }
}
