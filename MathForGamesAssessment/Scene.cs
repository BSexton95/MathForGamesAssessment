﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGamesAssessment
{
    class Scene
    {
        /// <summary>
        /// Array that contains all actors in the scene
        /// </summary>
        private Actor[] _actors;

        /// <summary>
        /// Initalizes the array of actors to have 0 actors
        /// </summary>
        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Calls update for every actor in the scene.
        /// Calls start for the actor if it hasn't already been called
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        public virtual void Update(float deltaTime)
        {
            //Loop through intire actor array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If actors start has not been called...
                if (!_actors[i].Started)
                    //...call actors start function
                    _actors[i].Start();

                //Call actors update function
                _actors[i].Update(deltaTime);

                //Loop checks for a collision
                for (int j = 0; j < _actors.Length; j++)
                {
                    //If both actors local positio are the same...
                    if (_actors[i].LocalPosition == _actors[j].LocalPosition && j != 1)
                        //...a collision has occured
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }

        /// <summary>
        /// Calls draw for every actor in the array
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        /// <summary>
        /// Calls end for every actor in the array
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
        }

        /// <summary>
        /// Adds an actor to the scenes list of actors.
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public virtual void AddActor(Actor actor)
        {
            //Create a temporary array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from the original array into the temporary array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array
            _actors = tempArray;
        }

        /// <summary>
        /// Removes an actor from the scenes list of actors.
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copy all values except the actor we don't want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the actor that the loop is on is not the one to remove...
                if (_actors[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _actors[i];
                    j++;
                }
                //Otherwise if this actor is the one to remove...
                else
                {
                    //...set actorRemoved to true
                    actorRemoved = true;
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
            {
                //...set the old array to be the new array
                _actors = tempArray;
            }

            return actorRemoved;
        }
    }
}
