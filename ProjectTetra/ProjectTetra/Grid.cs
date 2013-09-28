using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections;
namespace ProjectTetra
{
    class Grid : Drawer
    {
        Texture2D t2d;
        public Block[,] board = new Block[4, 6];
        
        int solid_count;
        Random random = new Random();

        bool stop = false;
        int slow_factor = 10;
        int spawn_buffer =0;

        public bool isMovableSpace(int x, int y, Variables.direction dir)
        {
            if (dir == Variables.direction.North)
                return isMovableSpace(x, y + 1, 0, 1);
            if (dir == Variables.direction.South)
                return isMovableSpace(x, y - 1, 0, -1);
            if (dir == Variables.direction.East)
                return isMovableSpace(x + 1, y, 1, 0);
            //west
            return isMovableSpace(x - 1, y, -1, 0);
        }

        public bool isMovableSpace(int x, int y, int dx, int dy)
        {
            if (!validX(x))
                return false;
            if (!validY(y))
                return false;
            if (board[x, y].isEmpty)
                return true;
            return isMovableSpace(x + dx, y + dy, dx, dy);
        }

        public bool validX(int x)
        {
            if (x >= board.GetLength(0) || x < 0)
                return false;
            return true;
        }
        public bool validY(int y)
        {
            if (y >= board.GetLength(1) || y < 0)
                return false;
            return true;
        }
        //dx is actually dy because of the screen ><
        public void moveSpace(int x, int y, int dx, int dy)
        {
            if (board[x, y].isEmpty)
                return;
            board[x, y].setDX(dx);
            board[x, y].setDY(dy);
            if (dy > 0)
            {
                if (!validX(x+1))
                    return;
                moveSpace(x + 1, y, dx, dy);
            }
            if (dy < 0)
            {
                if (!validX(x-1))
                    return;
                moveSpace(x - 1, y, dx, dy);
            }
            if (dx > 0)
            {
                if (!validY(y + 1))
                    return;
                moveSpace(x, y + 1, dx, dy);
            }
            if (dx < 0)
            {
                if (!validY(y - 1))
                    return;
                moveSpace(x, y - 1, dx, dy);
            }
        }
        public Grid(Game game, SpriteBatch spriteBatch) : base(game,spriteBatch)
        {

        }
        public void resetGridDXY()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j].dx = 0;
                    board[i, j].dy = 0;
                }
            }
        }
        public override void init()
        {
            t2d = game.Content.Load<Texture2D>("blackpixel");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = new RegularBlock(game, spriteBatch, i,j);
                }
            }
            solid_count = 0;
        }

        public override void draw(GameTime gameTime)
        {
            for (int x = 0; x < Variables.numBlocksX; ++x)
                for (int y = 0; y < Variables.numBlocksY; ++y)
                    spriteBatch.Draw(t2d, 
                        new Rectangle( (int) y*Variables.blockHP, (int)x *Variables.blockWP,
                            Variables.blockHP, Variables.blockWP), new Color(100 + x*5 + y*5,100+x*5 +y*5,100+x*5 + y*5));
            for (int x = 0; x < Variables.numBlocksX; ++x)
                for (int y = 0; y < Variables.numBlocksY; ++y)
                {
                    board[x, y].draw(gameTime);
                }
        }

        public override void update(GameTime gameTime)
        {
            if (!stop)
            {
                if (random.Next((int)(Math.Sqrt((double)solid_count) * slow_factor) + spawn_buffer) <= 1)
                    spawnBlock();
            }

        }

        private void analyzeBlock(Block block, int x, int y)
        {
            List<Block> blocks = new List<Block>();
            if (sameNeighborColor(block.color, x,y))
            {
                blocks.Add(block);
                followLine(blocks, x, y, Variables.direction.North);

                blocks = new List<Block>();
                blocks.Add(block);
                followLine(blocks, x, y, Variables.direction.East);
                
                blocks = new List<Block>();
                blocks.Add(block);
                followLine(blocks, x, y, Variables.direction.West);

                blocks = new List<Block>();
                blocks.Add(block);
                followLine(blocks, x, y, Variables.direction.South);
            }
        }

        private void followLine(List<Block> blocks, int x, int y, Variables.direction direction)
        {
            int new_x = x;
            int new_y = y;

            if (direction == Variables.direction.North)
            {
                new_y++;
            }
            if (direction == Variables.direction.West)
            {
                new_x--;
            }
            if (direction == Variables.direction.South)
            {
                new_y--;
            }
            if (direction == Variables.direction.East)
            {
                new_x++;
            }

            if (isInBound(new_x,new_y))
            {
                if (board[new_x, new_y].color == blocks[0].color)
                {
                    //keep moving forward
                    blocks.Add(board[new_x, new_y]);
                    followLine(blocks, new_x, new_y, direction);
                }

            }

            if (blocks.Count >= 3)
            {
                foreach (Block block in blocks){
                    block.flag();
                }
            }

        }

        public int checkBlocks()
        {
            Block block;
            for (int x = 0; x < Variables.numBlocksX; ++x)
            {
                for (int y = 0; y < Variables.numBlocksY; ++y)
                {
                    block = board[x,y];
                    if (!block.isEmpty)
                    {
                        analyzeBlock(block, x, y);
                    }
                }
            }
            return pointGather()*500;
        }

        //Returns the deserved points and destroys flagged blocks
        private int pointGather()
        {
            Block block;
            int broken = 0;
            for (int x = 0; x < Variables.numBlocksX; ++x)
            {
                for (int y = 0; y < Variables.numBlocksY; ++y)
                {
                    block = board[x, y];
                    if (block.flagged)
                    {
                        block.reset();
                        broken++;
                    }
                }
            }
            solid_count -= broken;
            if (broken == 0) return 0;
            return (int) Math.Pow(2.0, broken);
        }

        private bool isInBound(int x, int y){
            return ((x >= 0) && (x < Variables.numBlocksX) && (y >= 0) && (y < Variables.numBlocksY));
        }

        private Block safeGetBlock(int x, int y)
        {
            if (isInBound(x, y))
            {
                return board[x, y];
            }
            else return new RegularBlock(game, spriteBatch, 0, 0);
        }

        private bool sameNeighborColor(Color color, int x, int y){
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (safeGetBlock(x+i, y+j).color == color && (i != j ) && ((i == 0) || (j == 0)))
                    {
                        Debug.WriteLine(color);
                        return true;
                    }
                }
            }
            return false;
        }

        public void spawnBlock()
        {
            Color random_color = Variables.colors[random.Next(0, 5)];
            int x = random.Next(0, (int) Variables.numBlocksX);
            int y = random.Next(0, (int)Variables.numBlocksY);
            
            RegularBlock new_block;

            if (board[x, y].isEmpty)
            {
                while (sameNeighborColor(random_color, x, y))
                {
                    random_color = Variables.colors[random.Next(0, 5)];
                }
                new_block = new RegularBlock(game, spriteBatch, x, y);
                new_block.wakeUp(random_color);
                board[x, y] = new_block;
                solid_count++;
            }
            else if (solid_count >= Variables.numBlocksX * Variables.numBlocksY)
            {
                //TODO: Declare End of Game, map is full!
                stop = true;
            }
            else 
            {
                //Try again until a proper spawn is found.
                spawnBlock();
            }
        }

        public void die()
        {
            stop = true;
        }

        public bool getStop()
        {
            return stop;
        }

    }
}
