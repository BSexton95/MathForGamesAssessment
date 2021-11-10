using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;

        /// <summary>
        /// The radius to which the collider can reach
        /// </summary>
        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        /// <summary>
        /// Circle Collider Constructor
        /// </summary>
        /// <param name="collisionRadius">The radius to which the collider can reach</param>
        /// <param name="owner">The actor that the collider will be attached to</param>
        public CircleCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.CIRCLE)
        {
            _collisionRadius = collisionRadius;
        }

        /// <summary>
        /// Checks for a collision for collider type circle
        /// </summary>
        /// <param name="other">The circle collider</param>
        /// <returns>The distance greater than or equal to the combined radii</returns>
        public override bool CheckCollisionCircle(CircleCollider other)
        {
            //If the owner of the circle collider collides with itself...
            if (other.Owner == Owner)
                //...return false
                return false;

            //Find the distance between the two actors
            float distance = Vector2.Distance(other.Owner.LocalPosition, Owner.LocalPosition);
            //Find the length of the radii combined
            float combinedRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combinedRadii;
        }

        /// <summary>
        /// Draws the circle collider around actor
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Raylib.DrawCircleLines((int)Owner.LocalPosition.X, (int)Owner.LocalPosition.Y, CollisionRadius, Color.RED);
        }
    }
}
