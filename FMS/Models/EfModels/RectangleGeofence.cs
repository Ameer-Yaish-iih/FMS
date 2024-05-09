using System;
using System.Collections.Generic;

namespace FMS.Models.EfModels;

public partial class RectangleGeofence
{
    public long Id { get; set; }

    public long? GeofenceId { get; set; }

    public float? North { get; set; }

    public float? East { get; set; }

    public float? West { get; set; }

    public float? South { get; set; }

    public virtual Geofence? Geofence { get; set; }
}
