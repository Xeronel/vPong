using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace vPong
{
    class BrickProperties
    {
        static void BrickLife()
        {
            List<int> _brickLife = new List<int>(new int[] 
            {
                //0,  //Invisible Brick
                1,  //Dirt Brick
                2,  //Ice Brick
                3,  //Rock Brick
                4,  //Living Brick
                30  //Metal Brick
            });
            List<String> _brickTexture = new List<String>(new String[] 
            {
                "Dirt",
                "Ice",
                "Rock",
                "Meat",
                "Metal"
            });

        }



    }
}
