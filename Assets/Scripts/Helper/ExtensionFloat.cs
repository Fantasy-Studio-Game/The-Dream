
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public static class ExtensionFloat
    {
        public static Vector2 RadianToVector2(this float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(this float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
    }
}
