using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    class GameManager
    {
        public static int _enemyCounter;
        public static int _lives;

        public void Update()
        {
            if (_enemyCounter == 0)
                Engine.CloseApplication();
        }

    }
}
