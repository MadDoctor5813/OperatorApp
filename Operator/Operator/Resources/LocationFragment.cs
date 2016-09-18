using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Net;
using Android.Provider;
using Java.IO;
using Android.Support.V4.Content;
using Android.Graphics;
using ImageEncoder;

namespace Operator.Resources
{
    public class LocationFragment : Fragment, IOnMapReadyCallback, GoogleMap.IOnMapClickListener, RadioGroup.IOnCheckedChangeListener
    {
        private SubmitActivity submitActivity;
        MapFragment mapFrag;
        GoogleMap map = null;
        LatLng selectedLoc;

        RadioGroup locationOptions;

        byte[] submittedPicture;

        public bool TrackLocation
        {
            get
            {
                return locationOptions.CheckedRadioButtonId == Resource.Id.TrackButton;
            }
        }

        public LatLng SelectedLoc
        {
            get
            {
                return selectedLoc;
            }
        }

        private Button submitButton;
        private Button photoSubmitButton;

        private TextView detailsField;

        string currentImagePath;
        string currentImageName;

        public LocationFragment(SubmitActivity submitActivity)
        {
            this.submitActivity = submitActivity;
            submitActivity.LocationFragment = this;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.LocationLayout, container, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initMap();
            locationOptions = Activity.FindViewById<RadioGroup>(Resource.Id.LocationOptions);
            locationOptions.SetOnCheckedChangeListener(this);
            Activity.FindViewById<Button>(Resource.Id.backButton).Click += BackButton_Click;
            submitButton = Activity.FindViewById<Button>(Resource.Id.submitButton);
            submitButton.Click += SubmitButton_Click;
            photoSubmitButton = Activity.FindViewById<Button>(Resource.Id.PhotoSubmit);
            photoSubmitButton.Click += PhotoSubmitButton_Click;

            detailsField = Activity.FindViewById<TextView>(Resource.Id.Details);
        }


        private void initMap()
        {
            mapFrag = new MapFragment();
            FragmentManager.BeginTransaction().Add(Resource.Id.MapFrame, mapFrag).Commit();
            mapFrag.GetMapAsync(this);           
        }

        private void PhotoSubmitButton_Click(object sender, EventArgs e)
        {
            Intent photoIntent = new Intent(MediaStore.ActionImageCapture);
            if (photoIntent.ResolveActivity(Activity.PackageManager) != null)
            {
                File imageFile;
                string time = DateTime.Now.ToString("yymmdd_HHmmss");
                string test = Android.OS.Environment.DirectoryPictures;
                imageFile = File.CreateTempFile("OPERATOR_JPEG_", time, Activity.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures));
                currentImagePath = imageFile.AbsolutePath;
                currentImageName = imageFile.Name;
                Android.Net.Uri imageUri = FileProvider.GetUriForFile(Activity, "Operator.Operator.fileprovider", imageFile);
                photoIntent.PutExtra(MediaStore.ExtraOutput, imageUri);
                StartActivityForResult(photoIntent, 1); //1 = REQUEST_IMAGE_CAPTURE
            }

        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            submittedPicture = BitmapToJpeg.ConvertToJpeg(BitmapFactory.DecodeFile(currentImagePath));
            GoogleVisionHelper.GetImageLabels(submittedPicture);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            EmergencySubmission emergency = new EmergencySubmission();
            emergency.category = submitActivity.TypeFragment.EmergencyType;
            emergency.details = detailsField.Text;
            emergency.imageName = currentImageName;
            GeocodedLocation loc = new GeocodedLocation();
            if (!TrackLocation)
            {
                loc.latitude = (float)(SelectedLoc.Latitude);
                loc.longitude = (float)(SelectedLoc.Longitude);
            }
            string id = null;
            try
            {
                id = ServerHelper.SubmitEmergency(emergency).Trim().Replace("\"", "");
                if (!TrackLocation)
                {
                    ServerHelper.SubmitLocation(loc, id, Activity);
                }
                if (submittedPicture != null)
                {
                    ServerHelper.uploadImage(submittedPicture, currentImageName, null);
                }
            }
            catch (Exception ex) when (ex is WebException || ex is Java.IO.IOException)
            {
                ServerHelper.ShowErrorDialog(Activity, "Submit Error", "Error submitting emergency. Please retry.");
                Log.Debug("HTN_APP", ex.Message);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            submitActivity.switchLayout(0);
        }


        public override void OnDestroyView()
        {
            base.OnDestroyView();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.map = googleMap;
            map.MyLocationEnabled = true;
            map.SetOnMapClickListener(this);
        }

        public void OnMapClick(LatLng point)
        {
            if (!TrackLocation)
            {
                selectedLoc = point;
                map.Clear();
                MarkerOptions opts = new MarkerOptions();
                opts.SetPosition(point);
                map.AddMarker(opts);
            }   
        }

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            if (checkedId == Resource.Id.TrackButton)
            {
                map.Clear();
            }
        }
    }
}