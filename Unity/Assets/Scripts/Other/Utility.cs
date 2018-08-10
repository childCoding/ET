using UnityEngine;

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
        static public Transform SearchChildRecurvese(Transform t, string name)
        {
            if (t.name == name)
                return t;
            int count = t.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = SearchChildRecurvese(t.GetChild(i), name);
                if (child) return child;
            }
            return null;
        }
    }
}
