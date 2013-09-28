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
    public static class Variables
    {
        public enum direction {North, East, South, West};
        public static double gridStartX = .2;
        public static double blockW = .25;
        public static double blockH = .15;
        public static double numBlocksX = 4;
        public static double numBlocksY = 6;
        public static int screenW = 480;
        public static int screenH = 800;
        public static Color[] colors = new Color[5] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue};
        public static int getX(double percentage)
        {
            return (int)(screenW * percentage);
        }
        public static int getY(double percentage)
        {
            return (int)(screenH * percentage);
        }
        public static int blockWP = getX(blockW);
        public static int blockHP = getY(blockH);
    }
}
