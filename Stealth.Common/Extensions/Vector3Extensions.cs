using System;
using Rage;

namespace Stealth.Common.Extensions
{
    public static class Vector3Extensions
    {
        [Obsolete]
        public static Vector3 Around(this Vector3 start, float radius)
        {
            return start.AroundExt(radius);
        }

        public static Vector3 AroundExt(this Vector3 start, float radius)
        {
            // Random direction.
            Vector3 direction = Vector3Extensions.RandomXY();
            Vector3 around = start + (direction * radius);
            return around;
        }

        [Obsolete]
        public static float DistanceTo(this Vector3 start, Vector3 end)
        {
            return (end - start).Length();
        }

        public static Vector3 RandomXY()
        {
            Random random = new Random(Environment.TickCount);

            Vector3 vector3 = new Vector3();
            vector3.X = (float)(random.NextDouble() - 0.5);
            vector3.Y = (float)(random.NextDouble() - 0.5);
            vector3.Z = 0.0f;
            vector3.Normalize();
            return vector3;
        }

        public static float GetHeadingToPoint(this Vector3 pOriginPoint, Vector3 pDestinationPoint)
        {
            Vector3 mDirection = (pDestinationPoint - pOriginPoint);
            mDirection.Normalize();

            return MathHelper.NormalizeHeading(MathHelper.ConvertDirectionToHeading(mDirection));
        }
    }
}