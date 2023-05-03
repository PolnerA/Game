using System;
using System.Collections.Generic;
using System.ComponentModel;
using SFML.Graphics;
using SFML.System;

namespace GameEngine
{
    // The Scene manages all the GameObjects currently in the game.
    class Scene
    {
        // This holds our game objects.
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        private readonly List<GameObject> _tiles = new List<GameObject>();

        private readonly List<GameObject> _background = new List<GameObject>();

        private readonly List<GameObject> _cloud = new List<GameObject>();

        private readonly List<GameObject> _userinterface = new List<GameObject>();

        // Puts a GameObject into the scene.
        public void AddGameObject(GameObject gameObject)
        {
            // This adds the game object onto the back (the end) of the list of game objects.
            _gameObjects.Add(gameObject);
        }
        public void AddGameObject(int position, GameObject gameObject)
        {
            //This adds the game object at a location into the list of game objects.
            _gameObjects.Insert(position, gameObject);
        }

        public void AddTile(GameObject tile)
        {
            _tiles.Add(tile);
        }

        public void AddBackground(GameObject background)
        {
            _background.Add(background);
        }

        public void AddUserInterface(GameObject UI)
        {
            _userinterface.Add(UI);
        }

        public void AddCloud(GameObject cloud)
        {
            _cloud.Add(cloud);
        }
        public int GameObjectAmount()
        {
            return _gameObjects.Count;
        }

        // Called by the Game instance once per frame.
        public void Update(Time time)
        {
            // Clear the window.
            Game.RenderWindow.Clear();

            // Go through our normal sequence of game loop stuff.

            // Handle any keyboard, mouse events, etc. for our game window.
            Game.RenderWindow.DispatchEvents();

            HandleCollisions();
            UpdateGameObjects(time);
            RemoveDeadGameObjects();
            DrawBackground();
            DrawTiles();
            DrawGameObjects();//draw background, then tiles, objects (top-bottom rendering), then the clouds ending with the ui.
            DrawClouds();
            DrawUserInterface();
            // Draw the window as updated by the game objects.
            Game.RenderWindow.Display();
        }

        // This method lets game objects respond to collisions.
        private void HandleCollisions()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var gameObject = _gameObjects[i];

                // Only check objects that ask to be checked.
                if (!gameObject.IsCollisionCheckEnabled()) continue;

                FloatRect collisionRect = gameObject.GetCollisionRect();

                // Don't bother checking if this game object has a collision rectangle with no area.
                if (collisionRect.Height == 0 || collisionRect.Width == 0) continue;

                // See if this game object is colliding with any other game object.
                for (int j = 0; j < _gameObjects.Count; j++)
                {
                    var otherGameObject = _gameObjects[j];

                    // Don't check an object colliding with itself.
                    if (gameObject == otherGameObject) continue;

                    if (gameObject.IsDead()) return;

                    // When we find a collision, invoke the collision handler for both objects.
                    if (collisionRect.Intersects(otherGameObject.GetCollisionRect()))
                    {
                        gameObject.HandleCollision(otherGameObject);
                        otherGameObject.HandleCollision(gameObject);
                    }
                }
            }
        }

        // This function calls update on each of our game objects.
        private void UpdateGameObjects(Time time)
        {
            for (int i = 0; i < _gameObjects.Count; i++) { _gameObjects[i].Update(time); }
            for (int i = 0; i < _cloud.Count; i++) {_cloud[i].Update(time); }
            for (int i = 0; i<_userinterface.Count; i++) {_userinterface[i].Update(time); }
            for (int i = 0; i < _background.Count; i++) _background[i].Update(time);
        }

        // This function calls draw on each of our game objects.
        private void DrawGameObjects()
        {
            for (int y = 0; y<Game.RenderWindow.Size.Y; y++)
            {
                foreach (var GameObject in _gameObjects)
                {
                    if (GameObject.GetPosition().Y==y)
                    {
                        GameObject.Draw();
                    }
                }
            }
        }
        private void DrawBackground()
        { 
            foreach (var gameobject in _background) gameobject.Draw();
        }
        private void DrawUserInterface()
        {
            foreach (var gameobject in _userinterface) gameobject.Draw();
        }
        private void DrawClouds()
        {
            foreach (var gameobject in _cloud) gameobject.Draw();
        }
        private void DrawTiles()
        {
            foreach (var gameobject in _tiles) gameobject.Draw();
        }

        // This function removes objects that indicate they are dead from the scene.
        private void RemoveDeadGameObjects()
        {
            // This is a "lambda", which is a fancy name for an anonymous function.
            // It's "anonymous" because it doesn't have a name. We've declared a variable
            // named "isDead", and that variable can be used to call the function, but the
            // function itself is nameless.
            Predicate<GameObject> isDead = gameObject => gameObject.IsDead();

            // Here we use the lambda declared above by passing it to the standard RemoveAll
            // method on List<T>, which calls our lambda once for each element in
            // gameObjects. If our lambda returns true, that game object ends up being
            // removed from our list.
            _gameObjects.RemoveAll(isDead);
            _cloud.RemoveAll(isDead);
            _userinterface.RemoveAll(isDead);
        }
    }
}