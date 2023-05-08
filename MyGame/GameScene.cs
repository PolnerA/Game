using GameEngine;
using SFML.System;
using System.Net.Http.Headers;
using System.Threading;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _lives = 1;
        private int _score;
        private int tilesplaced=5;
        private bool spellbook = false;
        public Hero hero = new Hero(new Vector2f(122, 488));
        public GameScene()
        {
            Background background = new Background();
            AddBackground(background);
            Tile tile = new Tile(new Vector2f(100,520));//original tile            _ 
            Tile tile1 = new Tile(new Vector2f(132, 536));//south tile +32x, + 16y  \
            Tile tile2 = new Tile(new Vector2f (132,504));//east tile +32x, -16y     |_ compared to original tile
            Tile tile3 = new Tile(new Vector2f(68, 504));//north tile -32x, -16y     |
            Tile tile4 = new Tile(new Vector2f(68, 536));//west tile -32x, +16y    _/
            Tile_Spawner tilespawner = new Tile_Spawner();
            AddTile(tilespawner);
            AddTile(tile);
            AddTile(tile2);
            AddTile(tile3);
            AddTile(tile4);//2072 tiles is the screen
            //character location +22x, -32y compared to the tile stood on
            AddGameObject(hero);
            Cloud_Spawner clouds = new Cloud_Spawner();
            AddCloud(clouds);
            Score score = new Score(new Vector2f(1700,0));
            AddUserInterface(score);
            Lives lives = new Lives(new Vector2f(1500, 0));
            AddUserInterface(lives);
            TilesPlaced tiles = new TilesPlaced(new Vector2f(1180, 0));
            AddUserInterface(tiles);
            AdvancedSpellBook spellbook = new AdvancedSpellBook();
            AddUserInterface(spellbook);
        }
        // Get the current score
        public int GetScore()
        {
            return _score;
        }
        public int GetTilesplaced()
        {
            return tilesplaced;
        }
        public void SetTilesPlaced(int tilesplaced)
        {
            this.tilesplaced = tilesplaced;
        }
        public Vector2f GetHeroPos()
        {
            return hero.GetPos();
        }
        // Increase the score
        public void IncreaseScore()
        {
            ++_score;
        }
        public void IncreaseLives()
        {
            ++_lives;
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
                Thread.Sleep(200);
                GameOverScene gameOverScene = new GameOverScene(_score,tilesplaced);
                Game.SetScene(gameOverScene);
            }
        }
        public void GameWon()
        {
            Victory victory = new Victory(_score, tilesplaced);
            Game.SetScene(victory);
        }
        public void ToggleSpellBook()
        {
            if (!spellbook)
            {
                spellbook = true;
            }
            else
            {
                spellbook = false;
            }
        }
        public bool GetSpellBook()
        {
            return spellbook;
        }
    }
}
