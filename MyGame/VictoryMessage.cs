using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class VictoryMessage : GameObject
    {
        private readonly Text _text = new Text();
        public VictoryMessage(int score, int tilesplaced)
        {
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(50.0f, 50.0f);
            _text.CharacterSize = 50;
            _text.FillColor = Color.Yellow;
            _text.DisplayedString = "Victory!!\n\nYour kills: " + score +
            "\n"+tilesplaced+" tiles placed\nPress enter to continue playing";
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                GameScene scene = new GameScene();
                Game.SetScene(scene);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Game.RenderWindow.Close();
            }
        }
    }
}
