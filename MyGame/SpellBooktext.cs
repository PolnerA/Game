using GameEngine;
using Microsoft.Win32.SafeHandles;
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
    class SpellBooktext: GameObject
    {
        Text info = new Text();//informational text for the spellbook
        public SpellBooktext(Vector2f pos) 
        {//UI
            info.Font = Game.GetFont("../../../Resources/times new roman.ttf");
            info.CharacterSize = 20;//sets up the characteristics
            info.FillColor = Color.White;
            info.DisplayedString = "This is the advanced spellbook use qwe to fire spells in that direction \nClick to activate                                 a  d\n                                                              zxc";
            info.Position = pos;
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(info);//it displays the info
        }
        public override void Update(Time elapsed)
        {
            if (64<Mouse.GetPosition().X || Mouse.GetPosition().X<0 || 64< Mouse.GetPosition().Y || Mouse.GetPosition().Y<0)
            {//if the mouse cursor isn't in the advanced spellbook's rectangle it doesn't show the text
                MakeDead();
            }
        }
    }
}
