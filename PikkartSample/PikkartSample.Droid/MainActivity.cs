﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android;
using Android.Support.V7.App;
using System.Collections.Generic;
using Android.Support.V4.App;
using Com.Pikkart.AR.Recognition;
using Com.Pikkart.AR.Recognition.Data;
using Com.Pikkart.AR.Recognition.Items;

namespace PikkartSample.Droid
{
    [Activity(Label = "PikkartSample.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AppCompatActivity, IRecognitionListener
    {
        int count = 1;
        const int m_permissionCode = 1234;
        RecognitionFragment _cameraFragment;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
            {
                //you don’t have to do anything, just init your app
                InitLayout();
            }
            else
            {
                CheckPermissions(m_permissionCode);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            /*RecognitionFragment t_cameraFragment = ((RecognitionFragment)FragmentManager.FindFragmentById(Resource.Id.ar_fragment));
            if (t_cameraFragment != null) t_cameraFragment.StartRecognition(
                       new RecognitionOptions(
                               RecognitionOptions.RecognitionStorage.Local,
                               RecognitionOptions.RecognitionMode.ContinuousScan,
                               new CloudRecognitionInfo(new String[] { })
                       ), this);*/
        }

        private void InitLayout()
        {
            SetContentView(Resource.Layout.Main);

            ARView arView = new ARView(this);
            AddContentView(arView, new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent));

            _cameraFragment = FragmentManager.FindFragmentById<RecognitionFragment>(Resource.Id.ar_fragment);
            _cameraFragment.StartRecognition(new RecognitionOptions(RecognitionOptions.RecognitionStorage.Local, RecognitionOptions.RecognitionMode.ContinuousScan,
                new CloudRecognitionInfo(new string[] { })), this);
        }

        private void CheckPermissions(int code)
        {
            string[] permissions_required = new string[] {
            Manifest.Permission.Camera,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.ReadExternalStorage
                };

            List<string> permissions_not_granted_list = new List<string>();
            foreach (string permission in permissions_required)
            {
                if (ActivityCompat.CheckSelfPermission(ApplicationContext, permission) != Permission.Granted)
                {
                    permissions_not_granted_list.Add(permission);
                }
            }
            if (permissions_not_granted_list.Count > 0)
            {
                string[] permissions = new string[permissions_not_granted_list.Count];
                permissions = permissions_not_granted_list.ToArray();
                ActivityCompat.RequestPermissions(this, permissions, code);
            }
            else
            {
                InitLayout();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == m_permissionCode)
            {
                bool ok = true;
                for (int i = 0; i < grantResults.Length; ++i)
                {
                    ok = ok && (grantResults[i] == Permission.Granted);
                }
                if (ok)
                {
                    InitLayout();
                }
                else
                {
                    Toast.MakeText(this, "Error: required permissions not granted!", ToastLength.Short).Show();
                    Finish();
                }
            }
        }

        void IRecognitionListener.ARLogoFound(string p0, int p1)
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.CloudMarkerNotFound()
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.ExecutingCloudSearch()
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.InternetConnectionNeeded()
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.MarkerEngineToUpdate(string p0)
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.MarkerFound(Marker p0)
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.MarkerNotFound()
        {
            // throw new NotImplementedException();
        }

        void IRecognitionListener.MarkerTrackingLost(string p0)
        {
            // throw new NotImplementedException();
        }

        bool INetworkInfoProvider.IsConnectionAvailable(Context p0)
        {
            // throw new NotImplementedException();
            return false;
        }
    }
}


