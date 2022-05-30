using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Engine.Core.Effects;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fusee.Engine.Core.Input;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform _baseTransform;

        //roter Arm - body
        private Transform _bodyTransform;

        //grüner Arm - upperArm
        private Transform _upperArmTransform;

        //blauer Arm - foreArm
        private Transform _foreArmTransform;

        //blau Erster Finger - blau
        private Transform _firstFingerTransform;

         // Zweiter Finger - gelb
        private Transform _secondFingerTransform;

         // Dritter Finger - lila
        private Transform _thirdFingerTransform;


        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Translation = new float3(0, 0, 0)
            };

            _bodyTransform = new Transform //red
            {
                Translation = new float3(0, 6, 0),
                Rotation = new float3(0, 0.2f, 0)
            };

            _upperArmTransform = new Transform //green
            {
                Translation = new float3(2, 4, 0),
                Rotation = new float3(0, 0, 0)
            };

            _foreArmTransform = new Transform //blue
            {
                Translation = new float3(2, 2, 0),
                Rotation = new float3(0, 0, 0)
            };

            _firstFingerTransform = new Transform //blue
            {
                Translation = new float3(2, 2, 0),
                Rotation = new float3(0, 0, 0)
            };

            _secondFingerTransform = new Transform //yellow
            {
                Translation = new float3(-1, 6, 0),
                Rotation = new float3(0, 0, 0)
            };

            _thirdFingerTransform = new Transform //violet
            {
                Translation = new float3(-2, 6, 1),
                Rotation = new float3(0, 0, 0)
            };


            // Setup the scene graph
             return new SceneContainer
            {
                Children = 
                {
                    new SceneNode 
                    {
                        Name = "Camera",
                        Components = 
                        {
                            new Transform
                            {
                                Translation = new float3(0, 16, -50),
                            },
                            new Camera(ProjectionMethod.Perspective, 5, 100, M.PiOver4) 
                            {
                                BackgroundColor =  (float4) ColorUint.Greenery
                            }
                        }
                    },

                    new SceneNode
                    {
                        Name = "Base (grey)",
                        Components = 
                        {
                            _baseTransform,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },
                        Children =
                        {
                            new SceneNode
                            {
                                Name = "Body (red)",
                                Components = 
                                {
                                    _bodyTransform,
                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.IndianRed),
                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                },
                                Children =
                                {
                                    new SceneNode
                                    {
                                        Name = "UpperArm (green)",
                                        Components = 
                                        {
                                            _upperArmTransform,
                                        },
                                        Children = 
                                        {
                                            new SceneNode
                                            {
                                                Components =
                                                {
                                                    new Transform { Translation = new float3(0, 4, 0)},
                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.ForestGreen),
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                },
                                                Children = {
                                                    new SceneNode
                                                    {
                                                        Name = "ForeArm (blue)",
                                                        Components =
                                                        {
                                                            _foreArmTransform,
                                                        },
                                                        Children =
                                                        {
                                                            new SceneNode
                                                            {
                                                                Components =
                                                                {
                                                                    new Transform {Translation = new float3(0, 4, 0)},
                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.CadetBlue),
                                                                    SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                                                },
                                                                //finger
                                                                Children = {
                                                                    new SceneNode
                                                                    {
                                                                        Name = "FirstFinger(blue)",
                                                                        Components =
                                                                        {
                                                                            _firstFingerTransform,
                                                                        },
                                                                        Children =
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform {Translation = new float3(-1, 6, 0)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4)ColorUint.Blue),
                                                                                    SimpleMeshes.CreateCuboid(new float3(1,6,2))
                                                                                }
                                                                            },
                                                                            new SceneNode
                                                                            {
                                                                                Name = "secondFinger",
                                                                                Components =
                                                                                {
                                                                                    _secondFingerTransform
                                                                                },
                                                                                Children =
                                                                                {
                                                                                    new SceneNode
                                                                                    {
                                                                                        Components =
                                                                                        {
                                                                                        new Transform {Translation = new float3(-1, 0, 1)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4)ColorUint.Yellow),
                                                                                        SimpleMeshes.CreateCuboid(new float3(2,6,1))
                                                                                        }
                                                                                    }
                                                                                }
                                                                            },
                                                                            new SceneNode
                                                                            {
                                                                                Name = "thirdFinger",
                                                                                Components =
                                                                                {
                                                                                    _thirdFingerTransform
                                                                                },
                                                                                Children = 
                                                                                {
                                                                                    new SceneNode
                                                                                    {
                                                                                        Components =
                                                                                        {
                                                                                        new Transform {Translation = new float3(-1, 0, -1)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4)ColorUint.Violet),
                                                                                        SimpleMeshes.CreateCuboid(new float3(1,6,2)) 
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
                    
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);


            //Bedienung
            //Pfeiltasten links/rechts dreht rot um y-Achse
            float bodyRot = _bodyTransform.Rotation.y + Keyboard.LeftRightAxis * DeltaTime * 4; 
            _bodyTransform.Rotation = new float3(0, bodyRot, 0); 

            //Pfeiltasten hoch/runter rotiert grün um x-Achse
            float upperArmRot = _upperArmTransform.Rotation.x + Keyboard.UpDownAxis * DeltaTime * 4; 
            _upperArmTransform.Rotation = new float3(upperArmRot, 0, 0);

            //Tasten W/S rotiert blau um x-Achse
            float foreArmRot = _foreArmTransform.Rotation.x - Keyboard.WSAxis * DeltaTime *2; 
            _foreArmTransform.Rotation = new float3(foreArmRot, 0, 0);



            //Greifzangen auf und zu --- habe ich leider einfach nicht hinbekommen :/

              float rotAngleFirst = _firstFingerTransform.Rotation.z;
             if (Keyboard.IsKeyDown(KeyCodes.Space)) {
                  if(rotAngleFirst <= 0) {
                      rotAngleFirst = 3*M.Pi/2;
                  } else {
                      rotAngleFirst = -M.Pi/8;
                 }          
              }
             
              _firstFingerTransform.Rotation = new float3(0, 0, rotAngleFirst); //blau
              _thirdFingerTransform.Rotation = new float3(0, 0, -rotAngleFirst); //lila
            

             float rotAngleSecond= _secondFingerTransform.Rotation.x;
             if (Keyboard.IsKeyDown(KeyCodes.Space) && _secondFingerTransform.Rotation.x <= 3f) {
                 if(rotAngleSecond <= 0) {
                     rotAngleSecond = 3*M.Pi/2;
                 } else {
                     rotAngleSecond = -M.Pi/8;
                 }              
             }
             _secondFingerTransform.Rotation = new float3(rotAngleSecond, 0, 0); //gelb


            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }
    }
}