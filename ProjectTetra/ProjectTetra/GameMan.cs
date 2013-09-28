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
    class GameMan:Drawer
    {
        public Level level;

        public GameMan(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        public override void init()
        {
            level = new Level(game, spriteBatch);
        }
        public override void draw(GameTime gameTime)
        {
            level.draw(gameTime);
        }
        public override void update(GameTime gameTime)
        {
            level.update(gameTime);
        }

    }
}
