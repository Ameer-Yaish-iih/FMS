using System;
using System.Collections.Generic;

namespace FMS.Models.EfModels;

public partial class Geofence
{
    public long GeofenceId { get; set; }

    public string? GeofenceType { get; set; }

    public long? AddedDate { get; set; }

    public string? StrockColor { get; set; }

    public float? StrockOpacity { get; set; }

    public float? StrockWeight { get; set; }

    public string? FillColor { get; set; }

    public float? FillOpacity { get; set; }

    public virtual ICollection<CircleGeofence> CircleGeofences { get; set; } = new List<CircleGeofence>();

    public virtual ICollection<PolygonGeofence> PolygonGeofences { get; set; } = new List<PolygonGeofence>();

    public virtual ICollection<RectangleGeofence> RectangleGeofences { get; set; } = new List<RectangleGeofence>();
}
