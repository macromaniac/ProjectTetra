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

        public void checkBlocks()
        {
        }
    }
}
