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
    class CursorClick : AnimatedSprite//ToDo make a cursor click animation that works
    {
        public CursorClick(Vector2f pos) : base(pos)
        {//TODO make click animation
            Texture = Game.GetTexture("../../../Resources/click-spritesheet.png");
            SetUpClickAnimation();
            PlayAnimation("click", AnimationMode.OnceForwards);
        }
        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
            if (!IsPlaying())
            {
                MakeDead();
            }
        }
        private void SetUpClickAnimation()
        {
            var frames = new List<IntRect>
            {
                new IntRect(   5, 5, 40, 40), // frame 1
                new IntRect(  55, 5, 40, 40), // frame 2
                new IntRect( 105, 5, 40, 40), // frame 3
                new IntRect( 155, 5, 40, 40), // frame 4
                new IntRect( 205, 5, 40, 40), // frame 5
                new IntRect( 255, 5, 40, 40), // frame 6
                new IntRect( 305, 5, 40, 40), // frame 7
                new IntRect( 355, 5, 40, 40), // frame 8
                new IntRect( 405, 5, 40, 40), // frame 9

            };
            AddAnimation("click", frames);
        }
    }
}
