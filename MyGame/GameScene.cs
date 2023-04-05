using GameEngine;
using SFML.System;
namespace MyGame
{
    class GameScene : Scene
    {
        private int _lives = 3;
        private int _score;
        public GameScene()
        {
            
             
            Background background = new Background();
             AddGameObject(background);
             Tile tile = new Tile(new Vector2f(100,520));//original tile            _ 
             Tile tile1 = new Tile(new Vector2f(132, 536));//south tile +32x, + 16y  \
             Tile tile2 = new Tile(new Vector2f (132,504));//east tile +32x, -16y     |_ compared to original tile
             Tile tile3 = new Tile(new Vector2f(68, 504));//north tile -32x, -16y     |
             Tile tile4 = new Tile(new Vector2f(68, 536));//west tile -32x, +16y    _/
             AddGameObject(tile);
             AddGameObject(tile1);
             AddGameObject(tile2);
             AddGameObject(tile3);
             AddGameObject(tile4);
             Tile_Spawner tiles = new Tile_Spawner();
             AddGameObject(tiles);
             Hero hero = new Hero(new Vector2f(122, 488));//character location +22x, -32y compared to the tile stood on
             AddGameObject(hero);

             
            
            
        }
        // Get the current score
        public int GetScore()
        {
            return _score;
        }
        // Increase the score
        public void IncreaseScore()
        {
            ++_score;
        }
        // Get the number of lives
        public int GetLives()
        {
            return _lives;
        }
        // Decrease the number of lives
        public void DecreaseLives()
        {
            --_lives;
            if (_lives == 0)
            {
                GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);
            }
        }
    }
}
