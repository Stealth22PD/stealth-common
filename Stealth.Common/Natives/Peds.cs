using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Natives
{
    public static class Peds
    {
        public static Vector3 GetSafeCoordinatesForPed(Vector3 v3PosOnStreet)
        {
            return NativeGetSafeCoordinatesForPed(v3PosOnStreet);
        }

        private static unsafe Vector3 NativeGetSafeCoordinatesForPed(Vector3 v3PosOnStreet)
        {
            Vector3 v3Result;

            bool safeLocFound = Rage.Native.NativeFunction.CallByName<bool>("GET_SAFE_COORD_FOR_PED",
                v3PosOnStreet.X, v3PosOnStreet.Y, v3PosOnStreet.Z, true,
                &v3Result.X, &v3Result.Y, &v3Result.Z, 0);

            if (safeLocFound == true)
            {
                return v3Result;
            }
            else
            {
                return Vector3.Zero;
            }
        }

        public static void SetPedIsDrunk(Ped pPed, bool pValue)
        {
            Rage.Native.NativeFunction.CallByName<uint>("SET_PED_IS_DRUNK", pPed, pValue);
        }

        public static void AttackPed(Ped pPed, Ped pTargetPed)
        {
            Rage.Native.NativeFunction.CallByName<uint>("TASK_COMBAT_PED", pPed, pTargetPed, 0, 1);
        }

        public static void ReactAndFleePed(Ped pPed, Ped pTargetPed)
        {
            // TASK_REACT_AND_FLEE_PED
            Rage.Native.NativeFunction.CallByHash<uint>(0x72C896464915D1B1, pPed, pTargetPed);
        }

        public static void AimGunAtEntity(Ped pPed, Entity pTarget, int timeout)
        {
            // AI::TASK_AIM_GUN_AT_ENTITY
            Rage.Native.NativeFunction.CallByHash<uint>(0x9B53BB6E8943AF53, pPed, pTarget, timeout, true);
        }

        public static void AimGunAtPosition(Ped pPed, Vector3 pTarget, int time)
        {
            // AI::TASK_AIM_GUN_AT_COORD
            Rage.Native.NativeFunction.CallByHash<uint>(0x6671F3EEC681BDA1, pPed, pTarget.X, pTarget.Y, pTarget.Z, time, true, true);
        }

        public static void ShootAtPosition(Ped pPed, Vector3 pTarget)
        {
            // AI::TASK_SHOOT_AT_COORD
            Rage.Native.NativeFunction.CallByHash<uint>(0x46A6CC01E0826106, pPed, pTarget.X, pTarget.Y, pTarget.Z, 0, 1);
        }

        public static void TurnToFaceEntity(Ped pPed, Entity pTarget, int timeout)
        {
            // AI::TASK_TURN_PED_TO_FACE_ENTITY
            Rage.Native.NativeFunction.CallByHash<uint>(0x5AD23D40115353AC, pPed, pTarget, timeout, true);
        }

        public static void GoToEntityAiming(Ped pPed, Entity pTarget, float distanceToStopAt, float StartAimingDist)
        {
            // AI::TASK_GOTO_ENTITY_AIMING
            Rage.Native.NativeFunction.CallByHash<uint>(0xA9DA48FAB8A76C12, pPed, pTarget, distanceToStopAt, StartAimingDist);
        }

        public static void FollowEntityInVehicle(Ped pDriver, Vehicle pVehicle, Entity pTarget, int pDrivingStyle, float pSpeed, float pMinDistance)
        {
            Rage.Native.NativeFunction.CallByHash<uint>(0xFC545A9F0626E3B6, pDriver, pVehicle, pTarget, pDrivingStyle, pSpeed, pMinDistance);
        }

        public static void EscortVehicle(Ped pDriver, Vehicle pVehicle, Vehicle pTarget, int p3, float pSpeed, int pDrivingStyle, float pMinDistance, int p7, float pNoRoadsDistance)
        {
            Rage.Native.NativeFunction.CallByHash<uint>(0x0FA6E4B75F302400, pDriver, pVehicle, pTarget, p3, pSpeed, pDrivingStyle, pMinDistance, p7, pNoRoadsDistance);
        }

        public static void ChaseEntityInVehicle(Ped pDriver, Entity pTarget)
        {
            Rage.Native.NativeFunction.CallByHash<uint>(0x3C08A8E30363B353, pDriver, pTarget);
        }
    }
}