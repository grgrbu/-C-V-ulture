using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace App7
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button buttonSignUp;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            buttonSignUp = FindViewById<Button>(Resource.Id.buttonSignUp);

            buttonSignUp.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                reg_dialogwindow reg_Dialogwindow = new reg_dialogwindow();
                reg_Dialogwindow.Show(transaction, "dialog fragment");
            };
        }

        /*private void buttonSignUp_Click(object sender, EventArgs args) =>
        {
            throw new NotImplementedException();
        }*/

        /*public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }*/
    }
}