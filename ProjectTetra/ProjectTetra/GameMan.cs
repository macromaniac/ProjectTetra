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
using System.Diagnostics;

namespace ProjectTetra
{
    class GameMan : Drawer
    {
        public Level level;
        bool isTouched;
        int xbase, ybase;
        int xbBase, ybBase;
        public GameMan(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        public override void init()
        {
            level = new Level(game, spriteBatch);
            isTouched = false;
            xbase = 0;
            ybase = 0;
        }
        public override void draw(GameTime gameTime)
        {;
            level.draw(gameTime);
        }
double leftEdgeDistP;
double rightEdgeDistP;
double topEdgeDistP;
     double bottomEdgeDistP;



        public Variables.direction getClosestAvailableDirection(int curY, int curX, int bX, int bY)
        {
            //check the directions that are available, if a direction is
            //available then check to see if its possible to be the closest one

            //The edges, in pixels
            int leftEdgeP = bX * Variables.blockWP;
            int rightEdgeP = (bX+1)*Variables.blockWP;
            int topEdgeP = (bY+1)*Variables.blockHP;
            int bottomEdgeP = (bY)*Variables.blockHP;

            //The percentage of the distance traveled to each edge
            leftEdgeDistP = ((double)(ybase - curX)) / (double)(ybase - leftEdgeP);
           rightEdgeDistP = ((double)(curX - ybase)) / (double)(rightEdgeP - ybase);
           topEdgeDistP = ((double)(xbase-curY)) / (double)( xbase - topEdgeP);
           bottomEdgeDistP = ((double)(curY-xbase)) / (double)(bottomEdgeP-xbase);

           double[] dists = new double[4] 
           { Variables.clipNumber(topEdgeDistP) ,
            Variables.clipNumber(rightEdgeDistP),
            Variables.clipNumber(bottomEdgeDistP), 
            Variables.clipNumber(leftEdgeDistP)};
           //Debug.WriteLine( dists[0].ToString() + " E: " + dists[1].ToString() + " S: " + dists[2].ToString() + " W: " + dists[3].ToString());
           //Debug.WriteLine(dists[2]);
           double max = -100;
           int maxd = -1;
            for(int i=0;i<dists.Length;++i)
                if (dists[i] > max)
                {
                    if (level.grid.isMovableSpace(bX, bY, (Variables.direction)i))
                    {
                        max = dists[i];
                        maxd = i;
                    }
                }
            if(maxd<0)
               return Variables.direction.Nowhere;
            if ((Variables.direction)maxd == Variables.direction.West)
                Debug.WriteLine(max);
            if ((Variables.direction)maxd == Variables.direction.South)
                Debug.WriteLine(max);
            return (Variables.direction)maxd;
        }
        private void moveBlock(int bX, int bY, Variables.direction dir)
        {

        }
        private bool setPos(int curY, int curX, int bX, int bY)
        {
            bool didMove = false;
            Variables.direction dir = getClosestAvailableDirection(curY, curX, bX, bY);
            if (dir == Variables.direction.Nowhere)
                return false;
            //This is sloppy, it uses variables set in the backgruond by getClosestAvailDir, running out of time X_X
            if (dir == Variables.direction.North)
            {
                if (topEdgeDistP >= 1)
                {
                    moveBlock(bX, bY, Variables.direction.North);
                    //update the touch coordinates!
                    //ybBase = bY + 1;
                    //xbase = curX;
                    //ybase = curY;
                    didMove = true;
                }
                else
                {
                    level.grid.board[bX, bY].setDY(0);
                    level.grid.board[bX, bY].setDX(  (int)(Variables.blockHP * Variables.clipNumber(topEdgeDistP)));
                }
            }
            if (dir == Variables.direction.South)
            {
                if (bottomEdgeDistP >= 1)
                {
                    moveBlock(bX, bY, Variables.direction.South);
                    //update the touch coordinates!
                    //ybBase = bY - 1;
                    //xbase = curX;
                    //ybase = curY;
                    didMove = true;

                }
                else
                {
                    level.grid.board[bX, bY].setDY(0);
                    level.grid.board[bX, bY].setDX(-(int)(Variables.blockHP * Variables.clipNumber(bottomEdgeDistP)));
                }
            }
            if (dir == Variables.direction.East)
            {
                if (rightEdgeDistP >= 1)
                {
                    moveBlock(bX, bY, Variables.direction.East);
                    //update the touch coordinates!
                    //xbBase = bX - 1;
                    //xbase = curX;
                    //ybase = curY;
                    didMove = true;

                }
                else
                {
                    level.grid.board[bX, bY].setDY((int)(Variables.blockWP * Variables.clipNumber(rightEdgeDistP)));
                    level.grid.board[bX, bY].setDX(0);
                }
            }


            if (dir == Variables.direction.West)
            {
                if (leftEdgeDistP >= 1)
                {
                    moveBlock(bX, bY, Variables.direction.West);
                    //update the touch coordinates!
                    //xbBase = bX - 1;
                    //xbase = curX;
                    //ybase = curY;
                    didMove = true;

                }
                else
                {
                    level.grid.board[bX, bY].setDY((int)(Variables.blockWP * -Variables.clipNumber(leftEdgeDistP)));
                    level.grid.board[bX, bY].setDX(0);
                }
            }

            return didMove;
        }
        public override void update(GameTime gameTime)
        {
            level.update(gameTime);
            TouchCollection tc = TouchPanel.GetState();
            if (tc.Count > 0)
            {
                double touchX = tc[0].Position.X;
                double touchY = tc[0].Position.Y;

                int touchXB = (int)(touchY / Variables.blockWP);
                int touchYB = (int)(touchX / Variables.blockHP);

                if (isTouched == false)
                {
                    xbase = (int)touchX;
                    ybase = (int)touchY;
                    xbBase = touchXB;
                    ybBase = touchYB;
                    isTouched = true;
                }
                if ( touchYB< (Variables.numBlocksY) && touchXB<(Variables.numBlocksX))
                {
                   // level.grid.board[xbBase, ybBase].setDX((int)(touchX-xbase));
                   // level.grid.board[xbBase, ybBase].setDY((int)(touchY-ybase));
                }
                int num = 0;
                setPos((int)touchX, (int)touchY, xbBase, ybBase);
            }
            else
            {
                //if you just let go then reset the DX/DYs so there are no floating blocks
                if(isTouched==true)
                    level.grid.resetGridDXY();
                isTouched = false;
            }
        }
    }

}
