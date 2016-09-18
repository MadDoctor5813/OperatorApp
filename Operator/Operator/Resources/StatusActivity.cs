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
    [Activity(Label = "StatusActivity")]
    public class StatusActivity : Activity
    {
        TextView statusText;
        TextView detailsText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StatusLayout);
            statusText = FindViewById<TextView>(Resource.Id.statusText);
            detailsText = FindViewById<TextView>(Resource.Id.detailsText);
        }

        private void setStatusDisplay(int status)
        {
            switch (status)
            {
                case 1:
                    statusText.Text = GetString(Resource.String.StatusIndicatorPending);
                    break;
                case 2:
                    statusText.Text = GetString(Resource.String.StatusIndicatorInProgress);
                    break;
                case 3:
                    statusText.Text = GetString(Resource.String.StatusIndicatorComplete);
                    break;
                case 4:
                    statusText.Text = GetString(Resource.String.StatusIndicatorArchives);
                    break;
                case 5:
                    statusText.Text = GetString(Resource.String.StatusIndicatorTrash);
                    break;
            }
        }
    }
}