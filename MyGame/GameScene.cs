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
        private int _lives = 5;//keeps track of lives for the lives UI to track
        private int _score; //keeps track of score for the score UI to track
        private int tilesplaced = 5;//keeps track of tilesplaced for tilesplaced UI to track
        private bool spellbook;//keeps track of the spellbook toggle for what to display, and for different elements to be able to access it.
        private int numofenemies;//keeps track of the number of enemies for music to change
        public Hero hero = new Hero(new Vector2f(122, 488));//sets up the heros attributes outside of the constructor to access the heros position
        
        public GameScene(bool spellbook)
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
            //all of them are added to the scene as tiles
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
            this.spellbook=spellbook;//value spellbook gets, changed depending on whether it's true or not
            AdvancedSpellBook _spellbook = new AdvancedSpellBook(this.spellbook);//spellbook is added with the current boolean value
            AddUserInterface(_spellbook);//added as UI
            
        }
        // Get the current score
        public int GetScore()
        {
            return _score;
        }
        //Get the number of enemies
        public int GetNumOfEnemies()
        {
            return numofenemies;
        }
        //increase the number of enemies
        public void IncreaseEnemyNum()
        {
            numofenemies++;
        }
        //decrease the number of enemies
        public void DecreaseEnemyNum()
        {
            numofenemies--;
        }
        //gets the amount of tiles placed (for the ui & win condition)
        public int GetTilesplaced()
        {
            return tilesplaced;
        }
        //sets the current amount of tiles placed to sync it with the tiles placed list in tilespawner
        public void SetTilesPlaced(int tilesplaced)
        {
            this.tilesplaced = tilesplaced;
        }
        //gets the hero's position for enemy movement
        public Vector2f GetHeroPos()
        {
            return hero.GetPos();
        }
        // Increase the score
        public void IncreaseScore()
        {
            ++_score;
        }
        //increases the amount of lives
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
            {//if lives are at 0 then the scene changes to game over
                Thread.Sleep(200);
                GameOverScene gameOverScene = new GameOverScene(_score,tilesplaced,spellbook);
                Game.SetScene(gameOverScene);
            }
        }
        public void GameWon()//if triggered it changes the scene to indicate you have won
        {
            Victory victory = new Victory(_score, tilesplaced);
            Game.SetScene(victory);
        }
        public void ToggleSpellBook()//function to toggle the spellbook when clicked
        {
            if (!spellbook)//toggles the spellbook if it's on or not
            {
                spellbook = true;
            }
            else
            {
                spellbook = false;
            }
        }
        public bool GetSpellBook()
        {//gets the status of the spellbook
            return spellbook;
        }
    }
}
