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
    class Block:Drawer
    {
        public Texture2D t2d;
        public Color color;
        public bool isEmpty;
        public bool flagged;
        public int dx, dy;
        public double x_base, y_base;
        public Block(Game game, SpriteBatch spriteBatch, int x, int y) : base(game,spriteBatch)
        {
            t2d = game.Content.Load<Texture2D>("blackpixel");
            isEmpty = true;
            color = new Color(0, 0, 0);
            x_base = x * Variables.blockW;
            y_base = y * Variables.blockH;
            dy = 0; dx = 0;
        }
        public override void init()
        {
        }
        public override void draw(GameTime gameTime)
        {
        }
        public override void update(GameTime gameTime)
        {
        }

        public void flag()
        {
            //TODO
        }

        public void destroy()
        {
            //TODO
        }


        public void setDX(int dx)
        {
            this.dx = dx;
        }
        public void setDY(int dy)
        {
            this.dy = dy;
        }
    }
}
