
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Zwart : Support.Texture
    {
        
     
        public Zwart(Vector2 position) : base("zwart", position, new Vector2(0.3f, 0.6f))
        {

        }      
        public void Update(GameTime gameTime)
        {   Vector2 previousPos = Position;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) Position.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 1f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Position.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 1f;

            var status = Support.Camera.GetCollision(this);
            if (status != Support.CollisionStatus.Inside)
            {
                Position = previousPos;
            }

        }

    }
}