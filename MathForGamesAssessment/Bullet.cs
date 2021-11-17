using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _bulletDirection;

        /// <summary>
        /// How fast the bullet is going
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// The rate of speed the bullet is traveling
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Bullet Constructor
        /// </summary>
        /// <param name="position">The position of the bullet in the x and y axis</param>
        /// <param name="bulletDirection">The direction the bullet will travel</param>
        /// <param name="speed">The speed of the bullet</param>
        /// <param name="name">Bullet name</param>
        /// <param name="path">Bullet sprite</param>
        public Bullet(Vector2 position, Vector2 bulletDirection, float speed, string name = "Bullet", string path = "")
            : base(position, name, path)
        {
            _bulletDirection = bulletDirection;
            _speed = speed;
        }

        /// <summary>
        /// Updates bullet every time engine is updated
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        public override void Update(float deltaTime)
        {
            LocalPosition += _bulletDirection.Normalized * Speed * deltaTime;

            base.Update(deltaTime);
        }

        /// <summary>
        /// Function called every time engine updates
        /// </summary>
        public override void Draw()
        {
            base.Draw();
        }

        /// <summary>
        /// When a collision with a bullet occurs the enemy and the bullet are destroyed
        /// </summary>
        /// <param name="actor">The actor that collision occured with</param>
        public override void OnCollision(Actor actor)
        {
            //If bullet collides with enemy...
            if (actor is Enemy)
            {
                //...enemy and the bullet are removed from scene
                Engine.DestroyActor(actor);
                Engine.DestroyActor(this);
            }

        }
    }
}
