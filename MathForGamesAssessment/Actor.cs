using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGamesAssessment
{
    class Actor
    {
        public bool _started;
        public string _name;

        //Actors forward vector
        private Vector2 _forward = new Vector2(1, 0);

        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;

        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        /// <summary>
        /// Position of actor
        /// </summary>
        public Vector2 LocalPosition
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set { SetTranslation(value.X, value.Y); }
        }

        /// <summary>
        /// The position of this actor in the world
        /// </summary>
        public Vector2 WorldPosition
        {
            //return the global transform's T column
            get { return new Vector2(_globalTransform.M02, _globalTransform.M12); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local coordiniates and translate the actor
                    float xOffSet = (value.X - Parent.WorldPosition.X) / new Vector2(_globalTransform.M00, _globalTransform.M10).Magnitude;
                    float yOffSet = (value.Y - Parent.WorldPosition.Y) / new Vector2(_globalTransform.M10, _globalTransform.M11).Magnitude;
                    SetTranslation(xOffSet, yOffSet);
                }
                //If this actor doesn't have a parent...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
    }
}
