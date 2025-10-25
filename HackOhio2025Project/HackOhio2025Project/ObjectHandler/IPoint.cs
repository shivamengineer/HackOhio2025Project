using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackOhio2025Project.ObjectHandler {
    public interface IPoint {

        void setPositionAtTime(int x, int y, int time);

        void translatePositionAtTime(int x, int y, int time);

        (int, int) getPositionAtTime(int time);

    }
}
