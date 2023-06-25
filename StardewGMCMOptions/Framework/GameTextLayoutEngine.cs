// Copyright 2023 Jamie Taylor
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace GMCMOptions.Framework {
    public class GameTextLayoutEngine : ITextLayoutEngine {
        private string formattedText = "";
        public GameTextLayoutEngine() {
        }

        /// <inheritdoc/>
        public int Layout(string text, int width) {
            formattedText = Game1.parseText(text, Game1.smallFont, width);
            return (int)Game1.smallFont.MeasureString(formattedText).Y;
        }

        /// <inheritdoc/>
        public void DrawLastLayout(SpriteBatch b, int left, int top) {
            b.DrawString(Game1.smallFont, formattedText, new Vector2(left, top), Color.Black);
        }

    }
}

