struct EmergencySubmission
{
    public string Id { get; set; }
    public string Category { get; set; }
    public string Details { get; set; }
}

struct ActiveEmergency
{
    public string Category { get; set; }
    public string Status { get; set; }
    public string Response { get; set; }
}

struct GeocodedLocation
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode  { get; set; }
}
