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
        public Grid grid;
        public int life;
        public int points;
        public int point_bleed = 1;

        public Level(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        public override void init()
        {
            grid = new Grid(game, spriteBatch);
            life = 3000;
            points = 0;
        }
        public override void draw(GameTime gameTime)
        {
            grid.draw(gameTime);
            spriteBatch.DrawString(game.Content.Load<SpriteFont>("SpriteFont1"), "Life: " + life + " Points: " + points, new Vector2(
                (float)((double)game.GraphicsDevice.Viewport.Width * 0.95), (float) ((double) game.GraphicsDevice.Viewport.Height * 0.80)), 
                Color.GhostWhite, -1.571f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }
        public override void update(GameTime gameTime)
        {
            grid.update(gameTime);
            life += grid.checkBlocks();
            life-=point_bleed;
            points += point_bleed;
        }
    }
}
