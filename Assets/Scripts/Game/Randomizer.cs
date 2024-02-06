using UnityEngine;

namespace DockMe
{
    public static class Randomizer
    {
        public static Vector3 Position(int radius)
        {
            return new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
        }

        public static Quaternion Rotation(int angle)
        {
            return Quaternion.Euler(Random.Range(-angle, angle), Random.Range(-angle, angle), Random.Range(-angle, angle));
        }

        public static Color Color()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}

