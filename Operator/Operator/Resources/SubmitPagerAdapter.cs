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
using Android.Support.V13.App;

namespace Operator.Resources
{
    class SubmitPagerAdapter : FragmentStatePagerAdapter
    {
        SubmitActivity submitActivity;

        public SubmitPagerAdapter(SubmitActivity submitActivity, FragmentManager fm) : base(fm)
        {
            this.submitActivity = submitActivity;
        }

        public override int Count
        {
            get
            {
                return 2;
            }
        }

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return new TypeFragment(submitActivity);
                case 1:
                    return new LocationFragment(submitActivity);
                default:
                    return new Fragment();
            }
        }
    }
}