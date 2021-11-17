using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;

        /// <summary>
        /// UIText constructor
        /// </summary>
        /// <param name="x">The x position of the text box</param>
        /// <param name="y">The y position of the text box</param>
        /// <param name="color">The color of the text</param>
        /// <param name="name">The text name</param>
        /// <param name="width">How wide the text box is from left to right</param>
        /// <param name="height">How large from top to bottom the text box is</param>
        /// <param name="fontSize">The size of the text</param>
        /// <param name="text"> The text to be displayed on screen</param>
        public UIText(float x, float y, Color color, string name, int width, int height, int fontSize, string text = "")
            : base(x, y, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
        }

        //Overload that sets the Width, Height, and Font size.
        public UIText(float x, float y, Color color, string name, string text = " ") : base(x, y, name)
        {
            Text = text;
            Width = 300;
            Height = 200;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = 20;
        }

        /// <summary>
        /// Draws a text box
        /// </summary>
        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            //Raylib.DrawRectangleRec(textBox, Color.BLACK);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, Color.BLUE);
        }
    }
}
