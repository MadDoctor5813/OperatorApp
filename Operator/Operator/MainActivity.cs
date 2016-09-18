using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Operator.Resources;

namespace Operator
{
    [Activity(Label = "Operator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var prefs = GetSharedPreferences("prefs", FileCreationMode.Private);
            if (prefs.GetString("Id", null) == null)
            {
                StartActivity(typeof(SubmitActivity));
            }
            else
            {
                Intent intent = new Intent(this, typeof(StatusActivity));
                intent.PutExtra("Id", prefs.GetString("Id", null));
                intent.PutExtra("TrackLocation", prefs.GetBoolean("TrackLocation", false));
                StartActivity(intent);
            }
        }
    }
}

