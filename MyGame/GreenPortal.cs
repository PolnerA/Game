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
    class GreenPortal: AnimatedSprite
    {
        public GreenPortal(Vector2f pos) : base(pos,10)
        {
            Texture = Game.GetTexture("../../../Resources/GreenPortalSpriteSheet.png");
            SetOriginMode(OriginMode.TopLeft);
            SetUpPortalAnimation();
            PlayAnimation("GPortal", AnimationMode.OnceForwards);
        }
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);//goes to the next frame per default frames per second, if it is done it removes it from the list of things to render
            if (!IsPlaying())
            {
                MakeDead();
            }
        }
        private void SetUpPortalAnimation()
        {
            var frames = new List<IntRect>
            {
                new IntRect(   0, 0, 20, 50 ), // frame 1
                new IntRect(  20, 0, 20, 50), // frame 2
                new IntRect(  40, 0, 20, 50), // frame 3
                new IntRect(  60, 0, 20, 50), // frame 4
                new IntRect(  80, 0, 20, 50), // frame 5
                new IntRect( 100, 0, 20, 50), // frame 6
                new IntRect( 120, 0, 20, 50), // frame 7
                new IntRect( 140, 0, 20, 50), // frame 8
                new IntRect( 120, 0, 20, 50), // frame 9
                new IntRect( 140, 0, 20, 50), // frame 10
                new IntRect( 120, 0, 20, 50), // frame 11
                new IntRect( 140, 0, 20, 50), // frame 12
                new IntRect( 120, 0, 20, 50), // frame 13
                new IntRect( 140, 0, 20, 50), // frame 14
                new IntRect( 120, 0, 20, 50), // frame 15
                new IntRect( 140, 0, 20, 50), // frame 16
                new IntRect( 100, 0, 20, 50), // frame 17
                new IntRect(  80, 0, 20, 50), // frame 18
                new IntRect(  60, 0, 20, 50), // frame 19
                new IntRect(  40, 0, 20, 50), // frame 20
                new IntRect(  20, 0, 20, 50), // frame 21
                new IntRect(   0, 0, 20, 50), // frame 22
            };
            AddAnimation("GPortal", frames);
        }
    }
}
