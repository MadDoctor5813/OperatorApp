struct EmergencySubmission
{
    public string Id { get; set; }
    public string Catagory { get; set; }
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
    float Latitude { get; set; }
    float Longitude { get; set; }
    string Street { get; set; }
    string City { get; set; }
    string Province { get; set; }
    string PostalCode  { get; set; }
}
