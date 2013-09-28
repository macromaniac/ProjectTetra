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
    class Level:Drawer
    {
        public Block block;

        public Level(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        public override void init()
        {
            block = new Block(game,spriteBatch);
        }
        public override void draw(GameTime gameTime)
        {
            block.draw(gameTime);
        }
        public override void update(GameTime gameTime)
        {
            block.update(gameTime);
        }
    }
}
