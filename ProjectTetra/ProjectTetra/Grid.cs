using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ((RegularBlock)(board[1, 1])).wakeUp( new Color(0,0,0));

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
        }

        public void analyzeBlock(Block block, int x, int y)
        {
            followLine(block, x, y, Variables.direction.North);
            followLine(block, x, y, Variables.direction.East);
            followLine(block, x, y, Variables.direction.West);
            followLine(block, x, y, Variables.direction.South);
        }

        public void followLine(Block block, int x, int y, Variables.direction direction)
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

            if ((x >= 0) && (x < Variables.numBlocksX) && (y >= 0) && (y < Variables.numBlocksY))
            {
                if (board[x, y].color == block.color)
                {
                    //keep moving forward
                    block.flag();
                    board[x, y].flag();
                    followLine(block, new_x, new_y, direction);
                }

            }

        }

        public void checkBlocks()
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
        }

        //Returns the deserved points and destroys flagged blocks
        public int pointGather()
        {
            Block block;
            double broken = 0.0;
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
            return (int) Math.Pow(2.0, broken);
        }
    }
}
