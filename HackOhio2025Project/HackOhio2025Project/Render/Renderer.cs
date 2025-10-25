using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HackOhio2025Project.Render {
    public class RenderEffects {

        private BasicEffect basicEffect;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;

        private List<VertexPositionColor[]> triangles;
        private List<VertexPositionColor[]> lines;

        public RenderEffects(float viewport, GraphicsDevice graphicsDevice) {
            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), Vector3.Zero, Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, viewport, 1.0f, 300.0f);

            basicEffect = new BasicEffect(graphicsDevice);

            triangles = new List<VertexPositionColor[]>();
            lines = new List<VertexPositionColor[]>();

            Initialize();
        }

        public void Initialize() {
            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;

            basicEffect.EmissiveColor = new Vector3(1.0f, 1.0f, 1.0f);

            basicEffect.VertexColorEnabled = true;

            basicEffect.EnableDefaultLighting();
        }

        public void AddTriangle(VertexPositionColor[] vertices) {
            triangles.Add(vertices);
        }

        public void AddLine(VertexPositionColor[] vertices) {
            lines.Add(vertices);
        }

        public void Draw(GraphicsDevice graphicsDevice) {

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes) {
                pass.Apply();

                foreach(var triangle in triangles) {
                    graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangle, 0, 1, VertexPositionColor.VertexDeclaration);
                }
                foreach(var line in lines) {
                    graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, line, 0, 1, VertexPositionColor.VertexDeclaration);
                }
            }

        }

    }
}