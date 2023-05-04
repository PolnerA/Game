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
        Sprite _sprite = new Sprite();
        const int toggledelay = 50;
        private int timer = 50;
        
        public AdvancedSpellBook()
        {
            //part of UI
            _sprite.Texture = Game.GetTexture("../../../Resources/spell book2.png");//make spell book and spell book on the same area
            _sprite.Position = new Vector2f(0,0);//make spells red so you can see them on the background
            
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            if (Mouse.GetPosition().X<_sprite.Position.X+_sprite.Texture.Size.X&&_sprite.Position.X<Mouse.GetPosition().X&& Mouse.GetPosition().Y<_sprite.Position.Y+_sprite.Texture.Size.Y&&_sprite.Position.Y<Mouse.GetPosition().Y)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left)&&toggledelay<=timer)
                {
                    GameScene scene = (GameScene)Game.CurrentScene;
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
                SpellBooktext text = new SpellBooktext(new Vector2f(_sprite.Position.X+_sprite.Texture.Size.X, _sprite.Position.Y));
                Game.CurrentScene.AddUserInterface(text);
            }
            timer++;
        }
    }
}
