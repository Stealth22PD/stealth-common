using Stealth.Common.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Models
{
    public class VehicleColor : IVehicleColor
    {
        public VehicleColor()
        {
        }

        public VehicleColor(Vehicles.EPaint color)
        {
            this.PrimaryColor = color;
            this.SecondaryColor = color;
        }

        public VehicleColor(Vehicles.EPaint primColor, Vehicles.EPaint secColor)
        {
            this.PrimaryColor = primColor;
            this.SecondaryColor = secColor;
        }

        public Vehicles.EPaint PrimaryColor { get; set; } = Vehicles.EPaint.Graphite;
        public Vehicles.EPaint SecondaryColor { get; set; } = Vehicles.EPaint.Graphite;

        public string PrimaryColorName
        {
            get { return GetColorName(PrimaryColor); }
        }

        public string SecondaryColorName
        {
            get { return GetColorName(SecondaryColor); }
        }

        public string GetColorName(Vehicles.EPaint paint)
        {
            String name = Enum.GetName(typeof(Vehicles.EPaint), paint);
            return name.Replace("_", " ");
        }
    }
}