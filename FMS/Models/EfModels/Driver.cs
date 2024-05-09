using System;
using System.Collections.Generic;

namespace FMS.Models.EfModels;

public partial class Driver
{
    public long DriverId { get; set; }

    public string? DriverName { get; set; }

    public long? PhoneNumber { get; set; }

    public virtual ICollection<VehiclesInformation> VehiclesInformations { get; set; } = new List<VehiclesInformation>();
}
