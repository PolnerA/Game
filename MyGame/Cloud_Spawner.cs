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
    {//TODO spawn clouds isometricly
        private const int SpawnDelay = 1000;//spawn delay for the clouds
        private int _timer;
        public override void Update(Time elapsed)
        {//clouds
            int msElapsed = elapsed.AsMilliseconds();
            _timer -= msElapsed;
            if (_timer <= 0)
            {
                _timer = SpawnDelay;
                Vector2u size = Game.RenderWindow.Size;//gets the size of the renderwindow and adds the cloud's texture's x value, then it finds a random position along the y axis
                float cloudX = size.X + 201;
                float cloudY = Game.Random.Next() % size.Y;
                Cloud cloud= new Cloud(new Vector2f(cloudX, cloudY));//creates a new cloud and adds it to _cloud
                Game.CurrentScene.AddCloud(cloud);
            }
        }
    }
}
