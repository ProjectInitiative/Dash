using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.Wearable.Views;
using Wearable.src.Helpers;
using CommonLibrary.Database;
using Android.Util;

namespace Wearable.src.Fragments
{
    public class FragLapList : Fragment
    {
        private String TAG = "FragLapList";

        private View rootView;
        private WearableRecyclerView rv_lapList;
        private GlobalDataDash app;
        private AdapterRunData adapter;
        private List<RunData> currentLapData = null;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            rootView = inflater.Inflate(Resource.Layout.lap_list, container, false);
            app = GlobalDataDash.GetInstance();

            currentLapData = app.currentLapData;

            rv_lapList = (WearableRecyclerView)rootView.FindViewById(Resource.Id.rv_laps);
            adapter = new AdapterRunData(Activity, rv_lapList, Resource.Layout.item_lap, currentLapData, false);
            rv_lapList.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            return rootView;
        }

        public void LapRecorded()
        {
            try
            {
                adapter.LapAdded();
            }
            catch (Exception e)
            { }
        }

        public override void OnResume()
        {
            base.OnResume();
            currentLapData = app.currentLapData;
            adapter.LinkList(currentLapData);
        }
    }
}