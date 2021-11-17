using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    class PlayerHud : Actor
    {
        private UIText _enemys;
        private UIText _lives;

        /// <summary>
        /// Player Hud constructor
        /// </summary>
        /// <param name="enemys">The UI text for enemys</param>
        /// <param name="lives">The UI text for lives</param>
        public PlayerHud(UIText enemys, UIText lives)
        {
            _enemys = enemys;
            _lives = lives;
        }

        /// <summary>
        /// Calls start for the UIText of enemys and lives
        /// </summary>
        public override void Start()
        {
            base.Start();
            _enemys.Start();
            _lives.Start();
        }

        /// <summary>
        /// Updates the enemy counter and the player lives every time the engine updates.
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _enemys.Text = "Enemys: " + GameManager._enemyCounter;
            _lives.Text = "Lives: " + GameManager._lives;
        }

        /// <summary>
        /// Calls draw function for the enmy and lives text to be displayed on screen
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            _enemys.Draw();
            _lives.Draw();
        }
    }
}
