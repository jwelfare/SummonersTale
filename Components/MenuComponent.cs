namespace SummonersTale.Components
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class MenuComponent
    {
        const int MENUPADDING = 50;

        SpriteFont spriteFont;

        readonly List<string> menuItems = new List<string>();

        int selectedIndex = -1;

        int width;
        int height;

        Color defaultColor = Color.White;
        Color highlightColor = Color.Beige;

        Vector2 position;

        bool mouseOver;

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width;  }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
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
                selectedIndex = MathHelper.Clamp(
                    value,
                    0,
                    menuItems.Count - 1);
            }
        }

        public MenuComponent(SpriteFont spriteFont)
        {
            mouseOver = false;
            this.spriteFont = spriteFont;
        }

        public MenuComponent(SpriteFont spriteFont, string[] menuItems) : this(spriteFont)
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
            width = 0;
            height = 0;

            foreach (string itemString in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(itemString);

                if (size.X > width)
                {
                    width = (int)size.X;
                }

                if (size.Y > height)
                {
                    height = (int)size.Y;
                }

                height += (int)size.Y + MENUPADDING;
            }

            height -= MENUPADDING;
        }

        public void Update()
        {
            Vector2 currentPosition = this.position;
            Point mousePosition = InputState.MouseState.Position;

            Rectangle buttonRectangle;
            this.mouseOver = false;

            for (var i = 0; i < this.menuItems.Count; i++)
            {
                Vector2 size = this.spriteFont.MeasureString(this.menuItems[i]);

                buttonRectangle = new Rectangle(
                    (int)currentPosition.X,
                    (int)currentPosition.Y,
                    (int)size.X,
                    (int)size.Y);

                if (buttonRectangle.Contains(mousePosition))
                {
                    this.mouseOver = true;
                    this.selectedIndex = i;
                }

                currentPosition.Y += MENUPADDING + size.Y;
            }

            if (!mouseOver && InputState.CheckKeyReleased(Keys.Up))
            {
                this.SelectedIndex = this.selectedIndex - 1;
            }
            else if (!mouseOver && InputState.CheckKeyReleased(Keys.Down))
            {
                this.SelectedIndex = this.selectedIndex + 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 currentPosition = this.position;
            Color myColor;

            for (int i = 0; i < this.menuItems.Count; i++)
            {
                if (i == this.SelectedIndex) {
                    myColor = this.HighlightColor; }
                else {
                    myColor = this.DefaultColor; }

                Vector2 textSize = this.spriteFont.MeasureString(this.menuItems[i]);

                spriteBatch.DrawString(
                    this.spriteFont,
                    this.menuItems[i],
                    currentPosition,
                    myColor);

                currentPosition.Y += textSize.Y + 50;
            }
        }
    }
}
