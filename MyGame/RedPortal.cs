using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class RedPortal:AnimatedSprite
    {
        public RedPortal(Vector2f pos) : base(pos) 
        {
            Texture = Game.GetTexture("../../../Resources/RedPortalSpriteSheet.png");
            SetUpPortalAnimation();
            PlayAnimation("RPortal",AnimationMode.OnceForwards);
        }
        private void SetUpPortalAnimation()
        {
            var frames = new List<IntRect>
            {
                new IntRect(   0, 0, 0,0 ), // frame 1
                new IntRect(  20, 0, 0,0), // frame 2
                new IntRect( 40, 0, 0,0), // frame 3
                new IntRect( 60, 0, 0, 0), // frame 4
                new IntRect( 80, 0, 0, 0), // frame 5
                new IntRect( 100, 0, 0, 0), // frame 6
                new IntRect( 120, 0, 0, 0), // frame 7
                new IntRect( 140, 0,0, 0), // frame 8
                new IntRect( 100, 0, 0, 0), // frame 9
                new IntRect( 80, 0, 0, 0), // frame 10
                new IntRect( 60, 0, 0, 0), // frame 11
                new IntRect( 40, 0, 0,0), // frame 12
                new IntRect(  20, 0, 0,0), // frame 13
                new IntRect(   0, 0, 0,0 ), // frame 14
            };
            AddAnimation("RPortal", frames);
        }
    }
}
