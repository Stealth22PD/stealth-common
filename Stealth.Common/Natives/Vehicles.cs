using Rage;
using Rage.Native;
using Stealth.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Natives
{
    public static class Vehicles
    {
        public static VehicleNode GetClosestVehicleNodeWithHeading(Vector3 pPosition)
        {
            return NativeGetClosestVehicleNodeWithHeading(pPosition);
        }

        private static unsafe VehicleNode NativeGetClosestVehicleNodeWithHeading(Vector3 pPosition)
        {
            //BOOL GET_CLOSEST_VEHICLE_NODE_WITH_HEADING(float x, float y, float z,   Vector3 *outPosition, float *outHeading, int p5, float p6,   int p7) // 0xFF071FB798B803B0
            VehicleNode node = new VehicleNode();
            float heading = 0;
            Vector3 outPosition = Vector3.Zero;

            try
            {
                bool success = NativeFunction.CallByHash<bool>(0xFF071FB798B803B0, pPosition.X, pPosition.Y, pPosition.Z, new IntPtr(&outPosition), new IntPtr(&heading), 1, 0x40400000, 0);

                if (success == false)
                {
                    heading = 0;
                    outPosition = Vector3.Zero;
                    Game.LogVerboseDebug("Error getting vehicle node with heading");
                }
            }
            catch (Exception ex)
            {
                heading = 0;
                outPosition = Vector3.Zero;
                Game.LogVerboseDebug("Error getting vehicle node with heading -- " + ex.Message);
            }

            node.Position = outPosition;
            node.Heading = heading;
            return node;
        }
        
        public static VehicleColor GetVehicleColors(Vehicle v)
        {
            return NativeGetVehicleColors(v);
        }

        private static unsafe VehicleColor NativeGetVehicleColors(Vehicle v)
        {
            VehicleColor c = new VehicleColor();

            try
            {
                uint primaryColor = 0;
                uint secondaryColor = 0;

                NativeFunction.CallByHash<uint>(0xA19435F193E081AC, v, new IntPtr(&primaryColor), new IntPtr(&secondaryColor));

                c.PrimaryColor = (EPaint)primaryColor;
                c.SecondaryColor = (EPaint)secondaryColor;
            }
            catch (Exception ex)
            {
                c.PrimaryColor = EPaint.Unknown;
                c.SecondaryColor = EPaint.Unknown;
                Game.LogVerboseDebug("Error getting vehicle colors -- " + ex.Message);
            }

            return c;
        }

        public static void SetVehicleColors(Vehicle pVehicle, VehicleColor pColors)
        {
            try
            {
                int primColor = (int)pColors.PrimaryColor;
                int secColor = (int)pColors.SecondaryColor;

                NativeFunction.CallByHash<uint>(0x4F1D4BE3A7F24601, pVehicle, primColor, secColor);
            }
            catch (Exception ex)
            {
                Game.LogVerboseDebug("Error setting vehicle colors -- " + ex.Message);
            }
        }

        public enum EPaint
        {
            Unknown = -1,

            Chrome = 120,

            //Classic/Metallic: (Metallic just adds a Pearlescent Color of the same color to Secondary Color)
            Black = 0,
            Carbon_Black = 147,
            Graphite = 1,
            Anhracite_Black = 11,
            Black_Steel = 2,
            Dark_Steel = 3,
            Silver = 4,
            Bluish_Silver = 5,
            Rolled_Steel = 6,
            Shadow_Silver = 7,
            Stone_Silver = 8,
            Midnight_Silver = 9,
            Cast_Iron_Silver = 10,
            Red = 27,
            Torino_Red = 28,
            Formula_Red = 29,
            Lava_Red = 150,
            Blaze_Red = 30,
            Grace_Red = 31,
            Garnet_Red = 32,
            Sunset_Red = 33,
            Cabernet_Red = 34,
            Wine_Red = 143,
            Candy_Red = 35,
            Hot_Pink = 135,
            Pfsiter_Pink = 137,
            Salmon_Pink = 136,
            Sunrise_Orange = 36,
            Orange = 38,
            Bright_Orange = 138,
            Gold = 99,
            Bronze = 90,
            Yellow = 88,
            Race_Yellow = 89,
            Dew_Yellow = 91,
            Dark_Green = 49,
            Racing_Green = 50,
            Sea_Green = 51,
            Olive_Green = 52,
            Bright_Green = 53,
            Gasoline_Green = 54,
            Lime_Green = 92,
            Midnight_Blue = 141,
            Galaxy_Blue = 61,
            Dark_Blue = 62,
            Saxon_Blue = 63,
            Blue = 64,
            Mariner_Blue = 65,
            Harbor_Blue = 66,
            Diamond_Blue = 67,
            Surf_Blue = 68,
            Nautical_Blue = 69,
            Racing_Blue = 73,
            Ultra_Blue = 70,
            Light_Blue = 74,
            Chocolate_Brown = 96,
            Bison_Brown = 101,
            Creeen_Brown = 95,
            Feltzer_Brown = 94,
            Maple_Brown = 97,
            Beechwood_Brown = 103,
            Sienna_Brown = 104,
            Saddle_Brown = 98,
            Moss_Brown = 100,
            Woodbeech_Brown = 102,
            Straw_Brown = 99,
            Sandy_Brown = 105,
            Bleached_Brown = 106,
            Schafter_Purple = 71,
            Spinnaker_Purple = 72,
            Midnight_Purple = 142,
            Bright_Purple = 145,
            Cream = 107,
            Ice_White = 111,
            Frost_White = 112,

            //_Matte=
            Matte_Black = 12,
            Matte_Gray = 13,
            Matte_Light_Gray = 14,
            Matte_Ice_White = 131,
            Matte_Blue = 83,
            Matte_Dark_Blue = 82,
            Matte_Midnight_Blue = 84,
            Matte_Midnight_Purple = 149,
            Matte_Schafter_Purple = 148,
            Matte_Red = 39,
            Matte_Dark_Red = 40,
            Matte_Orange = 41,
            Matte_Yellow = 42,
            Matte_Lime_Green = 55,
            Matte_Green = 128,
            Matte_Forest_Green = 151,
            Matte_Foliage_Green = 155,
            Matte_Olive_Darb = 152,
            Matte_Dark_Earth = 153,
            Matte_Desert_Tan = 154,

            //Metals =
            Brushed_Steel = 117,
            Brushed_Black_Steel = 118,
            Brushed_Aluminum = 119,
            Pure_Gold = 158,
            Brushed_Gold = 159
        }

        public enum ENormalPaint
        {
            //Classic/Metallic: (Metallic just adds a Pearlescent Color of the same color to Secondary Color)
            Black = 0,
            Carbon_Black = 147,
            Graphite = 1,
            Anhracite_Black = 11,
            Black_Steel = 2,
            Dark_Steel = 3,
            Silver = 4,
            Bluish_Silver = 5,
            Rolled_Steel = 6,
            Shadow_Silver = 7,
            Stone_Silver = 8,
            Midnight_Silver = 9,
            Cast_Iron_Silver = 10,
            Red = 27,
            Torino_Red = 28,
            Formula_Red = 29,
            Lava_Red = 150,
            Blaze_Red = 30,
            Grace_Red = 31,
            Garnet_Red = 32,
            Sunset_Red = 33,
            Cabernet_Red = 34,
            Wine_Red = 143,
            Candy_Red = 35,
            Hot_Pink = 135,
            Pfsiter_Pink = 137,
            Salmon_Pink = 136,
            Sunrise_Orange = 36,
            Orange = 38,
            Bright_Orange = 138,
            Gold = 99,
            Bronze = 90,
            Yellow = 88,
            Race_Yellow = 89,
            Dew_Yellow = 91,
            Dark_Green = 49,
            Racing_Green = 50,
            Sea_Green = 51,
            Olive_Green = 52,
            Bright_Green = 53,
            Gasoline_Green = 54,
            Lime_Green = 92,
            Midnight_Blue = 141,
            Galaxy_Blue = 61,
            Dark_Blue = 62,
            Saxon_Blue = 63,
            Blue = 64,
            Mariner_Blue = 65,
            Harbor_Blue = 66,
            Diamond_Blue = 67,
            Surf_Blue = 68,
            Nautical_Blue = 69,
            Racing_Blue = 73,
            Ultra_Blue = 70,
            Light_Blue = 74,
            Chocolate_Brown = 96,
            Bison_Brown = 101,
            Creeen_Brown = 95,
            Feltzer_Brown = 94,
            Maple_Brown = 97,
            Beechwood_Brown = 103,
            Sienna_Brown = 104,
            Saddle_Brown = 98,
            Moss_Brown = 100,
            Woodbeech_Brown = 102,
            Straw_Brown = 99,
            Sandy_Brown = 105,
            Bleached_Brown = 106,
            Schafter_Purple = 71,
            Spinnaker_Purple = 72,
            Midnight_Purple = 142,
            Bright_Purple = 145,
            Cream = 107,
            Ice_White = 111,
            Frost_White = 112,

            //_Matte=
            Matte_Black = 12,
            Matte_Gray = 13,
            Matte_Light_Gray = 14,
            Matte_Ice_White = 131,
            Matte_Blue = 83,
            Matte_Dark_Blue = 82,
            Matte_Midnight_Blue = 84,
            Matte_Midnight_Purple = 149,
            Matte_Schafter_Purple = 148,
            Matte_Red = 39,
            Matte_Dark_Red = 40,
            Matte_Orange = 41,
            Matte_Yellow = 42,
            Matte_Lime_Green = 55,
            Matte_Green = 128,
            Matte_Forest_Green = 151,
            Matte_Foliage_Green = 155,
            Matte_Olive_Darb = 152,
            Matte_Dark_Earth = 153,
            Matte_Desert_Tan = 154
        }
    }
}