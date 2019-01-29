using System;

using Android.App;
using Android.OS;
using Wearable.src.Fragments;
using Android.Support.Wearable.Views;
using Wearable.src.Helpers;
using Android.Util;
using Realms;
using CommonLibrary.Database;
using Android.Runtime;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using Android.Support.Wearable.View.Drawer;

namespace Wearable.src.Activities
{
    [Activity(Label = "Wearable", MainLauncher = true, /*LaunchMode = Android.Content.PM.LaunchMode.SingleInstance, */ Icon = "@drawable/ic_launcher")]
    public class ActivityWearStart : Activity
    {
        private String TAG = "ActivityWearStart";
        private GlobalDataDash app;
        private WearableRecyclerView wrv_workouts;
        private ScrollView sv_workouts;
        private WearableActionDrawer actionDrawer;
        private AdapterRunHistory adapter;

        private Database DB;
        private List<RunRecord> runRecords;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_start);

            actionDrawer = (WearableActionDrawer)FindViewById(Resource.Id.ad_start);
            actionDrawer.SetOnMenuItemClickListener(new MenuItemClickListener(this));
            actionDrawer.PeekDrawer();

            sv_workouts = (ScrollView)FindViewById(Resource.Id.sv_workout_list );
            sv_workouts.RequestFocus();
            sv_workouts.SmoothScrollingEnabled = true;

            PopulateList();

            wrv_workouts = (WearableRecyclerView)FindViewById(Resource.Id.wrv_workout_list);
            adapter = new AdapterRunHistory(this, wrv_workouts, Resource.Layout.item_history, runRecords, true);
            wrv_workouts.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

        }

        private class MenuItemClickListener : Java.Lang.Object, WearableActionDrawer.IOnMenuItemClickListener
        {
            private ActivityWearStart activity;
            public MenuItemClickListener(ActivityWearStart activity)
            {
                this.activity = activity;
            }
            

            public new void Dispose()
            {
            }

            public bool OnMenuItemClick(IMenuItem menuItem)
            {
                activity.actionDrawer.CloseDrawer();
                if(menuItem.ItemId == Resource.Id.action_start_run)
                {
                    activity.StartWorkout();
                    return true;
                }
                if(menuItem.ItemId == Resource.Id.action_delete_all)
                {
                    activity.runRecords.Clear();
                    activity.DB.DeleteAll();
                    activity.PopulateList();
                    activity.adapter.NotifyDataSetChanged();
                    activity.actionDrawer.PeekDrawer();
                    return true;
                }
                return true;
            }
        }


        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (e.RepeatCount == 0)
            {
                if (keyCode == KeyEvent.KeyCodeFromString("KEYCODE_STEM_1"))
                {
                    StartWorkout();
                    return true;
                }
            }

            return base.OnKeyDown(keyCode, e);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (adapter != null)
                adapter.NotifyDataSetChanged();
            if (actionDrawer != null)
                actionDrawer.PeekDrawer();
            if (sv_workouts != null)
                sv_workouts.RequestFocus();
        }

        public void StartButtonClicked()
        {
            StartWorkout();
            TAG = "btn_start";
            Log.Debug(TAG, "Clicked");
        }

        private void PopulateList()
        {
            DB = new Database();
            runRecords = new List<RunRecord>();
            runRecords = DB.GetAllObjects(nameof(RunRecord)).Cast<RunRecord>().ToList();
            if (runRecords == null)
                runRecords = new List<RunRecord>();
            runRecords.Reverse();
        }

        private void StartWorkout()
        {
            StartActivity(typeof(ActivityWearWorkout));
            //TaskStackBuilder.Create(this);
            Finish();
        }
    }
}