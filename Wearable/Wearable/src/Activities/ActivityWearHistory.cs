using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Wearable.Views;
using Wearable.src.Helpers;
using Android.Util;
using CommonLibrary.Database;
using System;
using Android.Widget;
using Android.Support.Wearable.View.Drawer;
using Android.Views;

namespace Wearable.src.Activities
{
    [Activity(/*LaunchMode = Android.Content.PM.LaunchMode.SingleInstance,*/ Label = "ActivityWearHistory")]
    public class ActivityWearHistory : Activity
    {
        private WearableActionDrawer actionDrawer;
        private ScrollView sv_workout;
        private TextView tv_runDate;
        private WearableRecyclerView wrv_laps;
        private AdapterRunData adapter;

        private Database DB = null;
        private RunRecord runRecord = null;
        private List<RunData> runData = null;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_workout_history);

            actionDrawer = (WearableActionDrawer)FindViewById(Resource.Id.ad_history);
            actionDrawer.SetOnMenuItemClickListener(new MenuItemClickListener(this));
            actionDrawer.PeekDrawer();

            DB = new Database();

            runRecord = (RunRecord)DB.Find(Intent.Extras.GetString("RunID"), typeof(RunRecord));
            runData = RunRecord.GetRunDataFromRun(runRecord.ID);

            sv_workout = (ScrollView)FindViewById(Resource.Id.sv_workout);
            sv_workout.RequestFocus();
            sv_workout.SmoothScrollingEnabled = true;



            tv_runDate = (TextView)FindViewById(Resource.Id.tv_run_date);
            tv_runDate.Text = runRecord.dor;

            wrv_laps = (WearableRecyclerView)FindViewById(Resource.Id.wrv_workout_data);
            adapter = new AdapterRunData(this, wrv_laps, Resource.Layout.item_lap, runData, false);
            wrv_laps.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            Console.WriteLine("Lap count" + runData.Count);

        }

        private class MenuItemClickListener : Java.Lang.Object, WearableActionDrawer.IOnMenuItemClickListener
        {
            private ActivityWearHistory activity;
            public MenuItemClickListener(ActivityWearHistory activity)
            {
                this.activity = activity;
            }


            public new void Dispose()
            {
            }

            public bool OnMenuItemClick(IMenuItem menuItem)
            {
                activity.actionDrawer.CloseDrawer();
                
                switch (menuItem.ItemId)
                {
                    case (Resource.Id.action_start_run):
                        {
                            activity.StartActivity(typeof(ActivityWearWorkout));
                            return true;
                        }
                    case (Resource.Id.action_delete_run):
                        {
                            Intent deleteConfirm = new Intent(activity, typeof(ActivityWearConfirmation));
                            deleteConfirm.PutExtra("deleteRun", activity.runRecord.ID);
                            deleteConfirm.PutExtra("action", activity.GetString(Resource.String.action_deleting_run));
                            deleteConfirm.PutExtra("context", activity.runRecord.dor);
                            activity.StartActivity(deleteConfirm);
                            activity.Finish();
                            return true;
                        }
                    case (Resource.Id.action_open_history):
                        {
                            activity.StartActivity(typeof(ActivityWearStart));
                            activity.Finish();
                            return true;
                        }
                }
                return false;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(actionDrawer != null)
                actionDrawer.PeekDrawer();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //StartActivity(typeof(ActivityWearStart));
            Finish();
        }
    }
}