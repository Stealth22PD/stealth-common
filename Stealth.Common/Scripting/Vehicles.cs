using Rage;
using Stealth.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Scripting
{
    public static class Vehicles
    {
        public static List<Vehicle> GetVehiclesNearPosition(Vector3 pPosition, float pRadius)
        {
            return GetVehiclesNearPosition(pPosition, pRadius, GetEntitiesFlags.ConsiderAllVehicles);
        }

        public static List<Vehicle> GetVehiclesNearPosition(Vector3 pPosition, float pRadius, GetEntitiesFlags pFlags)
        {
            try
            {
                List<Entity> entities = World.GetEntities(pPosition, pRadius, pFlags).ToList();
                List<Vehicle> vehList = (from x in entities where x.Exists() && x.IsVehicle() select (Vehicle)x).ToList();
                vehList = (from x in vehList where x.IsPlayersVehicle() == false select x).ToList();

                return vehList;
            }
            catch
            {
                return new List<Vehicle>();
            }
        }
    }
}
