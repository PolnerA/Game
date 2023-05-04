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
        {//S GameObject
            _sprite.Position = new Vector2f(pos.X+10,pos.Y+25);
            this.direction = direction;
            switch (this.direction)
            {//direction  -1: nowehere 0:North 1:east 2:south 3:west 4:northwest 5: southwest 6:southeast 7: northeast
                case 0:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell north.png");
                    break;
                case 1:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell east.png");
                    break;
                case 2:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell south.png");
                    break;
                case 3:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell west.png");
                    break;
                case 4:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell northwest.png");
                    break;
                case 5:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell southwest.png");
                    break;
                case 6:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell southeast.png");
                    break;
                case 7:
                    _sprite.Texture = Game.GetTexture("../../../Resources/spell northeast.png");
                    break;
            }
            GameScene scene = (GameScene)Game.CurrentScene;
            scene.DecreaseLives();
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
            SetPosition(_sprite.Position);
        }
    }
}
