using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Widget;
using CommonLibrary.Database;
using System;
using Wearable.src.Activities;

namespace Wearable.src
{
    [Service]
    public class ServiceWorkout : Service
    {
        private const String TAG = "Workout Service";

        //Data management
        private GlobalDataDash app = GlobalDataDash.GetInstance();
        private Database DB = null;

        //Data tracking
        private long startTime;
        private String str_mainTime = "";
        private String str_lapTime = "";

        //Notification instantiations
        private Handler hd_notify = new Handler();
        public const int notifyID = 0123;
        private NotificationManager nm;
        private Notification not_workout;
        private Context cxt;
        private Intent in_notif;
        private PendingIntent in_pending;
        private NotificationCompat.Builder notifyBuilder;
        private Notification notif_main;

        //Hardware instantiations
        private Vibrator vibLap;
        private Toast tst_lapRecorded;

        //Key conponents of the service
        private Context context = null;
        private IBinder iBinder;

        public class LocalBinder : Binder
        {
            ServiceWorkout serviceWorkout;

            public LocalBinder(ServiceWorkout serviceWorkout)
            {
                this.serviceWorkout = serviceWorkout;
            }

            public ServiceWorkout GetService()
            {
                return serviceWorkout;
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            return iBinder;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            context = this;
            iBinder = new LocalBinder(this);
            DB = new Database();
            vibLap = (Vibrator)GetSystemService(VibratorService);
            
        }

        public void Resume()
        {
            RecordDataPoint(RunData.RESUME);
            app.Resume(DateTime.Now.Ticks);
        }

        public void Pause()
        {
            RecordDataPoint(RunData.PAUSE);
            app.Pause();
            UnlockWake();
        }

        private PowerManager pm = null;
        private PowerManager.WakeLock wl = null;
        public void LockWake()
        {
            if (app.isRunning)
            {
                pm = (PowerManager)GetSystemService(PowerService);
                wl = pm.NewWakeLock(WakeLockFlags.Partial, "awake");
                wl.Acquire();
            }
        }

        public void UnlockWake()
        {
            if (wl != null && wl.IsHeld)
                wl.Release();
        }
       
        public RunData RecordDataPoint(int et)
        {
            RunData runData = new RunData(app.currentRecord.ID, et, DateTime.Now.Ticks, 0, 0);
            DB.Write(runData);
            app.currentLapData.Add(runData);

            if (et == RunData.MANUEL_LAP)
                app.lapSW.ResetTime();
            return runData;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            app.serviceWorkout = this;
            //cleans objects
            app.CleanData();
            app.isRunning = true;

            DateTime dateTime = DateTime.Now;
            app.currentRecord = new RunRecord(dateTime.ToString("MM/dd/yyyy"), dateTime.ToString("hh:mm tt"));
            DB.Write(app.currentRecord);
            RecordDataPoint(RunData.START);
            
            startTime = app.StartTheTime();

            app.isServiceRunning = true;

            str_mainTime = app.str_mainTime;
            str_lapTime = app.str_lapTime;
            InitNotification("");


            return StartCommandResult.Sticky; //base.OnStartCommand(intent, flags, startId);
        }

        private void InitNotification(String mainTime)
        {
            nm = (NotificationManager)GetSystemService(NotificationService);
            not_workout = new Notification();
            cxt = ApplicationContext;
            in_notif = new Intent(this, typeof(ActivityWearWorkout));
            in_pending = PendingIntent.GetActivity(cxt, 0, in_notif, PendingIntentFlags.UpdateCurrent);
            notifyBuilder = new NotificationCompat.Builder(cxt)
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetContentTitle(GetString(Resource.String.app_name))
                .SetContentText(mainTime)
                .SetContentIntent(in_pending);
            notif_main = notifyBuilder.Build();
            notif_main.Flags = NotificationFlags.NoClear | NotificationFlags.OngoingEvent;
            StartForeground(notifyID, notif_main);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnlockWake();
            app.isServiceRunning = false;
            app.isRunning = false;
            app.ResetData();

            StopUIThread();
            nm.Cancel(notifyID);
        }

        public void StartUIThread()
        {
            hd_notify.Post(TimeUpdateThread);
        }

        public void StopUIThread()
        {
            hd_notify.RemoveCallbacks(TimeUpdateThread);
        }

        public void TimeUpdateThread()
        {
            if(!app.isPaused)
            {
                str_mainTime = app.str_mainTime;
                var dummy = app.str_lapTime;
                str_lapTime = app.lapSW.GetElapsedTime();
                notifyBuilder.SetContentText(str_mainTime);
                notif_main = notifyBuilder.Build();
                nm.Notify(notifyID, notif_main);
            }
            hd_notify.PostDelayed(TimeUpdateThread, 1000);
        }

        public void RecordData(bool isEnd, int dataPoint)
        {
            if (isEnd)
            {
                RecordDataPoint(RunData.STOP);
            }
            else if (!app.isPaused)
            {
                RecordDataPoint(RunData.MANUEL_LAP);
                if (!isEnd)
                    tst_lapRecorded.Show();
                if (vibLap.HasVibrator)
                    vibLap.Vibrate(300);
            }
        }
    }

}