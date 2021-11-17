using System;
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
        private static Scene[] _scenes = new Scene[0];
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

            

            //Player
            Player player = new Player(1, 1, 100, "Player", "Images/player.png");
            player.SetScale(40, 40);
            player.SetTranslation(400, 200);
            player.SetRotation(1.57f);
            //Player Collider
            CircleCollider playerCircleCollider = new CircleCollider(20, player);
            player.Collider = playerCircleCollider;
            //Add player to scene
            scene.AddActor(player);

            //Enemy1
            Enemy enemy = new Enemy(1, 1, 50, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy.SetScale(40, 40);
            enemy.SetTranslation(400, 40);
            enemy.SetRotation(-1.57f);
            //Enemy1 Collider
            AABBCollider enemyBoxCollider = new AABBCollider(40, 40, enemy);
            enemy.Collider = enemyBoxCollider;
            //Add enemy to scene
            scene.AddActor(enemy);
            
            //Enemy2
            Enemy enemy2 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy2.SetScale(40, 40);
            enemy2.SetTranslation(200, 40);
            enemy2.SetRotation(-1.57f);
            //Enemy2 Collider
            AABBCollider enemy2BoxCollider = new AABBCollider(40, 40, enemy2);
            enemy2.Collider = enemy2BoxCollider;
            //Add enemy2 to scene
            scene.AddActor(enemy2);

            //Enemy3
            Enemy enemy3 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy3.SetScale(40, 40);
            enemy3.SetTranslation(1000, 40);
            //Enemy3 Collider
            AABBCollider enemy3BoxCollider = new AABBCollider(40, 40, enemy3);
            enemy3.Collider = enemy3BoxCollider;
            //Add enemy3 to scene
            scene.AddActor(enemy3);

            //Enemy4
            Enemy enemy4 = new Enemy(1, 1, 50, 50, 50, player,"Enemy", "Images/enemy.png");
            enemy4.SetScale(40, 40);
            enemy4.SetTranslation(-100, 40);
            //Enemy4 Collider
            AABBCollider enemy4BoxCollider = new AABBCollider(40, 40, enemy4);
            enemy4.Collider = enemy4BoxCollider;
            //Add enemy4 to scene
            scene.AddActor(enemy4);

            //Enemy5
            Enemy enemy5 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy5.SetScale(40, 40);
            enemy5.SetTranslation(-200, 40);
            //Enemy5 Collider
            AABBCollider enemy5BoxCollider = new AABBCollider(40, 40, enemy5);
            enemy5.Collider = enemy5BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy5);

            //Enemy6
            Enemy enemy6 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy6.SetScale(40, 40);
            enemy6.SetTranslation(-500, -700);
            //Enemy6 Collider
            AABBCollider enemy6BoxCollider = new AABBCollider(40, 40, enemy6);
            enemy6.Collider = enemy6BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy6);

            //Enemy7
            Enemy enemy7 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy7.SetScale(40, 40);
            enemy7.SetTranslation(1500, 200);
            //Enemy7 Collider
            AABBCollider enemy7BoxCollider = new AABBCollider(40, 40, enemy7);
            enemy7.Collider = enemy7BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy7);


            //Enemy8
            Enemy enemy8 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy8.SetScale(40, 40);
            enemy8.SetTranslation(1000, 1000);
            //Enemy8 Collider
            AABBCollider enemy8BoxCollider = new AABBCollider(40, 40, enemy8);
            enemy8.Collider = enemy8BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy8);

            //Enemy9
            Enemy enemy9 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy9.SetScale(40, 40);
            enemy9.SetTranslation(-100, -500);
            //Enemy9 Collider
            AABBCollider enemy9BoxCollider = new AABBCollider(40, 40, enemy9);
            enemy9.Collider = enemy9BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy9);

            //Enemy10
            Enemy enemy10 = new Enemy(1, 1, 40, 50, 50, player, "Enemy", "Images/enemy.png");
            enemy10.SetScale(40, 40);
            enemy10.SetTranslation(-100, -200);
            //Enemy10 Collider
            AABBCollider enemy10BoxCollider = new AABBCollider(40, 40, enemy10);
            enemy10.Collider = enemy10BoxCollider;
            //Add enemy5 to scene
            scene.AddActor(enemy10);

            //Sun
            Actor sun = new Actor(1, 1, "Planet", "Images/sun.png");
            sun.SetScale(50, 50);
            sun.SetTranslation(650, 100);
            //Sun Collider
            CircleCollider planetCircleCollider = new CircleCollider(10, sun);
            sun.Collider = planetCircleCollider;
            //Add planet to scene
            scene.AddActor(sun);

            //Star
            Actor star = new Actor(1, 1, "Star", "Images/asteroid.png");
            star.SetScale(0.7f, 0.7f);
            star.SetTranslation(1, 1);
            //Star Collider
            CircleCollider starCircleCollider = new CircleCollider(10, star);
            star.Collider = starCircleCollider;
            //Add star as child
            sun.AddChild(star);
            //Add star to scene
            scene.AddActor(star);

            //UIText Points
            UIText enemys = new UIText(1, 20, Color.BLUE, "Enemys", "Enemys: " + GameManager._enemyCounter);
            //UIText Lives
            UIText lives = new UIText(1, 1, Color.BLUE, "Lives", "Lives: " + GameManager._lives);

            //Player hud
            PlayerHud playerHud = new PlayerHud(enemys, lives);
            scene.AddUIElement(playerHud);

            //WASD Controls
            UIText controlsWASD = new UIText(1, 350, Color.BLUE, "Controls", "WASD moves player ship.");
            scene.AddUIElement(controlsWASD);

            //Space Control
            UIText space = new UIText(1, 375, Color.BLUE, "Space", "Space to shoot");
            scene.AddUIElement(space);

            //Scaling Example
            UIText scaleExample = new UIText(1, 400, Color.BLUE, "Scale Example", "Want a bigger planet? Press B");
            scene.AddUIElement(scaleExample);

            //UIText to tell player shift increases speed
            UIText speedUp = new UIText(1, 425, Color.BLUE, "Speed Up", "To speed up hold left shift");
            scene.AddUIElement(speedUp);

            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;

        }

        /// <summary>
        /// Called every time the game loops
        /// </summary>
        /// <param name="deltaTime">Elapsed time</param>
        private void Update(float deltaTime)
        {
            //Update scenes and the current scene index
            _scenes[_currentSceneIndex].Update(deltaTime);
            //Update UI and the current scene index
            _scenes[_currentSceneIndex].UpdateUI(deltaTime);

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

            //Draws actors in scene and UI in scene
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

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

        /// <summary>
        /// Function removes actor from current scene and calls its end function
        /// </summary>
        /// <param name="actor">Actor to be destroyed</param>
        public static void DestroyActor(Actor actor)
        {
            _scenes[_currentSceneIndex].RemoveActor(actor);
            actor.End();
        }

        /// <summary>
        /// Get the current scene
        /// </summary>
        /// <returns>returns the current scene</returns>
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }
    }
}
