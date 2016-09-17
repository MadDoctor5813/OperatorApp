using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using Newtonsoft.Json;
using Android.Locations;
using System.Threading;
using System.Threading.Tasks;

namespace Operator.Resources
{
    class ServerHelper
    {
        private static WebClient webClient = new WebClient();
        private static readonly string ServerUrl = "operatorapp.ca";

        public static string SubmitEmergency(EmergencySubmission emergency)
        {
            string data = JsonConvert.SerializeObject(emergency);
            byte[] response = webClient.UploadData(ServerUrl + "/insertEmergencyJSON", Encoding.UTF8.GetBytes(data));
            return Encoding.UTF8.GetString(response);
        }

        public static void SubmitLocation(GeocodedLocation location, string id, Context context)
        {
            //fill in geocoding data
            Geocoder geocoder = new Geocoder(context);
            Address address = geocoder.GetFromLocation(location.Longitude, location.Latitude, 1)[0];
            location.PostalCode = address.PostalCode;
            location.Province = address.AdminArea;
            location.Street = address.Thoroughfare;
            location.City = address.Locality;
            string data = JsonConvert.SerializeObject(location);
            byte[] response = webClient.UploadData(ServerUrl + "/updateLocationJSON/" + id, Encoding.UTF8.GetBytes(data)); 
        }

        public static ActiveEmergency GetActiveEmergency(string id)
        {
            byte[] response = webClient.DownloadData(ServerUrl + "/loadEmergencyJSON/" + id);
            return JsonConvert.DeserializeObject<ActiveEmergency>(Encoding.UTF8.GetString(response));
        }

        public static void uploadImage(byte[] imageData, string filename, string[] labels)
        {
            webClient.UploadData(ServerUrl + "/uploadImage/" + filename, imageData);
        }

        public static void ShowErrorDialog(Context context, string title, string message)
        {
            new AlertDialog.Builder(context)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetPositiveButton("OK", new DialogButtonListener())
                    .Show();                   
        }

        public class DialogButtonListener : Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            public void OnClick(IDialogInterface dialog, int which)
            {
                dialog.Dismiss();
            }
        }
    }
}