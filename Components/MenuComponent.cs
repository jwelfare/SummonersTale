using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummonersTale.Components
{
    public class MenuComponent
    {
        const int MENUPADDING = 50;

        SpriteFont spriteFont;

        readonly List<string> menuItems = new List<string>();

        int selectedIndex = -1;
        bool mouseOver;

        int width;
        int height;

        Color defaultColor;
        Color highlightColor;

        Texture2D texture;
        Vector2 position;

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width;  }
        }

        public bool MouseOver
        {
            get { return mouseOver; }
        }

        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        public Color HighlightColor
        {
            get { return highlightColor; }
            set { highlightColor = value; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                MathHelper.Clamp(
                    value,
                    0,
                    menuItems.Count - 1);
            }
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D texture)
        {
            this.mouseOver = false;
            this.spriteFont = spriteFont;
            this.texture = texture;
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D texture, string[] menuItems) : this(spriteFont, texture)
        {
            SetMenuItems(menuItems);

            MeassureMenu();
        }

        public void SetMenuItems(string[] items)
        {
            selectedIndex = 0;

            menuItems.Clear();
            menuItems.AddRange(items);

            MeassureMenu();
        }

        private void MeassureMenu()
        {
            width = texture.Width;
            height = 0;

            foreach (string itemString in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(itemString);

                if (size.X > width)
                    width = (int)size.X;

                height += texture.Height + MENUPADDING;
            }

            height -= MENUPADDING;
        }

        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            Vector2 menuPosition = position;
            Point mousePosition = InputSate.MouseState.Position;

            Rectangle buttonRectangle;
            mouseOver = false;

            for (var i = 0; i < menuItems.Count; i++)
            {
                buttonRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

                if (buttonRectangle.Contains(mousePosition))
                {
                    mouseOver = true;
                    selectedIndex = i;
                }

                menuPosition.Y += MENUPADDING + texture.Height;
            }

            if (!mouseOver && (InputSate.CheckKeyReleased(Keys.Up)))
            {
                SelectedIndex = selectedIndex - 1;
            }
            else if (!mouseOver && (InputSate.CheckKeyReleased(Keys.Down)))
            {
                SelectedIndex = selectedIndex + 1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = position;
            Color myColor;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    myColor = HighlightColor;
                }
                else
                {
                    myColor = DefaultColor;
                }

                spriteBatch.Draw(texture, menuPosition, Color.White);

                Vector2 textSize = spriteFont.MeasureString(menuItems[i]);
                Vector2 textPosition = menuPosition + new Vector2(
                    (int)(texture.Width - textSize.X) / 2,
                    (int)(texture.Height - textSize.Y) / 2
                );

                spriteBatch.DrawString(spriteFont,
                    menuItems[i],
                    textPosition,
                    myColor);

                menuPosition.Y += texture.Height + 50;
            }
        }
    }
}
