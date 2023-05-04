using GameEngine;
using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace MyGame
{
    class Tutorial_Text : GameObject
    {
        private readonly Text _text = new Text();
        private readonly Text _text2 = new Text();
        public Tutorial_Text()
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text2.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(200,500);
            _text2.Position = new Vector2f(1000, 200);
            _text.CharacterSize = 40;
            _text2.CharacterSize = 40;
            _text.FillColor = Color.White;
            _text2.FillColor = Color.White;
            _text.DisplayedString ="Click on a nearby tile to move";
            _text2.DisplayedString = "Press space to fire a spell\nevery spell you cast decreases your health";
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            Game.RenderWindow.Draw(_text2);
        }
        public override void Update(Time elapsed)
        {
             if (Mouse.IsButtonPressed(Mouse.Button.Left))
             {
                MakeDead();  
             }
        }
    }
}
