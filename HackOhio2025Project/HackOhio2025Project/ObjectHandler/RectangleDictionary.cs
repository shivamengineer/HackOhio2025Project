using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackOhio2025Project.ObjectHandler {
    public class RectangleDictionary {

        private Dictionary<int, VertexPositionColor[]> keyframes;

        public RectangleDictionary() {
            keyframes = new Dictionary<int, VertexPositionColor[]>();
        }

        public void AddKeyframe(int time, VertexPositionColor[] rectangle) {
            keyframes.Add(time, rectangle);
        }

        private List<int> getSortedKeys() {
            List<int> keyList = new List<int>();
            var keys = keyframes.Keys;
            foreach(var key in keys) {
                keyList.Add(key);
            }
            keyList.Sort();
            return keyList;
        }

        public VertexPositionColor[] GetRectangleAtTime(int time) {
            VertexPositionColor[] rect = new VertexPositionColor[4];
            if(keyframes.ContainsKey(time)) {
                keyframes.TryGetValue(time, out rect);
                return rect;
            }
            List<int> sortedKeys = getSortedKeys();
            int index = 0;
            while(index < sortedKeys.Count - 1 && time > sortedKeys[index]) {
                index++;
            }
            
            if(index == 0 || index == sortedKeys.Count - 1) {
                keyframes.TryGetValue(sortedKeys[index], out rect);
                return rect;
            }
            VertexPositionColor[] rect1 = new VertexPositionColor[4];
            VertexPositionColor[] rect2 = new VertexPositionColor[4];
            keyframes.TryGetValue(sortedKeys[index - 1], out rect1);
            keyframes.TryGetValue(sortedKeys[index], out rect2);
            for(int i = 0; i < 4; i++) {
                rect[i] = new VertexPositionColor(new Vector3(
                    (rect2[i].Position.X - rect1[i].Position.X) / (time - sortedKeys[index - 1]),
                    (rect2[i].Position.Y - rect1[i].Position.Y) / (time - sortedKeys[index - 1]), 0), rect1[i].Color);
            }
            return rect;
        }

    }
}
