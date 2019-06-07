using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class GameState
    {
        Zwart zwart;
        List<Toets> Toetsen;

        List<Sols> sol = new List<Sols>();
        float timeToNewSol;
        List<Traan> traans = new List<Traan>();
        float timeToNewTraan;

        
        int score = 0;
       

        public GameState()

        {
            Toetsen = new List<Toets>();
            float hPos = -1.3f;
            for (int x = 0; x < 10; x++)
            { Toetsen.Add(new Toets(new Vector2(hPos, -0.7f), 0.7f)); hPos += 0.3f; }
            zwart = new Zwart(new Vector2(0, -0.53f));

            sol = new List<Sols>();
            timeToNewSol = 2.0f;

            traans = new List<Traan>();
            timeToNewTraan = 1000000f;

        }

        public void Update(GameTime gameTime)
        {

            sol.ForEach(Sols => Sols.Update(gameTime));
            zwart.Update(gameTime);
            traans.ForEach(Traan => Traan.Update(gameTime));

            timeToNewSol -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToNewSol <= 0)
            {
                sol.Add(new Sols(new Vector2(-1.1f + (float)Game1.sRandom.NextDouble() * 2.3f, 0.9f), 0.1f));
                timeToNewSol = 1.5f;
            }
            if (timeToNewTraan <= 0)
            {
                traans.Add(new Traan(new Vector2(-1.7f + (float)Game1.sRandom.NextDouble() * 5f, 0.9f), 0.2f));
                timeToNewTraan = 100000f;
            }

            for (int i = sol.Count - 1; i >= 0; i--)
            {
                if (sol[i].Collides(zwart))
                {
                    score++;
                    sol.RemoveAt(i);
                    timeToNewSol = 1.5f;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    score = 0;
                    sol.RemoveAt(i);
                    zwart.Position.X = 0f;
                    timeToNewSol = 1.8f;
                    timeToNewTraan = 1000000f;



                }
                if (score >= 10)
                {
                    timeToNewSol = 0.5f;
                }
                if (score >= 20)
                {
                    timeToNewSol = 0.05f;

                }
                if (score>= 30)
                {
                    timeToNewSol = 0.000001f;
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            Toetsen.ForEach(toets => toets.Draw());
            zwart.Draw();
            sol.ForEach(Sols => Sols.Draw());
            traans.ForEach(Traan => Traan.Draw());
            for (int i = sol.Count - 1; i >= 0; i--)
            {
                if (sol[i].Position.Y < -1)
                {
                    zwart.Position.X = 5f;
                    Support.Font.PrintAt(new Vector2(-0.05f, 0.25f), "Game Over", Color.MediumVioletRed);
                    Support.Font.PrintAt(new Vector2(-0.05f, 0.2f), "Awh, don't cry", Color.CornflowerBlue);
                    Support.Font.PrintAt(new Vector2(-0.2f, 0.15f), "Score:" + score, Color.Black);
                    Support.Font.PrintAt(new Vector2(-0.2f, 0.1f), "Press SPACE to play again", Color.Black);
                    
                    timeToNewTraan = 0f;
                    timeToNewSol =1000000f;
                }

                if (score >= 10)
                {
                    Support.Font.PrintStatus3("Difficulty: medium", Color.Yellow);
                   Support.Font.PrintStatus2("---------------------", Color.Black);
                }                             
                if (score >= 20)
                {

                   Support.Font.PrintStatus2("Difficulty: easy", Color.LawnGreen);
                   // Support.Font.PrintStatus2("---------------------", Color.Black);
                    Support.Font.PrintStatus4( "Difficulty: hard", Color.Orange);
                    Support.Font.PrintStatus3("-------------------------", Color.Black);

                }
                if(score >= 30)
                {
                 //   Support.Font.PrintStatus2("Difficulty: easy", Color.White);
                 //   Support.Font.PrintStatus2("---------------------", Color.Black);
                 //   Support.Font.PrintStatus4("Difficulty: hard", Color.Black);
                  //  Support.Font.PrintStatus3("-------------------------", Color.Black);
                    Support.Font.PrintStatus4("---------------------", Color.Black);
                    Support.Font.PrintStatus5("Difficulty: EXTREEM", Color.OrangeRed);

                }
                else Support.Font.PrintStatus2( "Difficulty: easy", Color.LawnGreen);

                Support.Font.PrintStatus("Score: " + score, Color.Black);
                
            }
        }
    }
}

