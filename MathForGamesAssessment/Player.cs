using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        /// <summary>
        /// How fast the player is moving
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// The players rate of speed
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="x">Players x position</param>
        /// <param name="y">Players y position</param>
        /// <param name="speed">Players speed</param>
        /// <param name="name">Players name</param>
        /// <param name="path">PLayers sprite</param>
        public Player(float x, float y, float speed, string name = "Player", string path = "")
            : base (x, y, name, path)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                             + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                             + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //If player presses and holds shift speed will increase
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                _speed = 150;
            else
                _speed = 100;

           



            //If player pressed the spacebar...
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                //...bullet spawns
                Bullet bullet = new Bullet(LocalPosition, Forward, 150, "Bullet", "Images/bullet.png");
                bullet.SetScale(50, 50);
                CircleCollider bulletCricleCollider = new CircleCollider(10, bullet);
                bullet.Collider = bulletCricleCollider;

                //Add bullet to scene
                Engine.GetCurrentScene().AddActor(bullet);
            }

            //Create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            //Set the veloctiy to the direction the player is moving multiplyed by the speed and elapsed time
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Translate(Velocity.X, Velocity.Y);

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor actor)
        {
            //If player collides with enemy...
            if(actor is Enemy)
            {
                //...player respawns
                LocalPosition = new Vector2(20, 20);
            }
        }
    }
}
