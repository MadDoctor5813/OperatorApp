struct EmergencySubmission
{
    public string id { get; set; }
    public string category { get; set; }
    public string details { get; set; }
}

struct ActiveEmergency
{
    public string category { get; set; }
    public string status { get; set; }
    public string response { get; set; }
}

struct GeocodedLocation
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public string street { get; set; }
    public string city { get; set; }
    public string province { get; set; }
    public string postalCode  { get; set; }
}
