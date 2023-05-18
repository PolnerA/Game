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
    class Lives : GameObject
    {
        private readonly Text _text = new Text();//creates new blank text
        public Lives(Vector2f pos)
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = pos;//gives the text a font, position, color and size
            _text.CharacterSize = 40;
            _text.FillColor = Color.Red;
            AssignTag("lives");//assigns a tag
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);//when asked to draw it draws the texts
        }
        public override void Update(Time elapsed)
        {// gets the number of lives from the scene and updates the displayed string based off of that
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString = "Lives: " + scene.GetLives();
        }
    }
}
