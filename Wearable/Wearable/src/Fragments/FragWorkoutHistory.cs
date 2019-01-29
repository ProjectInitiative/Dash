using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Support.Wearable.Views;
using Wearable.src.Helpers;
using CommonLibrary.Database;

namespace Wearable.src.Fragments
{
    public class FragWorkoutHistoryList : Fragment
    {
        private String TAG = "FragWorkoutHistoryList";
        private View rootView;

        //private WearableListView workoutListView;
        private WearableRecyclerView workoutListView;
        private AdapterRunHistory adapter;

        private Database DB;
        private List<RunRecord> runRecords;

        private int location = 0;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            rootView = inflater.Inflate(Resource.Layout.frag_history_list, container, false);
            workoutListView = (WearableRecyclerView)rootView.FindViewById(Resource.Id.rv_history);

            DB = new Database();
            runRecords = DB.GetAllObjects(nameof(RunRecord)).Cast<RunRecord>().ToList();
            if (runRecords == null)
                runRecords = new List<RunRecord>();
            runRecords.Reverse();
            adapter = new AdapterRunHistory(Activity, workoutListView, Resource.Layout.item_history, runRecords, true);
            workoutListView.SetAdapter(adapter);
            workoutListView.CenterEdgeItems = true;
            workoutListView.SetLayoutManager(new CurvedChildLayoutManager(Activity));
            //workoutListView.SetLayoutManager(new CustomCurvedChildLayoutManager(Activity));
            adapter.NotifyDataSetChanged();
            return rootView;
        }

        public void OnTopEmptyRegionClick()
        {
            throw new NotImplementedException();
        }

        public override void OnResume()
        {
            base.OnResume();
            if(runRecords != null && workoutListView != null)
            {
                if (location < runRecords.Count)
                    workoutListView.ScrollToPosition(location);
                else if (location == runRecords.Count && location != 0)
                    workoutListView.ScrollToPosition(location - 1);
            }
        }

    }
}