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
    class CursorClick : AnimatedSprite
    {
        public CursorClick(Vector2f pos) : base(pos)
        {
            Texture = Game.GetTexture("../../../Resources/spritesheet2px.png");
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
                new IntRect(   0, 0, 20, 20), // frame 1
                new IntRect(  20, 0, 20, 20), // frame 2
                new IntRect( 40, 0, 20, 20), // frame 3
                new IntRect( 60, 0, 20, 20), // frame 4
                new IntRect( 80, 0, 20, 20), // frame 5
                new IntRect( 100, 0, 20, 20), // frame 6
                new IntRect( 120, 0, 20, 20), // frame 7
                new IntRect( 140, 0, 20, 20), // frame 8
            };
            AddAnimation("click", frames);
        }
    }
}
