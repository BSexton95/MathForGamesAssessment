﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Raylib_cs;

namespace MathForGamesAssessment
{
    class Engine
    {
        private static bool _applicationShouldClose;
        
        //Array of scenes initalized to have zero scenes
        private Scene[] _scenes = new Scene[0];
        private static int _currentSceneIndex;

        private Stopwatch _stopWatch = new Stopwatch();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            Start();

            //The current time initalized to be 0
            float currentTime = 0;
            //Last time that was recorded initalized to be 0
            float lastTime = 0;
            //How much time has passed between one frame to the next initalized to be 0
            float deltaTime = 0;

            //Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started
                currentTime = _stopWatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);

                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            //Stop watch starts when game starts
            _stopWatch.Start();

            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Assessment");
            Raylib.SetTargetFPS(0);

            //Create an instance of a scene
            Scene scene = new Scene();


        }

        /// <summary>
        /// Called every time the game loops
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        private void Update(float deltaTime)
        {
            //Update scenes and the current scene index
            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            //Draw window
            Raylib.BeginDrawing();
            //Set background color
            Raylib.ClearBackground(Color.LIGHTGRAY);

            //Draws actors in scene
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            //Call scenes end function
            _scenes[_currentSceneIndex].End();

            //While key is abailable read key is true
            while ((Console.KeyAvailable))
                Console.ReadKey(true);
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index that the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream 
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
