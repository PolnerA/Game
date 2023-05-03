﻿using GameEngine;
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
        private readonly Text _text = new Text();
        public Score(Vector2f pos)
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = pos;
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            AssignTag("score");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString = "Score: " + scene.GetScore();
        }
    }
}

