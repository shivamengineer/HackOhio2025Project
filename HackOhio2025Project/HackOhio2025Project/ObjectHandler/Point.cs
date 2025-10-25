using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackOhio2025Project.ObjectHandler {
    public class Point : IPoint {

        private Dictionary<int, (int, int)> keyFrames;

        public Point(int x, int y, int time) {
            keyFrames = new Dictionary<int, (int, int)>();
            keyFrames.Add(time, (x, y));
        }

        void IPoint.setPositionAtTime(int x, int y, int time) {
            if(keyFrames.ContainsKey(time)) keyFrames.Remove(time);
            keyFrames.Add(time, (x, y));
        }

        void IPoint.translatePositionAtTime(int x, int y, int time) {
            (int, int) pos;
            if(keyFrames.TryGetValue(time, out pos)) {
                keyFrames.Remove(time);
                keyFrames.Add(time, (pos.Item1 + x, pos.Item2 + y));
            }
        }

        (int, int) IPoint.getPositionAtTime(int time) {
            return (0, 0); //need to get this to work later
        }

    }
}
