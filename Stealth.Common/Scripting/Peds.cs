using Rage;
using Stealth.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Scripting
{
    public static class Peds
    {
        public static List<Ped> GetPedsNearPosition(Vector3 pPosition, float pRadius)
        {
            return GetPedsNearPosition(pPosition, pRadius, GetEntitiesFlags.ConsiderAllPeds);
        }

        public static List<Ped> GetPedsNearPosition(Vector3 pPosition, float pRadius, GetEntitiesFlags pFlags)
        {
            try
            {
                List<Entity> entities = World.GetEntities(pPosition, pRadius, pFlags).ToList();
                List<Ped> pedList = (from x in entities where x.Exists() && x.IsPed() select (Ped)x).ToList();
                pedList = (from x in pedList where x.IsPlayer == false select x).ToList();

                return pedList;
            }
            catch
            {
                return new List<Ped>();
            }
        }
    }
}
