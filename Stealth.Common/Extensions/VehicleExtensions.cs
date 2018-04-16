using Rage;
using Stealth.Common.Natives;
using Stealth.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Extensions
{
    public static class VehicleExtensions
    {
        public static bool MustObeyPoliceRoadblocks(this Vehicle v)
        {
            bool mMustObeyPoliceRoadblocks = true;

            if (v != null && v.Exists())
            {
                if (v.IsEmergencyVehicle() | v.HasAttachedBlip())
                {
                    mMustObeyPoliceRoadblocks = false;
                }
            }

            return mMustObeyPoliceRoadblocks;
        }

        public static bool IsEmergencyVehicle(this Vehicle v)
        {
            bool mIsEmergencyVehicle = false;

            String[] excludedModels = new String[] { "POLICE" , "POLICE2", "POLICE3", "POLICE4", "SHERIFF", "SHERIFF2", "FBI", "FBI2", "POLICEB", "POLICET", "POLICEOLD1", "POLICEOLD2", "POLMAV", "AMBULANCE", "FIRETRUK", "CORSPEEDO", "CORBOXVILLE", "TOWTRUCK", "TOWTRUCK2" };

            if (v != null && v.Exists())
            {
                if (v.IsPlayersVehicle() == false)
                {
                    if (v.Model.IsCar || v.Model.IsBike || v.Model.IsQuadBike || v.Model.IsBicycle)
                    {
                        if ((v.HasSiren | v.IsSirenOn) | excludedModels.Contains(v.Model.Name))
                        {
                            mIsEmergencyVehicle = true;
                        }
                        
                    }
                }
            }

            return mIsEmergencyVehicle;
        }
        
        public static bool IsPlayersVehicle(this Vehicle v)
        {
            if (v != null && v.Exists())
            {
                if (Game.LocalPlayer.Character.IsInAnyVehicle(false) == true)
                {
                    if (Game.LocalPlayer.Character.CurrentVehicle != null && Game.LocalPlayer.Character.CurrentVehicle.Exists())
                    {
                        if (v == Game.LocalPlayer.Character.CurrentVehicle)
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
                else
                {
                    if (Game.LocalPlayer.Character.LastVehicle != null && Game.LocalPlayer.Character.LastVehicle.Exists())
                    {
                        if (v == Game.LocalPlayer.Character.LastVehicle)
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
            else
            {
                return false;
            }
        }

        public static void SetRandomColor(this Vehicle v)
        {
            if (v != null && v.Exists())
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());

                Vehicles.ENormalPaint[] paintColors = (Vehicles.ENormalPaint[])Enum.GetValues(typeof(Vehicles.ENormalPaint));
                Vehicles.ENormalPaint randColor = paintColors[rand.Next(paintColors.Length)];
                Vehicles.EPaint selectedColor = (Vehicles.EPaint)Enum.Parse(typeof(Vehicles.EPaint), randColor.ToString());

                v.SetColor(selectedColor);
            }
        }

        public static void SetColor(this Vehicle v, Vehicles.EPaint pColor)
        {
            if (v != null && v.Exists())
            {
                v.SetColor(pColor, pColor);
            }
        }

        public static void SetColor(this Vehicle v, Vehicles.EPaint primColor, Vehicles.EPaint secColor)
        {
            if (v != null && v.Exists())
            {
                VehicleColor color = new VehicleColor(primColor, secColor);
                v.SetColor(color);
            }
        }

        public static void SetColor(this Vehicle v, VehicleColor colors)
        {
            if (v != null && v.Exists())
            {
                Vehicles.SetVehicleColors(v, colors);
            }
        }
    }
}