using System;
using System.Linq;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.Wearable.Views;
using Wearable.src.Activities;
using CommonLibrary.Helpers;
using CommonLibrary.Database;

namespace Wearable.src.Helpers
{
    public class ListItemType
    {
        public const int MetricsLayout = 0;
        public const int ControlMenu = 1;
        public const int LapList = 2;
    }


    class AdapterMetricsMultiView : WearableBaseAdapter<int>
    {
        private ActivityWearWorkout activity;
        private GlobalDataDash app;

        public AdapterMetricsMultiView(ActivityWearWorkout activity, WearableRecyclerView listView, List<int> list, bool itemLongClickable)
            : base(activity, listView, 0, list, itemLongClickable)
        {
            this.activity = activity;
            app = GlobalDataDash.GetInstance();
        }

        public override int GetItemViewType(int position)
        {
            return GetItem(position);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = null;
            switch (viewType)
            {
                case (ListItemType.MetricsLayout):
                    {
                        try
                        {
                            v = inflater.Inflate(Resource.Layout.workout_metrics, parent, false);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                        return new NewViewHolder(v);
                    }
                case (ListItemType.ControlMenu):
                    {
                        v = inflater.Inflate(Resource.Layout.workout_menu, parent, false);
                        return new NewViewHolder(v);
                    }
                case (ListItemType.LapList):
                    {
                        v = inflater.Inflate(Resource.Layout.previous_laps, parent, false);
                        return new NewViewHolder(v);
                    }
            }
            return null;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ((NewViewHolder)holder).itemType = GetItem(position);
            PopulateUI(holder, position, 0);
        }

        public override void PopulateUI(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
            switch (((NewViewHolder)holder).itemType)
            {
                case (ListItemType.MetricsLayout):
                    {
                        tv_mainTime = (TextView)holder.ItemView.FindViewById(Resource.Id.tv_main_time);
                        tv_lapTime = (TextView)holder.ItemView.FindViewById(Resource.Id.tv_lap_time);
                        str_mainTime = app.str_mainTime;
                        str_lapTime = app.str_lapTime;
                        break;
                    }
                case (ListItemType.ControlMenu):
                    {
                        Button btn = ((Button)holder.ItemView.FindViewById(Resource.Id.btn_pause));
                        btn.Click += delegate
                        {
                            String pause = activity.GetString(Resource.String.btn_pause);
                            String resume = activity.GetString(Resource.String.btn_resume);
                            if (btn.Text == pause)
                                btn.Text = resume;
                            else
                                btn.Text = pause;

                            activity.Toggle();
                        };
                        ((Button)holder.ItemView.FindViewById(Resource.Id.btn_stop)).Click += delegate
                        {
                            activity.Stop();
                        };
                        break;
                    }
                case (ListItemType.LapList):
                    {
                        mainTimes = new TextView[] {
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_main_1),
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_main_2),
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_main_3) };

                        lapTimes = new TextView[] {
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_lap_1),
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_lap_2),
                        (TextView)holder.ItemView.FindViewById(Resource.Id.tv_lap_3) };
                        break;
                    }
            }
        }

        private TextView tv_mainTime;
        private TextView tv_lapTime;
        private string str_mainTime = "";
        private string str_lapTime = "";
        public void UpdateClocks()
        {
            try
            {
                str_mainTime = app.str_mainTime;
                str_lapTime = app.str_lapTime;
                tv_mainTime.Text = str_mainTime;
                tv_lapTime.Text = str_lapTime;
            }
            catch (Exception e)
            {
                //Log.d("thread", e.ToString());
            }
        }

        TextView[] mainTimes;
        TextView[] lapTimes;
        public void DataSetChanged()
        {
            List<long> laps = RunData.GetLapTimesOfRun(app.currentLapData);
            List<long> totals = RunData.GetTotalTimesOfRun(laps);
            //TODO: possibly dont reverse, just access the end, may speed up the execution
            laps.Reverse();
            totals.Reverse();
            try
            {
                for (int i = 0; i < mainTimes.Length; i++)
                {
                    mainTimes[i].Text = Stopwatch.FormatTime(totals.ElementAt(i));
                    lapTimes[i].Text = Stopwatch.FormatMilliTime(laps.ElementAt(i));

                    if (laps.Count == i)
                        break;
                }
            }
            catch (Exception e) { }
        }

        public override void ItemClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
        }

        public override void ItemLongClicked(RecyclerView.ViewHolder holder, int position, int centerIndex)
        {
        }

    }
}