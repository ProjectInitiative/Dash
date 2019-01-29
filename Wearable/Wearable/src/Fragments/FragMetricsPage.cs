using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Wearable.Input;

namespace Wearable.src.Fragments
{
    public class FragMetricsPage : Fragment
    {
        private View rootView;
        private IMetricsButtonListener btnListener = null;
        private String TAG = "FragMetricsPage";

        private GlobalDataDash app;
        private String str_mainTime = "",
                       str_lapTime = "";

        private TextView mainClockLabel,
                         lapClockLabel,
                         tv_mainTime,
                         tv_lapTime;

        private LinearLayout ll_timeRecorder;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            app = GlobalDataDash.GetInstance();

            rootView = inflater.Inflate(Resource.Layout.workout_metrics, container, false);
            mainClockLabel = (TextView)rootView.FindViewById(Resource.Id.tv_main_clock_label);
            lapClockLabel = (TextView)rootView.FindViewById(Resource.Id.tv_lap_clock_label);
            tv_mainTime = (TextView)rootView.FindViewById(Resource.Id.tv_main_time);
            tv_lapTime = (TextView)rootView.FindViewById(Resource.Id.tv_lap_time);
            str_mainTime = app.str_mainTime;
            str_lapTime = app.str_lapTime;

            ll_timeRecorder = (LinearLayout)rootView.FindViewById(Resource.Id.ll_time_recorder);
            if(WearableButtons.GetButtonCount(Context) < 2)
                SetupTimeRecorder();

            return rootView;
        }

        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);

            try
            {
                btnListener = (IMetricsButtonListener)activity;
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(activity.ToString()
                        + " must implement IMetricsButtonListener");
            }
        }

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


        public interface IMetricsButtonListener
        {
            void LapButtonClicked(bool isEnd, int dataPoint);
        }

        public void SetupTimeRecorder()
        {
            ll_timeRecorder.Click += delegate
            {
                btnListener.LapButtonClicked(false, CommonLibrary.Database.RunData.MANUEL_LAP);
            };
        }
    }
}