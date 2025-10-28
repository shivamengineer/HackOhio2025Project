using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackOhio2025Project.ObjectHandler {
    public class Rectangles {

        private BasicEffect basicEffect;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;

        private List<VertexPositionColor[]> createdRectangles;
        private VertexPositionColor[] creationRectangle;
        private RectangleDictionary rectangleDictionary;
        private int startX;
        private int startY;

        private Color selectedColor;

        private float SCALEX = 11.42857f;
        private float SCALEY = 12f;

        public Rectangles(float viewport, GraphicsDevice graphicsDevice, RectangleDictionary rectDict, Color startColor) {
            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), Vector3.Zero, Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, viewport, 1.0f, 300.0f);

            basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;

            basicEffect.EmissiveColor = new Vector3(1.0f, 1.0f, 1.0f);

            basicEffect.VertexColorEnabled = true;

            basicEffect.EnableDefaultLighting();

            rectangleDictionary = rectDict;

            createdRectangles = new List<VertexPositionColor[]>();
            creationRectangle = new VertexPositionColor[4];
            selectedColor = startColor;
        }

        public void changeColor(Color c) {
            selectedColor = c;
        }

        private void createRectangle(int x, int y, int width, int height) {
            float newX = (x / SCALEX) - 35f;
            float newY = -(y / SCALEY) + 20;
            float newWidth = (width / SCALEX);
            float newHeight = -(height / SCALEY);
            creationRectangle[0] = new VertexPositionColor(new Vector3(newX, newY, 0), selectedColor);
            creationRectangle[1] = new VertexPositionColor(new Vector3(newX + newWidth, newY, 0), selectedColor);
            creationRectangle[2] = new VertexPositionColor(new Vector3(newX + newWidth, newY + newHeight, 0), selectedColor);
            creationRectangle[3] = new VertexPositionColor(new Vector3(newX, newY + newHeight, 0), selectedColor);
        }

        public void startCreatingRectangle(int x, int y) {
            startX = x;
            startY = y;
            createRectangle(x, y, 1, 1);
        }

        public void continueCreatingRectangle(int x, int y) {
            int rectX, rectY, width, height;
            if(x > startX) {
                rectX = startX;
                width = x - startX;
            } else {
                rectX = x;
                width = startX - x;
            }
            if(y > startY) {
                rectY = startY;
                height = y - startY;
            } else {
                rectY = y;
                height = startY - y;
            }
            createRectangle(rectX, rectY, width, height);
        }

        public void stopCreatingRectangle(int x, int y) {
            continueCreatingRectangle(x, y);
            createdRectangles.Add(creationRectangle);
        }

        public void Draw(GraphicsDevice graphicsDevice) {
            foreach(var rect in createdRectangles) {
                DrawRect(graphicsDevice, rect);
            }
            DrawRect(graphicsDevice, creationRectangle);
        }

        private void DrawRect(GraphicsDevice graphicsDevice, VertexPositionColor[] rectangle) {
            VertexPositionColor[] triangle1 = {
                rectangle[0],
                rectangle[1],
                rectangle[2]
            };
            VertexPositionColor[] triangle2 = {
                    rectangle[0],
                    rectangle[2],
                    rectangle[3]
            };
            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes) {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangle1, 0, 1, VertexPositionColor.VertexDeclaration);
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangle2, 0, 1, VertexPositionColor.VertexDeclaration);
            }

        }

    }
}
