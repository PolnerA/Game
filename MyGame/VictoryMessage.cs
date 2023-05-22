using GameEngine;
using SFML.Audio;
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
        Sound gamewon = new Sound();
        //text stores info about the text font, size, color etc.. while gamewon stores the sound that will be repeated
        private int musictimer;
        //timer counts down untill when the music can be played again
        private const int musicdelay = 5000;
        //music is played every 5 seconds
        public VictoryMessage(int score, int tilesplaced)
        {//Background
            gamewon.SoundBuffer = Game.GetSoundBuffer("../../../Resources/shortVictory.wav");
            //sound buffer is gotten from the files and added to gamewon
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(50.0f, 50.0f);
            _text.CharacterSize = 50;
            _text.FillColor = Color.Yellow;
            _text.DisplayedString = "Victory!!\n\nYour kills: " + score +
            "\n"+tilesplaced+" tiles placed\nPress enter to continue playing";
            //text characteristics are filled out
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            int mselapsed = elapsed.AsMilliseconds();//finds the amount of milliseconds that elapsed between the last update
            if (0<musictimer)
            {//if music timer is above 0
                musictimer -= mselapsed;
                //takes away the time that has elapsed from the music timer
            }
            if (musictimer<=0)
            {
                //if music timer is below 0 (or equal to)
                gamewon.Play();//plays the sound and resets the timer
                musictimer=musicdelay;
            }
            //if enter is pressed the game is ready to be played again
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                GameScene scene = new GameScene(false);
                Game.SetScene(scene);
            }
            //if escape is pressed it closes the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Game.RenderWindow.Close();
            }
        }
    }
}
