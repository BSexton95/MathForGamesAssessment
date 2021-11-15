using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _target;
        private float _maxViewAngle;
        private float _maxSightDistance;

        /// <summary>
        /// How fast the enemy moves
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// The rate of speed the enemy is traveling
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }


        /// <summary>
        /// Enemy constructor
        /// </summary>
        /// <param name="x">Enemy x position</param>
        /// <param name="y">Enemy y position</param>
        /// <param name="speed">Enemy speed</param>
        /// <param name="maxSightDistance"> Enemy max distatnce it can see</param>
        /// <param name="maxViewAngle">Enemy max angle it can view</param>
        /// <param name="target">The actor the enemy will follow</param>
        /// <param name="name">Enemy name</param>
        /// <param name="path">The enemy sprite</param>
        public Enemy(float x, float y, float speed, float maxSightDistance, float maxViewAngle, Actor target, string name, string path = "")
            : base(x, y, name, path)
        {
            _speed = speed;
            _target = target;
            _maxViewAngle = maxViewAngle;
            _maxSightDistance = maxSightDistance;
        }

        /// <summary>
        /// Calls base start function and increments the enemy counter
        /// </summary>
        public override void Start()
        {
            base.Start();
            GameManager._enemyCounter++;

        }

        /// <summary>
        /// Updates the enemys velocity
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        public override void Update(float deltaTime)
        {
            //Find distance between the enemy and its target
            Vector2 enemyDistance = (_target.LocalPosition - LocalPosition).Normalized;

            //Set enemy velocity to be the distance between the enemy and the enemys target multiplyed by 
            //it's speed and the time elapsed
            Velocity = enemyDistance * _speed * deltaTime;

            LocalPosition += Velocity;

            //If enemy has target in sight...
            if (GetTargetInSight())
            {
                //...move and turn towards target
                LocalPosition += Velocity;
                
            }
            LookAt(_target.LocalPosition);
            base.Update(deltaTime);
        }

        /// <summary>
        /// Function that returns a boolan for when actor is in enemys sights
        /// </summary>
        /// <returns> The inverse cosine of the dotproduct if less than the max view angle and
        ///           return the distance to the target if less than the max sight distance</returns>
        public bool GetTargetInSight()
        {
            //Find the distance between the enemy and its target
            Vector2 directionOfTarget = (_target.LocalPosition - LocalPosition).Normalized;

            //Find the magnitude of the distance to the target
            float distanceToTarget = Vector2.Distance(_target.LocalPosition, LocalPosition);

            //Find the dot product of the direction of the target and the enemys forward vector
            float dotProduct = Vector2.DotProduct(directionOfTarget, Forward);

            //Return The inverse cosine of the dotproduct if less than the max view angle and
            //return the distance to the target if less than the max sight distance
            return Math.Acos(dotProduct) < _maxViewAngle && distanceToTarget < _maxSightDistance;
        }

        /// <summary>
        /// Calls the base actors draw function and draws the enemys collider
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        public override void End()
        {
            GameManager._enemyCounter--;

            if (GameManager._enemyCounter == 0)
            {
                UIText winner = new UIText(300, 200, Color.BLUE, "Winner", "YOU WIN!");
                Engine.GetCurrentScene().AddUIElement(winner);
            }

        }



    }
}
