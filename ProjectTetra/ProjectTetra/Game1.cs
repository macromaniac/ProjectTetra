using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


/*
 * Project Tetra
 * 
 * Created 9/28/2013 at Texas A&M's Hackathon by Eric Hornby and Joey Caero.
 * 
 * We started this project in thruth at around 2 AM after spending forever to figure out how to set set up our development environments and get it working 
 * on a phone. Then proceeded an exhausted and mad attempt to give substance to our ideas for a fun game for Windows Phone ending at 1:30 PM. 
 * 
 * All semblance of code quality was thrown out the window. I was ready to go to bed before we had even started the programming. Joey and I are planning on
 * rewriting much of those code, but with actual code quality as a concern. As of now, X doesnt always mean x and y doesnt always mean y. There are even
 * worse things than that hiding around.
 * 
 * So basically, if you're looking at this, feel free to laugh in hilarity at the abominations you see within and please do not use this as an example
 * of good code!
 * 
 * Cheers!
 * 
 * */

namespace ProjectTetra
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Menu menu;
        public SpriteBatch spriteBatch { get; set; }
        public static int stuff { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }
        protected override void Initialize()
        {
            base.Initialize();
            menu = new Menu(this,spriteBatch);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            menu.update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            menu.draw(gameTime);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
