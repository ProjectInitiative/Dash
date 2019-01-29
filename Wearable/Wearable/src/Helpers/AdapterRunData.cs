using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Support.Wearable.Views;
using Android.Widget;
using Android.Util;
using CommonLibrary.Database;
using CommonLibrary.Helpers;
using System;

namespace Wearable.src.Helpers
{
    class AdapterRunData : WearableBaseAdapter<RunData>
    {

        Context context = null;
        Database DB = null;
        List<RunData> runData = null;
        List<long> totalTimes = null;
        List<long> lapTimes = null;
        public AdapterRunData(Context context, WearableRecyclerView listView, int layout, List<RunData> list, bool itemLongClickable) : 
        base(context, listView, layout, list, itemLongClickable)
        {
            this.context = context;
            runData = GetAllItems();
            lapTimes = RunData.GetLapTimesOfRun(runData);
            totalTimes = RunData.GetTotalTimesOfRun(lapTimes);
        }

        public void LapAdded()
        {
            lapTimes.Add(RunData.GetNewElapsedLapTime(runData));
            totalTimes.Add(RunData.GetTotalElapsedTime(runData));

            NotifyDataSetChanged();
        }

        public override void ItemClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {

        }

        public override void ItemLongClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {

        }

        public override void PopulateUI(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
            TextView tv_mainTime = (TextView)holder.ItemView.FindViewById(Resource.Id.tv_item_main);
            tv_mainTime.Text = Stopwatch.FormatTime(totalTimes.ElementAt(position));

            TextView tv_lapTime = (TextView)holder.ItemView.FindViewById(Resource.Id.tv_item_lap);
            tv_lapTime.Text = Stopwatch.FormatMilliTime(lapTimes.ElementAt(position));
        }

        public override int ItemCount => totalTimes.Count;
    }
}