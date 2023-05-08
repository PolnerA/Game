using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace MyGame
{
    class GameOverMessage : GameObject
    {
        private readonly Text _text = new Text();
        private bool spellbook;
        public GameOverMessage(int score,int tilesplaced, bool spellbook)
        {
            //Background
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(50.0f, 50.0f);
            _text.CharacterSize = 50;
            _text.FillColor = Color.Red;
            _text.DisplayedString = "Game Over\n\nYour kills: " + score +
            "\n"+tilesplaced+" tiles placed\nPress enter to continue";
            this.spellbook=spellbook;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                GameScene scene = new GameScene(spellbook);
                Game.SetScene(scene);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Game.RenderWindow.Close();
            }
        }
    }
}
