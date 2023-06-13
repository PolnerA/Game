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
            _text.Position = new Vector2f(900,200);
            _text.CharacterSize = 40;
            _text.FillColor = Color.White;
            //sets all the original characteristics of the text
            sprite.Texture = Game.GetTexture("../../../Resources/TutorialRect.png");
            sprite.TextureRect = new IntRect(64, 0, 20, 35);
            //sprites texture is set to a png with all the textures, the texture is set to the first image with an integer rectangle
            _text.DisplayedString = "There will be enemies you will encounter on your journey,\n if they're defeated they drop health potions";
            //shows the text that will be displayed
            sprite.Position = new Vector2f(860, 200);
            //shows the position of the sprite
            tutorialnum=0;
            //tutorial is set to the very beggining
        }
        public override void Draw()
        {//when asked to draw it draws the text and the sprite
            Game.RenderWindow.Draw(_text);
            Game.RenderWindow.Draw(sprite);
        }
        public override void Update(Time elapsed)
        {
            int mselapsed = elapsed.AsMilliseconds();//Counts the time that passed as milliseconds
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Game.RenderWindow.Close();
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==0)//if the mouse button (left) is clicked and enough time has passed, 
            {
                //changes the position of the text and what it shows, 
                _text.Position = new Vector2f(1000, 200);
                _text.DisplayedString = "To get rid of them\nPress space to fire a spell\nevery spell you cast decreases your health\nThis is the advanced spellbook it allows you to \ncast spells in all 8 directions";
                //changes the texture rectangle to show a new texture and moves it somewhere else
                sprite.TextureRect= new IntRect(84, 0, 24, 34);
                sprite.Position = new Vector2f(900, 250);                
                //the timer till the next click is reset
                clicktimer=clickdelay;
                //the current spot in the tutorial is advanced
                tutorialnum=1;
                
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==1)
            {//if the left mouse button is pressed and if the click timer indicated enough time has passed and the current position in the tutorial is one
                //changes the current spot in the tutorial
                tutorialnum=2;
                //changes the position of the text, and the texture of the sprite, along with it's position
                _text.Position = new Vector2f(200, 500);
                sprite.TextureRect = new IntRect(0, 0, 64, 32);
                sprite.Position = new Vector2f(100, 450);
                //changse what the text shows
                _text.DisplayedString ="Click on a nearby tile to teleport there\nMove on every tile to win\nTurn all the pixels green";
                //timer till the next click is reset
                clicktimer=clickdelay;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==2)
            {//if the left mouse button is pressed and if the click timer indicated enough time has passed and the current position in the tutorial is one
                //changes the current spot in the tutorial
                tutorialnum=3;
                //changes the position of the text, and the texture of the sprite, along with it's position
                _text.Position = new Vector2f(200, 500);
                sprite.TextureRect = new IntRect(172, 0,20 ,50);
                sprite.Position = new Vector2f(100, 450);
                //changse what the text shows
                _text.DisplayedString ="This is your character, The only moves he can do are diagonal\nif the sides of the tile are touching, he can move there.";
                //timer till the next click is reset
                clicktimer=clickdelay;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==3)
            {//if the left mouse button is pressed and if the click timer indicated enough time has passed and the current position in the tutorial is one
                //changes the current spot in the tutorial
                tutorialnum=4;
                //changes the position of the text, and the texture of the sprite, along with it's position
                _text.Position = new Vector2f(200, 500);
                sprite.TextureRect = new IntRect(195, 0, 60, 50);
                sprite.Position = new Vector2f(100, 450);
                //changse what the text shows
                _text.DisplayedString ="These are coins, they are sometimes on the tiles\nthey increase your score by 10";
                //timer till the next click is reset
                clicktimer=clickdelay;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer<=0&&tutorialnum==4)
            {//if the left mouse button is pressed and if the click timer indicated enough time has passed and the current position in the tutorial is one
                //changes the current spot in the tutorial
                tutorialnum=5;
                //changes the position of the text, and the texture of the sprite, along with it's position
                _text.Position = new Vector2f(200, 500);
                sprite.TextureRect = new IntRect(0, 36, 12, 20);
                sprite.Position = new Vector2f(100, 450);
                //changse what the text shows
                _text.DisplayedString ="These are potions, they are sometimes on the tiles\nthey increase your health by 1";
                //timer till the next click is reset
                clicktimer=clickdelay;

            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)&&clicktimer <= 0&&tutorialnum==5)
            {//if the mouse button is pressed and the clicktimer is done counting, and the current spot in the tutorial is 2
                //creates a new gamescene for the game
                GameScene scene = new GameScene(false);//spellbook is set to false by default
                Game.SetScene(scene);//scene is changed to the main game
                MakeDead();//kills the text
            }
            if (0<clicktimer&&!Mouse.IsButtonPressed(Mouse.Button.Left))
            {//if the clicktimer is above zero and the left mouse button isn't down
                clicktimer -= mselapsed;
                //clicktimer removes the amount of milliseconds that have gone by in the game
            }
        }
    }
}
