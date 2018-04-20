using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Natives
{
    public static class Entities
    {
        public static bool IsEntityAVehicle(Entity e)
        {
            //IS_ENTITY_A_VEHICLE
            return Rage.Native.NativeFunction.Natives.x6AC7003FA6E5575E<bool>(e);
        }

        public static bool IsEntityAPed(Entity e)
        {
            //IS_ENTITY_A_PED
            return Rage.Native.NativeFunction.Natives.x524AC5ECEA15343E<bool>(e);
        }
    }
}