using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Sols : Support.Texture
    {
        Vector2 direction;

        public Sols(Vector2 position, float size) : base("sol", position, new Vector2(size))
        {
            direction.Y = -1f;

        }

        public float Radius
        {

            get => size.X * 0.3f;

            set => size.X = size.Y = value * 2.0f;
        }

        public void Update(GameTime gameTime)
        {
            var status = Support.Camera.GetCollision(this);
            switch (status)
            {

                case Support.CollisionStatus.Top:
                    direction.Y *= -1;
                    Position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Support.CollisionStatus.Left:
                case Support.CollisionStatus.Right:
                    direction.X *= -1;
                    Position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
            Position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
