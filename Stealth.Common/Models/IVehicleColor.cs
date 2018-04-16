using Stealth.Common.Natives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Models
{
    public interface IVehicleColor
    {
        string GetColorName(Vehicles.EPaint paint);

        Vehicles.EPaint PrimaryColor { get; set; }
        Vehicles.EPaint SecondaryColor { get; set; }
        string PrimaryColorName { get; }
        string SecondaryColorName { get; }
    }
}