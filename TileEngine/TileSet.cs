namespace SummonersTale.TileEngine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class TileSet
    {
        public int TilesWide = 8;
        public int TilesHigh = 8;
        public int TileWidth = 64;
        public int TileHeight = 64;

        Texture2D tileImage;
        string imageName;
        Rectangle[] sourceRectangles;

        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return tileImage; }
            set { tileImage = value; }
        }

        [ContentSerializer]
        public string TextureName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        [ContentSerializerIgnore]
        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); }
        }

        public TileSet()
        {
            sourceRectangles = new Rectangle[TilesWide * TilesHigh];

            int tile = 0;

            for (int y = 0; y < TilesHigh; y++)
            {
                for (int x = 0; x < TilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                        x * TileWidth,
                        y * TileHeight,
                        TileWidth,
                        TileHeight);

                    tile++;
                }
            }
        }

        public TileSet(int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            this.TilesWide = tilesWide;
            this.TilesHigh = tilesHigh;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;

            this.sourceRectangles = new Rectangle[this.TilesWide * this.TilesHigh];

            int tile = 0;

            for (int y = 0; y < this.TilesHigh; y++)
            {
                for (int x = 0; x < this.TilesWide; x++)
                {
                    this.sourceRectangles[tile] = new Rectangle(
                        x * this.TileWidth,
                        y * this.TileHeight,
                        this.TileWidth,
                        this.TileHeight);

                    tile++;
                }
            }
        }
    }
}
