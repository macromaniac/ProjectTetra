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
    class Menu : Drawer
    {

        Texture2D t2d;

        double x, y;
        public Menu(Game game, SpriteBatch spriteBatch):base(game,spriteBatch)
        {


        }
        public override void init()
        {
            x = 10; y = 10;
            t2d = game.Content.Load<Texture2D>("blackpixel");
        }
        public override void draw(GameTime gameTime)
        {
            y += gameTime.ElapsedGameTime.TotalMilliseconds * .04;
            spriteBatch.Draw(t2d, new Rectangle( (int) x, (int)y, 100, 100), Color.Beige);

        }

    }
}
