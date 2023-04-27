using GameEngine;
using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyGame
{
    class Tutorial_Text : GameObject
    {
        private readonly Text _text = new Text();
        public Tutorial_Text()
        {
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(0,0);
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            _text.DisplayedString ="";
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
             
        }
    }
}
