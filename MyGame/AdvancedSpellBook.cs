using System;
using GameEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace MyGame
{
    class AdvancedSpellBook: GameObject
    {
        Sprite _sprite = new Sprite();// Creates the sprite for the spell book
        const int toggledelay = 50;//delay for toggling it every 50 ms
        private int timer = 50;
        
        public AdvancedSpellBook(bool spellbook)
        {
            //part of UI

            if (!spellbook)//gets whether the spellbook is turned on or not
            {
                _sprite.Texture = Game.GetTexture("../../../Resources/spell book2.png");
            }
            else 
            {
                _sprite.Texture = Game.GetTexture("../../../Resources/spell book2 on.png");
            }
            _sprite.Position = new Vector2f(0,0);
            
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);//draws the sprite when called upon to get drawn in the scene
        }
        public override void Update(Time elapsed)
        {
            //if the mouse position is in the sprite texture rectangle
            if (Mouse.GetPosition().X<_sprite.Position.X+_sprite.Texture.Size.X&&_sprite.Position.X<Mouse.GetPosition().X&& Mouse.GetPosition().Y<_sprite.Position.Y+_sprite.Texture.Size.Y&&_sprite.Position.Y<Mouse.GetPosition().Y)
            {
                //if the mouse left button is clicked and the timer has reached at least 50
                if (Mouse.IsButtonPressed(Mouse.Button.Left)&&toggledelay<=timer)
                {
                    GameScene scene = (GameScene)Game.CurrentScene;//toggles the spellbook value, in the gamescene and toggles the texture
                    scene.ToggleSpellBook();
                    if (_sprite.Texture == Game.GetTexture("../../../Resources/spell book2.png"))
                    {
                        _sprite.Texture = Game.GetTexture("../../../Resources/spell book2 on.png");
                    }
                    else
                    {
                        _sprite.Texture = Game.GetTexture("../../../Resources/spell book2.png");
                    }
                    timer = 0;
                }
                SpellBooktext text = new SpellBooktext(new Vector2f(_sprite.Position.X+_sprite.Texture.Size.X, _sprite.Position.Y));//if the mouse is in position it shows the informational text about the spellbook
                Game.CurrentScene.AddUserInterface(text);
            }
            timer++;
        }
    }
}
