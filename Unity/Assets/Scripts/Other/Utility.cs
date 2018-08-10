using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    static public class Utility
    {
        static public UnityEngine.Vector3 UnityVector3FromETVector3(ETModel.vector3 v)
        {
            return new UnityEngine.Vector3(v.X, v.Y, v.Z);
        }
        static public ETModel.vector3 ETVector3FromUnityVector3(UnityEngine.Vector3 V)
        {
            var v = new ETModel.vector3();
            v.X = V.x;
            v.Y = V.y;
            v.Z = V.z;
            return v;
        }
    }
}
