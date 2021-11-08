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
    }
}