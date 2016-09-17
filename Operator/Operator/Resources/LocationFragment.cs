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

namespace Operator.Resources
{
    public class LocationFragment : Fragment, IOnMapReadyCallback, GoogleMap.IOnMapClickListener
    {
        private SubmitActivity submitActivity;
        MapFragment mapFrag;
        GoogleMap map = null;
        LatLng selectedLoc;

        RadioGroup locationOptions;

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

        public LocationFragment(SubmitActivity submitActivity)
        {
            this.submitActivity = submitActivity;
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
            locationOptions = Activity.FindViewById<RadioGroup>(Resource.Id.LocationOptions);
            Activity.FindViewById<Button>(Resource.Id.backButton).Click += BackButton_Click;

            initMap();
        }

        private void initMap()
        {
            mapFrag = new MapFragment();
            FragmentManager.BeginTransaction().Add(Resource.Id.MapFrame, mapFrag).Commit();
            mapFrag.GetMapAsync(this);
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
    }
}