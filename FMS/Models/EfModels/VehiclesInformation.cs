using System;
using System.Collections.Generic;

namespace FMS.Models.EfModels;

public partial class VehiclesInformation
{
    public long Id { get; set; }

    public long? VehicleId { get; set; }

    public long? DriverId { get; set; }

    public string? VehicleMake { get; set; }

    public string? VehicleModel { get; set; }

    public long? PurchaseDate { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
