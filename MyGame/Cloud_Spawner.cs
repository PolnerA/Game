using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Cloud_Spawner : GameObject
    {
        private const int SpawnDelay = 1000;
        private int _timer;
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            _timer -= msElapsed;
            if (_timer <= 0)
            {
                _timer = SpawnDelay;
                Vector2u size = Game.RenderWindow.Size;
                float cloudX = size.X + 201;
                float cloudY = Game.Random.Next() % size.Y;
                Cloud cloud= new Cloud(new Vector2f(cloudX, cloudY));
                Game.CurrentScene.AddGameObject(cloud);
            }
        }
    }
}
