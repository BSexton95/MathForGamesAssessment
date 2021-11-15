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

        public UIText(float x, float y, Color color, string name, int width, int height, int fontSize, string text = "")
            : base(x, y, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
        }

        public UIText(float x, float y, Color color, string name, string text = " ") : base(x, y, name)
        {
            Text = text;
            Width = 200;
            Height = 200;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = 20;
        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            Raylib.DrawRectangleRec(textBox, Color.BLACK);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, Color.BLUE);
        }
    }
}
