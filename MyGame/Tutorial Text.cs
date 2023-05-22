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
        private int clicktimer = 60;//timer until each click (60ms)
        private int tutorialnum;//counts where it is in the tutoriial
        private const int clickdelay = 60;//the time until each click (60 ms)
        private readonly Text _text = new Text();//text stores information such as where it is & what it looks like
        private readonly Sprite sprite = new Sprite();// same with sprite except from a png instead of a font

        public Tutorial_Text()
        {//UI
            _text.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            _text.Position = new Vector2f(100,500);
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            //sets all the original characteristics of the text
            sprite.Texture = Game.GetTexture("../../../Resources/.png");
            //sprites texture is set to background as then the space as then the spaces the sprites can exist under are simple (change to use texture rect in a bigger png)
            _text.DisplayedString = "There will be enemies you will encounter on your journey";
            //shows the text that will be displayed
            sprite.Position = new Vector2f(1900, 1030);
            //shows the position of the sprite
            tutorialnum=0;
            //tutorial is set to the very beggining
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
            Game.RenderWindow.Draw(sprite);
        }
        public override void Update(Time elapsed)
        {
            int mselapsed = elapsed.AsMilliseconds();
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==0)
            {
                _text.Position = new Vector2f(1000, 200);
                _text.DisplayedString = "To get rid of them\nPress space to fire a spell\nevery spell you cast decreases your health\nThis is the advanced spellbook it allows you to \ncast spells in all 8 directions";
                sprite.Position = new Vector2f(900, 200);
                sprite.Texture = Game.GetTexture("../../../Resources/spell book2.png");
                
                
                clicktimer=clickdelay;
                tutorialnum=1;
                
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==1)
            {
                tutorialnum=2;
                _text.Position = new Vector2f(200, 500);
                sprite.Texture = Game.GetTexture("../../../Resources/64X32tile.png");
                sprite.Position = new Vector2f(100, 450);
                _text.DisplayedString ="Click on a nearby tile to teleport there\nMove on every tile to win\nTurn all the pixels green";
                clicktimer=clickdelay;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer <= 0&&tutorialnum==2)
            {
                GameScene scene = new GameScene(false);
                Game.SetScene(scene);
                MakeDead();
            }
            if (0<clicktimer&&!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                clicktimer -= mselapsed;
            }
        }
    }
}
