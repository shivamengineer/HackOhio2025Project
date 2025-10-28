using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using HackOhio2025Project.Command;
using HackOhio2025Project.InputController;
using HackOhio2025Project.ObjectHandler;
using HackOhio2025Project.Render;
using System.Diagnostics;

namespace HackOhio2025Project {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<IController> controllerList;
        private VertexPositionColor[] vertices;

        private Rectangles rectangleHandler;
        private RectangleDictionary rectangleDictionary;

        private RenderEffects renderer;

        public Game1(){
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize(){
            controllerList = new List<IController>();
            rectangleDictionary = new RectangleDictionary();
            rectangleHandler = new Rectangles(GraphicsDevice.Viewport.AspectRatio, GraphicsDevice, rectangleDictionary, Color.Green);
            renderer = new RenderEffects(GraphicsDevice.Viewport.AspectRatio, GraphicsDevice);
            controllerList.Add(new MouseController(rectangleHandler));
            /*vertices = new VertexPositionColor[3];
            vertices = new VertexPositionColor[]{
                new VertexPositionColor(new Vector3(0, 0, 0), Color.Red),
                new VertexPositionColor(new Vector3(0, 20, 0), Color.Red),
                new VertexPositionColor(new Vector3(35, 0, 0), Color.Red),
            };
            renderer.AddTriangle(vertices);
            Debug.WriteLine("Width: " + GraphicsDevice.Viewport.Width);
            Debug.WriteLine("Height: " + GraphicsDevice.Viewport.Height);*/
            base.Initialize();
        }

        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var controller in controllerList) {
                controller.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);

            rectangleHandler.Draw(GraphicsDevice);
            renderer.Draw(GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
