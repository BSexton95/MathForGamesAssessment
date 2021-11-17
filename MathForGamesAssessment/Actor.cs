using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class Actor
    {
        public bool _started;
        public string _name;
        private Collider _collider;

        //Actors forward vector
        private Vector2 _forward = new Vector2(1, 0);

        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        //The actors movement
        private Matrix3 _translation = Matrix3.Identity;
        //The actors rotation
        private Matrix3 _rotation = Matrix3.Identity;
        //The actors size
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
        /// The world space to which all actors in the scene's position, rotation, and scale relate to.
        /// </summary>
        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        /// <summary>
        /// The position, rotation, scale relative to it's parent
        /// </summary>
        public Matrix3 LocalTransform
        {
            get { return _localTransform; }
            private set { _localTransform = value; }
        }


        /// <summary>
        /// The actor that will inherit a child
        /// </summary>
        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// An array of children
        /// </summary>
        public Actor[] Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Size of actor
        /// </summary>
        public Vector2 Size
        {
            get
            {
                //Size in the x
                float xScale = new Vector2(_scale.M00, _scale.M10).Magnitude;
                //Size in the y
                float yScale = new Vector2(_scale.M01, _scale.M11).Magnitude;

                return new Vector2(xScale, yScale);
            }
        }
        
        //Actors forward direction in the x direction
        public Vector2 Forward
        {
            //Gets the Xx and the Xy of Matrix3
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set
            {
                //Sets the forward to be a vector2 of the previous vector and adds it the the local position
                Vector2 point = value.Normalized + LocalPosition;
                //Call look at function for the actor to look in the direction that was set.
                LookAt(point);
            }
        }
        
        /// <summary>
        /// The collider attached to this actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        /// <summary>
        /// The sprite attached to the actor
        /// </summary>
        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        /// <summary>
        /// Actors Constructor
        /// </summary>
        /// <param name="x">Actors x position</param>
        /// <param name="y">Actors y position</param>
        /// <param name="name">Actors name</param>
        /// <param name="path">The path for the sprite</param>
        public Actor(float x, float y, string name = "Actor", string path = "") : 
            this(new Vector2 { X = x, Y = y}, name, path) { }

        public Actor(Vector2 position, string name = "Actor", string path = "")
        {
            SetTranslation(position.X, position.Y);
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public Actor() { }

        /// <summary>
        /// Updates all transforms
        /// </summary>
        public void UpdateTransforms()
        {
            //If there is a parent...
            if (Parent != null)
                //...set the global transform to be the parents globaltransform multiplyed by the local transform
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            //Otherwise...
            else
            {
                //...Set the actors global transform to be the local transform
                GlobalTransform = LocalTransform;
            }
        }

        /// <summary>
        /// Adds a child to the actor array list
        /// </summary>
        /// <param name="child">The child to be added to the array</param>
        public void AddChild(Actor child)
        {
            //Create a temporary array larger that the origianl
            Actor[] tempArray = new Actor[_children.Length + 1];

            //Copy all values from the original array into the temporary array
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            //Add the new child to the end of the new array
            tempArray[_children.Length] = child;

            //Set the parent of the actor to be this actor
            child.Parent = this;

            //Set the old array to be the new array
            _children = tempArray;
        }

        /// <summary>
        /// Removes a child from the actor array list
        /// </summary>
        /// <param name="child">The child to be removed from the array</param>
        /// <returns>True if child was successfully removed from the array</returns>
        public bool RemoveChild(Actor child)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a temporary array that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copy all values except the child we don't want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the child that the loop is on is not the one to remove...
                if (_children[i] != child)
                {
                    //...add the child into the new array and increment the temp array counter
                    tempArray[j] = _children[i];
                    j++;
                }
                //Otherwise if this child is the one to remove...
                else
                {
                    //...set childRemoved to true
                    actorRemoved = true;
                }
            }

            //If child removal was successfull...
            if (actorRemoved)
            {
                //...set the old array to be the new array
                _children = tempArray;

                //Set the parent of the child to be nothing
                child.Parent = null;
            }

            return actorRemoved;
        }

        /// <summary>
        /// When function is called, sets _started to be true.
        /// </summary>
        public virtual void Start()
        {
            _started = true;
        }

        /// <summary>
        /// Updates actors transforms, rotates actor, and displays the actors local position
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        public virtual void Update(float deltaTime)
        {
            //Sets the local transform to be the translatios multiplyed by the rotation and scale.
            _localTransform = _translation * _rotation * _scale;

            //Call function to update all transforms
            UpdateTransforms();

            
            //If the actor is not the player...
            if (_name != "Player")
                //...rotate the actor
                Rotate(deltaTime);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_B) && _name == "Planet")
                Scale(5, 5);


            //Writes actors local position on the x and y to the console screen
            Console.WriteLine(_name + ": " + LocalPosition.X + ", " + LocalPosition.Y);
        }

        /// <summary>
        /// Draws the actors sprite and collider
        /// </summary>
        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);
           
            //Collider.Draw();
        }

        public virtual void End() { }

        public virtual void OnCollision(Actor actor) { }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">The actor to check collision against</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined radii</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return false if either actor doesn't have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation
        /// </summary>
        /// <param name="radians">The angle in raidans</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        
        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position of the actor to look at</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forward);

            //If the magnitude of the dot product is greater than one...
            if (dotProd > 1)
                //...set if to be 1
                dotProd = 1;

            //Convert the angle to radians
            float angle = (float)Math.Acos(dotProd);

            //Find the perpindiculer vector to the direction
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result ins't 0, use it to change the sign of the angle to be either positibe or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
        
    }
}
