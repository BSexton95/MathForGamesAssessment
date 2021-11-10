using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    /// <summary>
    /// The two types of colliders
    /// </summary>
    enum ColliderType
    {
        CIRCLE,
        AABB
    }

    abstract class Collider
    {
        private Actor _owner;
        private ColliderType _colliderType;

        /// <summary>
        /// The actor that the collider will be attached to
        /// </summary>
        public Actor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// The type of collider that will be attached to the actor
        /// </summary>
        public ColliderType ColliderType
        {
            get { return _colliderType; }
        }

        /// <summary>
        /// Collider Constructor
        /// </summary>
        /// <param name="owner">The actor that the collider will be attached to</param>
        /// <param name="colliderType">The type of collider that will be attached to the actor</param>
        public Collider(Actor owner, ColliderType colliderType)
        {
            _owner = owner;
            _colliderType = colliderType;
        }

        /// <summary>
        /// Checks the actors collision
        /// </summary>
        /// <param name="other">The actor which collision is checked</param>
        /// <returns>True if there was a collision</returns>
        public bool CheckCollision(Actor other)
        {
            //If the actors collider type is a circle...
            if (other.Collider.ColliderType == ColliderType.CIRCLE)
                //...return the result of the check collision for circle collider
                return CheckCollisionCircle((CircleCollider)other.Collider);
            //Otherwise if the collider type is an AABB...
            else if (other.Collider.ColliderType == ColliderType.AABB)
                //...return the result of the check collision for the AABB collider
                return CheckCollisionAABB((AABBCollider)other.Collider);

            return false;
        }

        /// <summary>
        /// Checks for collision for a circle collider
        /// </summary>
        /// <param name="other">The type of collider to check collision for</param>
        /// <returns>True if collision occured</returns>
        public virtual bool CheckCollisionCircle(circleCollider other) { return false; }

        /// <summary>
        /// Check for collision for a AABB collider
        /// </summary>
        /// <param name="other">The type of collider to check collision for</param>
        /// <returns>True if collision occured</returns>
        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        /// <summary>
        /// Draws the collider
        /// </summary>
        public virtual void Draw() { }
    }
}
