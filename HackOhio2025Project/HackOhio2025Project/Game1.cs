using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using HackOhio2025Project.Command;
using HackOhio2025Project.InputController;
using HackOhio2025Project.ObjectHandler;

namespace HackOhio2025Project {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<IController> controllerList;

        private Rectangles rectangleHandler;

        public Game1(){
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize(){
            controllerList = new List<IController>();
            rectangleHandler = new Rectangles(GraphicsDevice.Viewport.AspectRatio, GraphicsDevice, Color.Green);
            controllerList.Add(new MouseController(rectangleHandler));
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

            base.Draw(gameTime);
        }
    }
}
