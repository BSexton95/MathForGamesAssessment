using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    class PlayerHud : Actor
    {
        private UIText _enemys;
        private UIText _lives;

        public PlayerHud(UIText enemys, UIText lives)
        {
            _enemys = enemys;
            _lives = lives;
        }

        public override void Start()
        {
            base.Start();
            _enemys.Start();
            _lives.Start();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _enemys.Text = "Enemys: " + GameManager._enemyCounter;
            _lives.Text = "Lives: " + GameManager._lives;
        }

        public override void Draw()
        {
            base.Draw();

            _enemys.Draw();
            _lives.Draw();
        }
    }
}
