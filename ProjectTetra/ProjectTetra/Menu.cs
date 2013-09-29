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

//We didn't have time to implement a menu, tragically

namespace ProjectTetra 
{
    class Menu : Drawer
    {

        public UI ui;
        public Menu(Game game, SpriteBatch spriteBatch):base(game,spriteBatch)
        {

        }
        public override void init()
        {
            ui = new UI(game, spriteBatch);
        }
        public override void draw(GameTime gameTime)
        {
            ui.draw(gameTime);
        }
        public override void update(GameTime gameTime)
        {
            ui.update(gameTime);
        }

    }
}
