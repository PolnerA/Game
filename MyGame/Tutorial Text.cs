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
        private int clicktimer = 10;//find a way to do tutorial w/o player movement
        private int tutorialnum;
        private const int clickdelay = 10;
        private readonly Text _text = new Text();
        private readonly Sprite sprite = new Sprite();
        private readonly Sprite sprite2 = new Sprite();
        public Tutorial_Text()
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            
            _text.Position = new Vector2f(200,500);
            _text.CharacterSize = 40;
            sprite2.Texture= Game.GetTexture("../../../Resources/64X32tile.png");
            sprite2.Position= new Vector2f(100, 500);
            _text.FillColor = Color.White;
            _text.DisplayedString ="Click on a nearby tile to move";
            tutorialnum=0;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            Game.RenderWindow.Draw(sprite);
            Game.RenderWindow.Draw(sprite2);
        }
        public override void Update(Time elapsed)
        {
            if (0<tutorialnum)
            {
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clickdelay<=clicktimer&&tutorialnum==0)
            {
                _text.Position = new Vector2f(1000, 200);
                _text.DisplayedString = "Press space to fire a spell\nevery spell you cast decreases your health";
                clicktimer=0;
                tutorialnum=1;
                
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clickdelay<=clicktimer&&tutorialnum==1)
            {
                tutorialnum=2;
                sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");
                _text.DisplayedString = "These are enemies you will encounter on your journey";
                sprite.Position = new Vector2f(1000, 300);
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
