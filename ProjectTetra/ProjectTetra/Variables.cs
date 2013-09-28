using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
