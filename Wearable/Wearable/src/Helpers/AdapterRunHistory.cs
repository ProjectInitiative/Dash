using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Support.Wearable.Views;
using Android.Support.V7.Widget;
using Wearable.src.Activities;
using CommonLibrary.Database;

namespace Wearable.src.Helpers
{
    class AdapterRunHistory : WearableBaseAdapter<RunRecord>
    {
        Context context = null;
        public AdapterRunHistory(Context context, WearableRecyclerView listView, int layout, List<RunRecord> list, bool itemLongClickable):
        base(context, listView, layout, list, itemLongClickable)
        {
            this.context = context;
        }

        public override void PopulateUI(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
            RunRecord currentRecord = GetItem(position);
            TextView tv_workoutName = (TextView)holder.ItemView.FindViewById(Resource.Id.tv_item_workoutName);
            tv_workoutName.Text = currentRecord.dor + " " + currentRecord.rwStartTime + " Dist.";
        }

        public override void ItemClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
            Intent i = new Intent(context, typeof(ActivityWearHistory));
            i.PutExtra("RunID", GetItem(position).ID);
            context.StartActivity(i);
        }

        public override void ItemLongClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
        }
    }
}