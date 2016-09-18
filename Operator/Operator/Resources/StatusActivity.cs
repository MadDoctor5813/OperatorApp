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
using Android.Text;

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
            setStatusDisplay(1);
        }
        
        private void setStatusDisplay(int status)
        {
            switch (status)
            {
                case 1:
                    statusText.SetText(Html.FromHtml("<font color='#F24333'>⬤</font> Pending"), TextView.BufferType.Spannable);
                    break;
                case 2:
                    statusText.SetText(Html.FromHtml("<font color='#FBFE4F'>⬤</font> In Progress"), TextView.BufferType.Spannable);
                    break;
                case 3:
                    statusText.SetText(Html.FromHtml("<font color='#4ACE82'>⬤</font> Complete"), TextView.BufferType.Spannable);
                    break;
                case 4:
                    statusText.SetText(Html.FromHtml("<font color='#78C0E0'>⬤</font> Archived"), TextView.BufferType.Spannable);
                    break;
                case 5:
                    statusText.SetText(Html.FromHtml("<font color='#8796BA'>⬤</font> Trash"), TextView.BufferType.Spannable);
                    break;
            }
        }
    }
}