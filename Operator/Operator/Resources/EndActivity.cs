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
    [Activity(Label = "EndActivity")]
    public class EndActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.EndLayout);
            FindViewById<Button>(Resource.Id.exitButton).Click += EndActivity_Click;
            
        }

        private void EndActivity_Click(object sender, EventArgs e)
        {
            FinishAffinity();
        }
    }
}