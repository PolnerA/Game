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
            Texture = Game.GetTexture("Resources/explosion-spritesheet.png");
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
                new IntRect( 0, 0, 64, 64), // frame 1
                new IntRect( 64, 0, 64, 64), // frame 2
                new IntRect(128, 0, 64, 64), // frame 3
                new IntRect(192, 0, 64, 64), // frame 4
                new IntRect(256, 0, 64, 64), // frame 5
                new IntRect(320, 0, 64, 64), // frame 6
                new IntRect(384, 0, 64, 64), // frame 7
                new IntRect(448, 0, 64, 64), // frame 8
                new IntRect(512, 0, 64, 64) // frame 9            
            
            };
            AddAnimation("click", frames);
        }
    }
}
