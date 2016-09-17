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

namespace Operator.Resources
{
    [Activity(Label = "SubmitActivity")]
    public class SubmitActivity : Activity
    {
        NonSwipingViewPager viewPager;
        SubmitPagerAdapter adapter;

        public TypeFragment TypeFragment { get; set; }
        public LocationFragment LocationFragment { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SubmitLayout);
            viewPager = FindViewById<NonSwipingViewPager>(Resource.Id.ViewPager);
            adapter = new SubmitPagerAdapter(this, FragmentManager);
            viewPager.Adapter = adapter;
            switchLayout(0);
        }

        public void switchLayout(int layout)
        {
            viewPager.SetCurrentItem(layout, true);
        }
    }
}