using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackOhio2025Project.ObjectHandler;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HackOhio2025Project.InputController {
    public class MouseController : IController {

        private MouseState lastState;

        private bool leftPressed;
        private bool rightPressed;

        private bool leftHeld;
        private bool rightHeld;

        private bool leftReleased;
        private bool rightReleased;

        private int mouseX;
        private int mouseY;

        private Rectangles rectanglesController;

        public MouseController(Rectangles rectangleHandler) {
            lastState = Mouse.GetState();
            rectanglesController = rectangleHandler;
        }

        void IController.Update() {
            updateMouseStates();
            triggerMouseEvents();
        }

        private void updateMouseStates() {
            MouseState currentMouseState = Mouse.GetState();
            leftPressed = currentMouseState.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released;
            rightPressed = currentMouseState.RightButton == ButtonState.Pressed && lastState.RightButton == ButtonState.Released;

            leftReleased = currentMouseState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed;
            rightReleased = currentMouseState.RightButton == ButtonState.Released && lastState.RightButton == ButtonState.Pressed;

            leftHeld = currentMouseState.LeftButton == ButtonState.Pressed;
            rightHeld = currentMouseState.RightButton == ButtonState.Pressed;

            mouseX = currentMouseState.X;
            mouseY = currentMouseState.Y;

            lastState = currentMouseState;
        }

        private void triggerMouseEvents() {
            if(leftPressed) onLeftMouseDown();
            if(rightPressed) onRightMouseDown();
            if(leftReleased) onLeftMouseUp();
            if(rightReleased) onRightMouseUp();
            if(leftHeld) whileLeftDown();
            if(rightHeld) whileRightDown();
        }

        private void onLeftMouseDown() {
            rectanglesController.startCreatingRectangle(mouseX, mouseY);
        }

        private void onRightMouseDown() {

        }

        private void onLeftMouseUp() {
            rectanglesController.stopCreatingRectangle(mouseX, mouseY);
        }

        private void onRightMouseUp() {

        }

        private void whileLeftDown() {
            rectanglesController.continueCreatingRectangle(mouseX, mouseY);
        }

        private void whileRightDown() {

        }
    }
}
