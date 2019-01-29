
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Wearable.Views;
using Android.Support.Wearable.Activity;
using CommonLibrary.Database;

namespace Wearable.src.Activities
{
    [Activity(LaunchMode = Android.Content.PM.LaunchMode.SingleTask, Label = "ActivityWearConfirmatoin")]
    public class ActivityWearConfirmation : Activity, DelayedConfirmationView.IDelayedConfirmationListener
    {
        private DelayedConfirmationView delayedView;
        private Database DB = null;
        private string runID;
        private Bundle bundle;

        private TextView tv_action;
        private TextView tv_context;
        private bool isCanceled = false;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_delayed_confirmation);

            delayedView = (DelayedConfirmationView)FindViewById(Resource.Id.dc_timer);
            delayedView.SetListener(this);
            delayedView.SetTotalTimeMs(5000);
            delayedView.Start();

            tv_action = (TextView)FindViewById(Resource.Id.tv_action);
            tv_context = (TextView)FindViewById(Resource.Id.tv_context);

            DB = new Database();
            bundle = Intent.Extras;
            runID = bundle.GetString("deleteRun");
            string action = bundle.GetString("action");
            string context = bundle.GetString("context");

            tv_action.Text = action;
            tv_context.Text = context;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (!isCanceled)
                OnTimerSelected(null);
        }

        //User did not cancel action.
        public void OnTimerFinished(View view)
        {
            if (!isCanceled)
            {
                isCanceled = true;
                DB.Delete(runID, typeof(RunRecord));
                RunRecord.DeleteRunDataFromRun(runID);

                Intent intent = new Intent(this, typeof(ConfirmationActivity));
                intent.PutExtra(ConfirmationActivity.ExtraAnimationType, ConfirmationActivity.SuccessAnimation);
                intent.PutExtra(ConfirmationActivity.ExtraMessage, GetString(Resource.String.confirm_run_deleted));
                StartActivity(typeof(ActivityWearStart));
                Finish();
                StartActivity(intent);
            }
        }

        //User canceled action.
        public void OnTimerSelected(View view)
        {
            delayedView.Reset();
            Intent intent = new Intent(this, typeof(ConfirmationActivity));
            intent.PutExtra(ConfirmationActivity.ExtraAnimationType, ConfirmationActivity.FailureAnimation);
            intent.PutExtra(ConfirmationActivity.ExtraMessage, GetString(Resource.String.confirm_run_deletion_canceled));
            if (!isCanceled)
            {
                Intent i = new Intent(this, typeof(ActivityWearHistory));
                i.PutExtra("RunID", runID);
                StartActivity(i);
                Finish();
            }
            isCanceled = true;
            StartActivity(intent);
        }
    }
}