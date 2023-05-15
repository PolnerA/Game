using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace MyGame
{
    class GameOverMessage : GameObject
    {
        private readonly Text _text = new Text();//text constructor initializes a new instance of text within sfml.graphics
        private bool spellbook;//bolean for if the spellbook was on or not upon death
        private Sound gamelost = new Sound(); //sound that plays upon the game being lost
        public GameOverMessage(int score,int tilesplaced, bool spellbook)
        {
            //Background
            //sets characteristics for the text
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(50.0f, 50.0f);
            _text.CharacterSize = 50;
            _text.FillColor = Color.Red;
            _text.DisplayedString = "Game Over\n\nYour score: " + score +
           "\n"+tilesplaced+" tiles placed\nPress enter to continue";
            //sound file gets added to the sound then plays the sound
            gamelost.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortGame over.wav");
            gamelost.Play();
            //value of spellbook gets assigned to from Game over scene which gets the value from the game
            this.spellbook=spellbook;
        }
        public override void Draw()//when asked to draw it draws the _text instance of text
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))//if enter is pressed it starts a new game, remembers your spellbook status from when you died
            {
                GameScene scene = new GameScene(spellbook);
                Game.SetScene(scene);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))//if escape is pressed then it exits the render window
            {
                Game.RenderWindow.Close();
            }
        }
    }
}
