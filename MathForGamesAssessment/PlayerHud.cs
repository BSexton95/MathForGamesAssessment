using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    class PlayerHud : Actor
    {
        private Player _player;
        private UIText _enemys;

        public PlayerHud(Player player, UIText enemys)
        {
            _player = player;
            _enemys = enemys;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _enemys.Text = "Enemys: " + GameManager._enemyCounter;
        }

        public override void Draw()
        {
            base.Draw();

            _enemys.Draw();
        }
    }
}
