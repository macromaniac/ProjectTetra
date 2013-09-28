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
        public int level;

        public Level(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        public override void init()
        {
            grid = new Grid(game, spriteBatch);
            life = 2000;
            points = 0;
            level = 1;
        }
        public override void draw(GameTime gameTime)
        {
            grid.draw(gameTime);
            spriteBatch.DrawString(game.Content.Load<SpriteFont>("SpriteFont1"), "Level: " + level + "    Life: " + life + "    Points: " + points, new Vector2(
                (float)((double)game.GraphicsDevice.Viewport.Width * 0.95), (float) ((double) game.GraphicsDevice.Viewport.Height * 0.98)), 
                Color.DarkRed, -1.571f, new Vector2(1.2f, 1.2f), 1f, SpriteEffects.None, 0f);
        }
        public override void update(GameTime gameTime)
        {
            if ((points > 500) && level < 2)
            {
                levelUp();
            }

            if ((points > 1100) && level < 3)
            {
                levelUp();
            }

            if ((points > 1800) && level < 4)
            {
                levelUp();
            }

            if ((points > 2600) && level < 5)
            {
                levelUp();
            }

            if ((points > 3500) && level < 6)
            {
                levelUp();
            }

            if ((points > 4400) && level < 7)
            {
                levelUp();
            }

            if ((points > 5400) && level < 8)
            {
                levelUp();
            }

            if ((points > 6500) && level < 9)
            {
                levelUp();
            }

            if ((points > 7700) && level < 10)
            {
                levelUp();
            }

            if ((points > 9000) && level < 11)
            {
                levelUp();
            }

            if ((points > 12000) && level < 12)
            {
                levelUp();
            }
            if ((points > 15000) && level < 13)
            {
                levelUp();
            }

            if ((points > 30000) && level < 14)
            {
                levelUp();
            }

            if ((points > 50000) && level < 15)
            {
                levelUp();
            }

            if (!grid.getStop())
            {
                grid.update(gameTime);
                life += grid.checkBlocks();
                life -= point_bleed;
                points += point_bleed;
                if (life <= 0)
                {
                    grid.die();
                }
            }
            else
            {
                life = 0;
            }

            
        }

        public void levelUp()
        {
            level++;
            point_bleed++;
            grid.levelUp(level);
        }
    }
}
