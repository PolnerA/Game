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
        private int clicktimer = 60;//find a way to do tutorial w/o player movement
        private int tutorialnum;
        private const int clickdelay = 60;
        private readonly Text _text = new Text();
        private readonly Sprite sprite = new Sprite();

        public Tutorial_Text()
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            
            _text.Position = new Vector2f(100,500);
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            sprite.Texture = Game.GetTexture("../../../Resources/Background.png");
            sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");
            _text.DisplayedString = "There will be enemies you will encounter on your journey";
            sprite.Position = new Vector2f(1900, 1030);
            
            tutorialnum=0;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            Game.RenderWindow.Draw(sprite);
        }
        public override void Update(Time elapsed)
        {
    
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clickdelay<=clicktimer&&tutorialnum==0)
            {
                _text.Position = new Vector2f(1000, 200);
                _text.DisplayedString = "To get rid of them\nPress space to fire a spell\nevery spell you cast decreases your health\nThis is the advanced spellbook it allows you to \ncast spells in all 8 directions";
                sprite.Position = new Vector2f(900, 200);
                sprite.Texture = Game.GetTexture("../../../Resources/spell book2.png");
                
                
                clicktimer=0;
                tutorialnum=1;
                
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clickdelay<=clicktimer&&tutorialnum==1)
            {
                tutorialnum=2;
                _text.Position = new Vector2f(200, 500);
                sprite.Texture = Game.GetTexture("../../../Resources/64X32tile.png");
                sprite.Position = new Vector2f(100, 450);
                _text.DisplayedString ="Click on a nearby tile to teleport there\nMove on every tile to win\nTurn all the pixels green";
                clicktimer=0;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clickdelay<=clicktimer&&tutorialnum==2)
            {
                GameScene scene = new GameScene();
                Game.SetScene(scene);
                MakeDead();
            }
            clicktimer++;
        }
    }
}
