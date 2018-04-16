using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsVehicle(this Entity e)
        {
            return Natives.Entities.IsEntityAVehicle(e);
        }

        public static bool IsPed(this Entity e)
        {
            return Natives.Entities.IsEntityAPed(e);
        }

        public static bool HasAttachedBlip(this Entity e)
        {
            if (e != null && e.Exists())
            {
                Blip b = e.GetAttachedBlip();
                if (b != null && b.Exists())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}