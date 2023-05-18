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
    class Score : GameObject
    {
        //text for the score
        private readonly Text _text = new Text();
        public Score(Vector2f pos)
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = pos;
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            //gives it a font position character size and a color
            AssignTag("score");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            //when asked to draw it draws the text
        }
        public override void Update(Time elapsed)
        {
            //accesses the scene, and shows the score that the scene stores
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString = "Score: " + scene.GetScore();
        }
    }
}

