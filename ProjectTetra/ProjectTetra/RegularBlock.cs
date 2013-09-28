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
    class RegularBlock : Block
    {
        Color oldcolor;
        int dieframe;
        public RegularBlock(Game game, SpriteBatch spriteBatch, int x, int y)
            : base(game, spriteBatch,  x, y)
        {
        }
        public override void init()
        {
            base.init();    //initialize
            isDying = false;
        }
        public override void draw(GameTime gameTime)
        {
            if(!isEmpty)
                spriteBatch.Draw(t2d, 
                    new Rectangle( ((int) ( y_base + dx)), ((int)(x_base +dy)),
                    Variables.blockHP, Variables.blockWP), color);
            if (isDying)
            {
                dieframe++;
                double dieframeP=(6-(double)dieframe)/6;
                Color curDieColor = new Color((int)(oldcolor.R * dieframeP), (int)(oldcolor.G * dieframeP), (int)(oldcolor.B * dieframeP));
                spriteBatch.Draw(t2d,
                    new Rectangle( ((int) ( y_base + dx)), ((int)(x_base +dy)),
                    Variables.blockHP, Variables.blockWP),
                    curDieColor
                 );
                if (dieframe == 6)
                {
                    dieframe = 0;
                    isDying = false;
                }
            }

        }
        public override void update(GameTime gameTime)
        {
        }
        public void wakeUp(Color toSet)
        {
            isEmpty = false;
            color = toSet;
        }

        public override void reset()
        {
            dieframe = 0;
            flagged = false;
            isEmpty = true;
            oldcolor = color;
            color = new Color(0, 0, 0);
        }
    }
}