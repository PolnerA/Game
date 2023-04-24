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
            Texture = Game.GetTexture("../../../Resources/click.png");
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
                new IntRect(   0, 0, 10, 10), // frame 1
                new IntRect(  15, 0, 10, 10), // frame 2
                new IntRect(  30, 0, 10, 10), // frame 3
                new IntRect(  45, 0, 10, 10), // frame 4
                new IntRect(  60, 0, 10, 10), // frame 5         
            };
            AddAnimation("click", frames);
        }
    }
}
