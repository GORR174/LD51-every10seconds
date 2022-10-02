using UnityEngine;

namespace Util
{
    public static class Vector2Utils
    {
        public static Vector2 Rotate(Vector2 v, float degrees) {
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }
    }
}