using GameEngine;
using SFML.Audio;
using SFML.System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _lives = 1;//keeps track of lives for the lives UI to track
        private int _score; //keeps track of score for the score UI to track
        private int tilesplaced = 5;//keeps track of tilesplaced for tilesplaced UI to track
        private bool spellbook;//keeps track of the spellbook toggle for what to display, and for different elements to be able to access it.
        private int numofenemies;//keeps track of the number of enemies for music to change
        public Hero hero = new Hero(new Vector2f(122, 488));//sets up the heros attributes
        
        public GameScene()
        {
            //creates a background and adds it to the scene as a background
            Background background = new Background();
            AddBackground(background);
            //initial tiles are created with manual values, and tiles spawner is made so the values don't need to be put manualy
            Tile tile = new Tile(new Vector2f(100, 520));//original tile            _ 
            Tile tile1 = new Tile(new Vector2f(132, 536));//south tile +32x, + 16y  \
            Tile tile2 = new Tile(new Vector2f(132, 504));//east tile +32x, -16y     |_ compared to original tile
            Tile tile3 = new Tile(new Vector2f(68, 504));//north tile -32x, -16y     |
            Tile tile4 = new Tile(new Vector2f(68, 536));//west tile -32x, +16y    _/
            Tile_Spawner tilespawner = new Tile_Spawner();
            AddTile(tilespawner);//all of them are added to the scene as tiles
            AddTile(tile);
            AddTile(tile1);
            AddTile(tile2);
            AddTile(tile3);
            AddTile(tile4);
            
            //character location +22x, -32y compared to the tile stood on
            AddGameObject(hero);//adds the hero as a GameObject
            Cloud_Spawner clouds = new Cloud_Spawner();
            AddCloud(clouds);//creates cloudspawner and adds it to the clouds in between the gameobjects and UI
            Score score = new Score(new Vector2f(1700, 0));//score is added to the top right 
            AddUserInterface(score);//added as a User Interface
            Lives lives = new Lives(new Vector2f(1500, 0)); // lives are added to the top right (left of score)
            AddUserInterface(lives);//added as UI
            TilesPlaced tiles = new TilesPlaced(new Vector2f(1180, 0));//creates tiles placed with it's position at 1180 x right of the middle
            AddUserInterface(tiles);//added as UI
            this.spellbook=false;//default spellbook is false
            AdvancedSpellBook spellbook = new AdvancedSpellBook(this.spellbook);//spellbook is added with the current boolean value
            AddUserInterface(spellbook);//added as UI
            
        }
        public GameScene(bool spellbook)
        {
            Background background = new Background();
            AddBackground(background);
            Tile tile = new Tile(new Vector2f(100, 520));//original tile            _ 
            Tile tile1 = new Tile(new Vector2f(132, 536));//south tile +32x, + 16y  \
            Tile tile2 = new Tile(new Vector2f(132, 504));//east tile +32x, -16y     |_ compared to original tile
            Tile tile3 = new Tile(new Vector2f(68, 504));//north tile -32x, -16y     |
            Tile tile4 = new Tile(new Vector2f(68, 536));//west tile -32x, +16y    _/
            Tile_Spawner tilespawner = new Tile_Spawner();
            AddTile(tilespawner);
            AddTile(tile);
            AddTile(tile1);
            AddTile(tile2);
            AddTile(tile3);
            AddTile(tile4);//2072 tiles is the screen
            //character location +22x, -32y compared to the tile stood on
            AddGameObject(hero);
            Cloud_Spawner clouds = new Cloud_Spawner();
            AddCloud(clouds);
            Score score = new Score(new Vector2f(1700, 0));
            AddUserInterface(score);
            Lives lives = new Lives(new Vector2f(1500, 0));
            AddUserInterface(lives);
            TilesPlaced tiles = new TilesPlaced(new Vector2f(1180, 0));
            AddUserInterface(tiles);
            this.spellbook = spellbook;
            AdvancedSpellBook _spellbook = new AdvancedSpellBook(this.spellbook);
            AddUserInterface(_spellbook);

        }
        // Get the current score
        public int GetScore()
        {
            return _score;
        }
        public int GetNumOfEnemies()
        {
            return numofenemies;
        }
        public void IncreaseEnemyNum()
        {
            numofenemies++;
        }
        public void DecreaseEnemyNum()
        {
            numofenemies--;
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
                GameOverScene gameOverScene = new GameOverScene(_score,tilesplaced,spellbook);
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
