using GameEngine;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Security.Cryptography;

namespace MyGame
{
    class Enemy : GameObject
    {
        
        private const int movedelay = 1000;
        private int _movetimer = movedelay;//when enemy spawns it takes a second for them to move
        private readonly Sprite _sprite = new Sprite();
        private List<Vector2f> placedtiles = new List<Vector2f>();
        public Enemy(Vector2f pos,List<Vector2f> tiles)
        {//Enemy character GameObject
            _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");
            _sprite.Position = new Vector2f(pos.X+22,pos.Y-32);//same way of standing on tile as hero
            placedtiles=tiles;//gets a postion for the enemy to spawn and the current list of placed tiles
            AssignTag("Enemy");//assigns a tag for collision with spells
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            Vector2f pos = _sprite.Position;
            Vector2f heropos = scene.GetHeroPos();
            int mselapsed = elapsed.AsMilliseconds();
            if (0<_movetimer)
            {
                _movetimer-=mselapsed;            
            }
            float x = pos.X;
            float y = pos.Y;
            float hx = heropos.X;//enemy goes to the hero pos
            float hy = heropos.Y; //creates an instance of gamescene, and gets the current position x & y and the target X & Y aswell
            if (hy==y&&hx==x)//if they are exactly the same (same position) then it removes the enemy and decreases the lives
            {
                MakeDead();
                scene.DecreaseLives();

            }
            if (_movetimer<=0)
            {
                bool move = false;//checks if it can move if it does only then does it reset the move timer
                if (hx < x&&hy<y)//if the enemy is to the right and below the hero (south) then it moves north 
                {
                    //movement north 
                    
                    x -= 32;
                    y -=16;//moves the sprite to the tile
                    move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;//goes through every sing placed itle and checks to see if the enmy is standing on them
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//compared with ranges to avoid evaluating floats in the event of a floating point error
                        {
                            if (-30<=y&&0<=x)//if the move is within the game borders
                            {
                                Tile redtile = new Tile(new Vector2f(x+10, y+48), "red");
                                scene.AddTile(redtile);
                                move = true;//it moves
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                               
                            }
                        }
                    }
                    if (!move)//if it doesn't move it resets the movement to the tile
                    {
                        x+=32;
                        y+=16;
                    }
                }
                else if (hx<x&&y<hy)// if the enemy is to the right and above the hero (east) then it moves west
                {
                    //movement west
                    x -= 32;
                    y +=16;//moves the sprite to the tile
                    move = false;
                    for (int num = 0; num<placedtiles.Count; num++)
                    {
                        float ptx = placedtiles[num].X;//goes through all the placed tiles and sees if the move is on a placed tile
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//does this through a range because float==float is bad
                        {
                            if (0<=x&&y<=1030)//if it is within the game borders
                            {

                                Tile redtile = new Tile(new Vector2f(x+10, y+16), "red");
                                scene.AddTile(redtile);
                                move = true;//moves
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                             
                            }
                        }
                    }
                    if (!move)//if the move failed (not in tile or border etc...) resets position
                    {
                        x+=32;
                        y-=16;
                    }
                }
                else if (x<hx&&y<hy)//if the enemy is to the left and above the hero (north) then it moves south
                {
                    //movement south
                    x += 32;
                    y +=16;//moves the values
                    move = false;
                    for (int num = 0; num<placedtiles.Count; num++)//goes through the list of placed tiles
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//checks if the move is on a tile
                        {//doesn't equate floats instead equates a range
                            if (x<=1900&&y<=1030)//if it's within the game border
                            {
                                Tile redtile = new Tile(new Vector2f(x-54, y + 16), "red");
                                scene.AddTile(redtile);
                                move = true;//moves
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");//facing to the south
                          
                            }

                        }
                    }
                    if (!move)//if error (no tile there, out of border etc..) resets the position
                    {


                        x-=32;
                        y-=16;

                    }

                }

                else if (x < hx&&hy<y)//if the enemy is to the left and below the hero (west) it moves east
                {
                    //movement east
                    x += 32;
                    y -=16;//moves
                    move = false;
                    for (int num = 0; num<placedtiles.Count; num++)//goes through each tile if the enemy move is on a tile or not
                    {
                        float ptx = placedtiles[num].X;
                        float pty = placedtiles[num].Y;
                        if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//compares a range of float values instead of floats directly
                        {
                            if (x<=1900&&-30<=y)//if it's inside the game window
                            {
                                Tile redtile = new Tile(new Vector2f(x - 54, y+48), "red");
                                scene.AddTile(redtile);
                                move=true;//moves
                                _sprite.Texture = Game.GetTexture("../../../Resources/enemy east.png");//facing to the east
                               
                            }
                        }
                    }
                    if (!move)//if error occurs during movement (out of game window or not on tile)
                    {
                        x-=32;//reverts the move
                        y+=16;
                    }




                }
                else if (hx ==x&&hy!=y)//if the enemy is in the same column but not row
                {
                    if (hy<y)//if the enemy is below moves up
                    {
                        //movement northeast
                        y -=32;//moves up one tile
                       move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;//goes through placed tiles sees if move is on tile or not
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//compares a range of floats
                            {
                                if (-30<=y&&0<=x)//if it is in the game window
                                {
                                    Tile redtile = new Tile(new Vector2f(x-22, y+64), "red");
                                    scene.AddTile(redtile);
                                    move = true;//moves
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                                    
                                }
                            }
                        }
                        if (!move)//if error occured durring movement doesn't move
                        {
                            y+=32;
                        }
                    }
                    if (y<hy)//if the enemy is above moves down.
                    {
                        //movement southwest
                        
                        
                        y +=32;//moves down
                        move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;//goes through placed tile if tile is a stood on tile
                            float pty = placedtiles[num].Y;
                            if (pty<y+33&&y+31<pty)//compares a range instead of direct float values
                            {
                                if (0<=x&&y<=1030)//if the move isn't out of the window
                                {
                                    Tile redtile = new Tile(new Vector2f(x-22, y), "red");
                                    scene.AddTile(redtile);
                                    move = true;//moves
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy west.png");//facing to the west texture
                                  
                                }
                            }
                        }
                        if (!move)//error occured resets move
                        {
                            y-=32;
                        }
                    }
                }
                else if (hy==y && hx !=x)//if in the same row not column
                {
                    if (hx<x)//if the enemy is to the right, move left
                    {
                        //movement northwest
                        x -= 64;
                        move = false;
                        for (int num = 0; num<placedtiles.Count; num++)//goes through placed tiles
                        {
                            float ptx = placedtiles[num].X;//if move is on tile
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//compares range not direct float==float
                            {
                                if (-30<=y&&0<=x)//if in game window
                                {
                                    Tile redtile = new Tile(new Vector2f(x+42, y+32), "red");
                                    scene.AddTile(redtile);
                                    move = true;//moves
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy north.png");//facing to the north texture
                                   
                                }
                            }
                        }
                        if (!move)//if error occured resets moves
                        {
                            x+=64;
                        }
                       
                    }
                    if (x<hx)//if the enemy is to the left, move right
                    {
                        //movement southeast
                        x += 64;//predicts the move by adding the value but not updating sprite.Position
                        move = false;
                        for (int num = 0; num<placedtiles.Count; num++)
                        {
                            float ptx = placedtiles[num].X;
                            float pty = placedtiles[num].Y;
                            if (ptx<x-21&&x-23<ptx&&pty<y+33&&y+31<pty)//goes through placed tiles if not on a tile the move is never true
                            {//checks a range instead of float==float
                                if (x<=1900&&y<=1030)
                                {
                                    Tile redtile = new Tile(new Vector2f(x-86, y+32), "red");
                                    scene.AddTile(redtile);
                                    move = true;//moves
                                    _sprite.Texture = Game.GetTexture("../../../Resources/enemy south.png");//facing to the south
                                   
                                }

                            }
                        }
                        if (!move)
                        {//if move is not true an error occured (no tiles, out of bounds etc...)


                            x-=64;//resets movement

                        }
                    }
                }

                if (move)
                {
                    Tile tile = new Tile(new Vector2f(x-22, y+32), "purple");
                    scene.AddTile(tile);
                    _movetimer = movedelay;//if the character moved it resets the move timer
                }
            }
            
             
            _sprite.Position = new Vector2f(x, y);//updates the sprite position
            SetPosition(new Vector2f(x-22,y+32));//shows the tile it is on for it to get rendered
            _movetimer++;// tracks the time it was between moves
        }
        
        public override FloatRect GetCollisionRect()
        {//gets the collision rectangle 
            return _sprite.GetGlobalBounds();// if called upon it gives the sprite's bounds
        }
        Random rng = new Random();
        public override void HandleCollision(GameObject otherGameObject)//in case of collision
        {
            if (otherGameObject.HasTag("spell"))//if it gets it by a spell
            {
                otherGameObject.MakeDead();//removes the other game object
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.IncreaseScore();//increases the score
                scene.DecreaseEnemyNum();//decreases the number of enemies read by the game
                Potion potion = new Potion(new Vector2f(_sprite.Position.X-22, _sprite.Position.Y+32));//the sprites position is +22X -32Y so it gets the tile it needs to spawn on
                scene.AddGameObject(potion);//if you hit the enemy it gives your life that it cost for the spell back  
            }
            MakeDead();
        }

    }
}