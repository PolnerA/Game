using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Spell : GameObject
    {
        private const float Speed = 0.3f;
        private readonly Sprite _sprite = new Sprite();
        private int direction;
        private int still;
        public Spell(Vector2f pos,int direction)//0 north 1 east 2 south 3 west
        {
            _sprite.Texture = Game.GetTexture("../../../Resources/laser.png");
            _sprite.Position = new Vector2f(pos.X+10,pos.Y+25);
            this.direction = direction;
            AssignTag("spell");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("spell"))
            {
                otherGameObject.MakeDead();
            }
            MakeDead();
        }
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            if (Game.RenderWindow.Size.X < pos.X||pos.X<0||pos.Y<0||Game.RenderWindow.Size.Y<pos.Y||still==1)
            {
                MakeDead();
            }
            else
            {
                if (direction == 0)//north
                {
                    _sprite.Position = new Vector2f(pos.X - Speed * msElapsed, pos.Y-(0.5f*Speed*msElapsed)); //left 2 up 1 

                }
                else if (direction ==1)//east
                {
                    _sprite.Position = new Vector2f(pos.X + Speed * msElapsed, pos.Y-(0.5f*Speed*msElapsed)); //right 2 up 1 


                }
                else if (direction ==2) //south
                {
                    _sprite.Position = new Vector2f(pos.X + Speed * msElapsed, pos.Y+(0.5f*Speed*msElapsed)); //right 2 down 1 

                }
                else if (direction ==3) //west
                {
                    _sprite.Position = new Vector2f(pos.X - Speed * msElapsed, pos.Y+(0.5f*Speed*msElapsed)); //left 2 down 1 

                }
                else if (direction ==4)//northwest
                {
                    _sprite.Position = new Vector2f(pos.X-Speed*msElapsed,pos.Y);//left
                }
                else if (direction ==5)//southwest
                {
                    _sprite.Position = new Vector2f(pos.X,pos.Y+Speed*msElapsed);//down
                }
                else if (direction ==6)//southeast
                {
                    _sprite.Position = new Vector2f(pos.X+Speed*msElapsed,pos.Y);//right
                }
                else if (direction ==7)//northeast
                {
                    _sprite.Position = new Vector2f(pos.X, pos.Y-Speed*msElapsed);//up
                }
                if (_sprite.Position == pos)
                {
                    still++;
                }
            }
        }
    }
}
