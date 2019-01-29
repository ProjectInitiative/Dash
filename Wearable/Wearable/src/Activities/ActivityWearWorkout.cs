using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.Wearable.Activity;
using Android.Support.Wearable.Views;
using Android.Util;
using Android.Views;
using Android.Widget;
using CommonLibrary.Database;
using System;
using System.Collections.Generic;
using Wearable.src.Fragments;
using Wearable.src.Helpers;

namespace Wearable.src.Activities
{
    [Activity(LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Label = "ActivityWearWorkout")]
    public class ActivityWearWorkout : WearableActivity, FragMetricsPage.IMetricsButtonListener,
        FragWorkoutMenu.IMenuButtonListener
    {
        private String TAG = "ActivityWearWorkout";

        private Handler hd_tBox = new Handler();

        private GlobalDataDash app;
        private ServiceWorkout serviceWorkout;
        private IServiceConnection serviceConnection;
        private bool isBound = false;
        private Intent serviceIntent;

        private WearableRecyclerView wrv_multiView;
        private AdapterMetricsMultiView adapter;

        private bool stopBtnClicked = false;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.multi_recycler_view);

            wrv_multiView = (WearableRecyclerView)FindViewById(Resource.Id.wrv_multi_view);
            SnapHelper snapHelper = new LinearSnapHelper();
            snapHelper.AttachToRecyclerView(wrv_multiView);
            adapter = new AdapterMetricsMultiView(this, wrv_multiView, new List<int> { ListItemType.MetricsLayout, ListItemType.LapList }, false);
            wrv_multiView.SetAdapter(adapter);

            SetAmbientEnabled();


            app = GlobalDataDash.GetInstance();
            serviceIntent = new Intent(this, typeof(ServiceWorkout));

            serviceConnection = new ServiceConnection(this);

            if (!app.isServiceRunning)
                StartService(serviceIntent);
            DoBindService();

            StartUIThread();
        }


        private class ServiceConnection : Java.Lang.Object, IServiceConnection
        {
            private ActivityWearWorkout outer;
            public ServiceConnection(ActivityWearWorkout outer)
            {
                this.outer = outer;
            }

            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                ServiceWorkout.LocalBinder binder = (ServiceWorkout.LocalBinder)service;
                outer.serviceWorkout = binder.GetService();
                outer.isBound = true;
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                outer.isBound = false;
            }
        }

        private void DoBindService()
        {
            BindService(serviceIntent, serviceConnection, Bind.AutoCreate);
            isBound = true;
        }

        private void DoUnbindService()
        {
            if(isBound)
            {
                UnbindService(serviceConnection);
                isBound = false;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartUIThread();
            DoBindService();
            if(serviceWorkout != null)
            {
                serviceWorkout.UnlockWake();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            StopUIThread();
            DoUnbindService();
            serviceWorkout.LockWake();
            serviceWorkout.StartUIThread();
            
        }

        private void TimeUpdateThread()
        {
            try
            {
                if (!app.isPaused)
                {
                    adapter.UpdateClocks();
                }
                else if (app.isAmbient)
                {
                    StopUIThread();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Source);
            }
            hd_tBox.PostDelayed(TimeUpdateThread, app.interval);
        }

        public void StartUIThread()
        {
            hd_tBox.RemoveCallbacks(TimeUpdateThread);
            hd_tBox.Post(TimeUpdateThread);
        }

        public void StopUIThread()
        {
            hd_tBox.RemoveCallbacks(TimeUpdateThread);
        }

        public override void OnEnterAmbient(Bundle ambientDetails)
        {
            base.OnEnterAmbient(ambientDetails);
            app.isAmbient = true;
            serviceWorkout.LockWake();
        }

        public override void OnUpdateAmbient()
        {
            base.OnUpdateAmbient();
        }

        public override void OnExitAmbient()
        {
            base.OnExitAmbient();
            app.isAmbient = false;
            serviceWorkout.UnlockWake();
        }

        public void Toggle()
        {
            if (!app.isPaused)
            {
                serviceWorkout.Pause();
            }
            else
            {
                serviceWorkout.Resume();
            }
            TAG = "Pause_btn";
            Log.Debug(TAG, "Clicked");
        }

        public void Stop()
        {
            serviceWorkout.RecordData(true, RunData.STOP);
            TAG = "Stop_btn";
            StopUIThread();
            StopService(serviceIntent);
            DoUnbindService();
            stopBtnClicked = true;
            Intent i = new Intent(this, typeof(ActivityWearHistory));
            i.PutExtra("RunID", app.currentRecord.ID);
            StartActivity(i);
            Finish();
            Intent intent = new Intent(this, typeof(ConfirmationActivity));
            intent.PutExtra(ConfirmationActivity.ExtraAnimationType, ConfirmationActivity.SuccessAnimation);
            intent.PutExtra(ConfirmationActivity.ExtraMessage, GetString(Resource.String.workout_run_saved));
            StartActivity(intent);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if(e.RepeatCount == 0)
            {
                if(keyCode == KeyEvent.KeyCodeFromString("KEYCODE_STEM_1"))
                {
                    if (app.isPaused)
                        Stop();
                    else
                    {
                        //LapButtonClicked(false, RunData.MANUEL_LAP);
                        serviceWorkout.RecordData(false, RunData.MANUEL_LAP);
                        adapter.DataSetChanged();
                    }
                    return true;
                }
                else if(keyCode == KeyEvent.KeyCodeFromString("KEYCODE_STEM_2"))
                {
                    Toggle();
                    return true;
                }
            }

            return base.OnKeyDown(keyCode, e);
        }

        public void MenuButtonClicked(Button btn)
        {
            switch(btn.Id)
            {
                case Resource.Id.btn_pause:
                    {
                        String pause = GetString(Resource.String.btn_pause);
                        String resume = GetString(Resource.String.btn_resume);
                        if (btn.Text == pause)
                            btn.Text = resume;
                        else
                            btn.Text = pause;

                        Toggle();
                    }
                    break;
                case Resource.Id.btn_stop:
                    {
                        Stop();
                    }
                    break;
                default:
                    TAG = "No button";
                    Log.Debug(TAG, "Clicked");
                    break;

            }
        }

        //Maybe I don't actually need this method will have to follow its execution
        public void LapButtonClicked(bool isEnd, int dataPoint)
        {
            TAG = "Lap_btn";
            serviceWorkout.RecordData(isEnd, dataPoint);
            adapter.DataSetChanged();
        }
    }
}