namespace SummonersTale.TileEngine
{
    using System;
    using Microsoft.Xna.Framework;

    public class ViewPort
    {
        Vector2 position;
        float speed;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(value, 1f, 16f); }
        }
    }
}
