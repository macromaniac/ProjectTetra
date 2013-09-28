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

namespace ProjectTetra
{
    class GameMan : Drawer
    {
        public Level level;
        bool isTouched;
        int xbase, ybase;

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
        {
            level.draw(gameTime);
        }
        public override void update(GameTime gameTime)
        {
            level.update(gameTime);
            TouchCollection tc = TouchPanel.GetState();
            if (tc.Count > 0)
            {
                double touchX = tc[0].Position.X;
                double touchY = tc[0].Position.Y;

                int touchXB = (int)(touchX / Variables.blockWP);
                int touchYB = (int)(touchY / Variables.blockHP);

                if (isTouched == false)
                {
                    xbase = (int)touchX;
                    ybase = (int)touchY;
                    isTouched = true;
                }
                if ( touchYB< (Variables.numBlocksY-1) && touchXB<(Variables.numBlocksX-1))
                {
                    level.grid.board[touchXB, touchYB].setDX(100);
                }
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
