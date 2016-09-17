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

namespace Operator.Resources
{
    public class LocationFragment : Fragment, IOnMapReadyCallback
    {
        private SubmitActivity submitActivity;
        MapFragment mapFrag;
        GoogleMap map = null;

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
            //init the map here later...
        }
    }
}