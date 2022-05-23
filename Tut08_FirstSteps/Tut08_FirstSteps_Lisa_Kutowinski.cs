using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Effects;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;

        private float _cubeAngle = 0;
        private float _cubeAngle2 = 0;

        private Camera _camera;

        private Transform _cubeTransform; // _ für private Variablen
        private Transform _cubeTransform2;
        private Transform _cubeTransform3;

        private Transform _cameraTransform;






        // Init is called on startup. 
        public override void Init()
        {
            // Create a scene tree containing the camera :
            // _scene---+
            //          |
            //          +---cameraNode-----_camera

            // THE CAMERA
            // A node containing one Camera component.
            _camera = new Camera(ProjectionMethod.Perspective, 5, 100, M.PiOver4)
            {
                BackgroundColor = (float4)ColorUint.Tomato
            };

            _cameraTransform = new Transform {
                Translation = new float3(0, 0, -50)
            };

            var cameraNode = new SceneNode();
            cameraNode.Components.Add(_cameraTransform);
            cameraNode.Components.Add(_camera);




            // THE CUBE 
            // Three components: one Transform, one SurfaceEffect (blue material) and the Mesh
            _cubeTransform = new Transform
            {

                Translation = new float3(1, 1, 1), //Translation = Verschieben
                Rotation = new float3(0, 0, 2f),
                Scale = new float3(1, 1, 1)
            };

            var cubeEffect = MakeEffect.FromDiffuseSpecular((float4)ColorUint.BlanchedAlmond); //Farbe des Würfels
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(20, 10, 10)); //creates the Mesh

        

            //2. Cube
            _cubeTransform2 = new Transform
            {
                Translation = new float3(30f, 20f, 30f),
                Rotation = new float3(0, 0, 1),
                Scale = new float3(1f, 1f, 1f)
            };
            var newCubeEffect = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Violet);
            var newCubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 15, 20));

            //3. Cube
            _cubeTransform3 = new Transform
            {
                Translation = new float3(-30f, 20f, 30f),
                Rotation = new float3(0, 0, 1),
                Scale = new float3(1f, 1f, 1f)
            };
            var newCubeEffect3 = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Turquoise);
            var newCubeMesh3 = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));



            // Assemble the cube node containing the three components
            var cubeNode = new SceneNode();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeEffect);
            cubeNode.Components.Add(cubeMesh);

            var newCubeNode = new SceneNode();
            newCubeNode.Components.Add(_cubeTransform2);
            newCubeNode.Components.Add(newCubeEffect);
            newCubeNode.Components.Add(newCubeMesh);

            var newCubeNode3 = new SceneNode();
            newCubeNode3.Components.Add(_cubeTransform3);
            newCubeNode3.Components.Add(newCubeEffect3);
            newCubeNode3.Components.Add(newCubeMesh3);

            // Create the scene containing the cube as the only object
            _scene = new SceneContainer();
            _scene.Children.Add(cameraNode);
            _scene.Children.Add(cubeNode);
            _scene.Children.Add(newCubeNode);
            _scene.Children.Add(newCubeNode3);
            

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);


        }



        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            //Animate the camera angle
            _cubeAngle = _cubeAngle + 0.01f;
            _cubeAngle2 = _cubeAngle2 + 0.02f;

            //Animate the cube
            //cube 1
            _cubeTransform.Rotation = new float3(0, 0, _cubeAngle);
            _cubeTransform.Translation = new float3(30, 5 * M.Sin(3 * TimeSinceStart), 30);

            //cube 2
            _cubeTransform2.Rotation = new float3(0, _cubeAngle, 0);
            _cubeTransform2.Translation = new float3(8 * M.Sin(4*TimeSinceStart), 10, 10);

            //cube 3
            _cubeTransform3.Rotation = new float3(0, _cubeAngle2, 0);
            _cubeTransform3.Translation = new float3(-15, -5, 10 * M.Sin(2*TimeSinceStart));
            

            // Render the scene tree
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

    }
}