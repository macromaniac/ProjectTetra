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
    class Drawer
    {
        protected Game game;
        protected SpriteBatch spriteBatch;
        public Drawer(Game game, SpriteBatch spriteBatch)
        {

            this.game = game;
            this.spriteBatch = spriteBatch;
            init();
        }
        public virtual void init()
        {


        }
        public virtual void draw(GameTime gameTime)
        {

        }
        public virtual void update(GameTime gameTime)
        {

        }
    }
}
