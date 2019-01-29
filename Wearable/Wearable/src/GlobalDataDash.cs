using CommonLibrary.Database;
using CommonLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace Wearable.src
{
    class GlobalDataDash
    {
        public ServiceWorkout serviceWorkout { get; set; }
        public RunRecord currentRecord { get; set; }
        public List<RunData> currentLapData { get; set; }

        public Stopwatch mainSW { get; set; } = new Stopwatch();
        public Stopwatch lapSW { get; set; } = new Stopwatch();

        private String _str_mainTime;
        public String str_mainTime
        {
            get
            {
                _str_mainTime = mainSW.GetElapsedTime();
                return _str_mainTime;
            }
            set
            {
                _str_mainTime = value;
            }
        }

        private String _str_lapTime;
        public String str_lapTime
        {
            get
            {
                if (interval == INTERACTIVE_INTERVAL)
                    _str_lapTime = lapSW.GetElapsedMilliTime();
                else if (interval == AMBIENT_INTERVAL)
                    _str_lapTime = lapSW.GetElapsedTime();
                return _str_lapTime;
            }
            set
            {
                _str_lapTime = value;
            }
        }

        private long _startTime;
        public long startTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                mainSW.startTime = value;
                lapSW.startTime = value;
            }
        }

        public const int AMBIENT_INTERVAL = 1000;
        public const int INTERACTIVE_INTERVAL = 85;
        public int interval { get; set; } = INTERACTIVE_INTERVAL;

        //Boolean state values
        public bool isRunning { get; set; } = false;
        public bool isPaused { get; set; } = false;
        public bool isServiceRunning { get; set; } = false;

        private bool _isAmbient = false;
        public bool isAmbient
        {
            get { return _isAmbient; }

            set
            {
                interval = isAmbient ? AMBIENT_INTERVAL : INTERACTIVE_INTERVAL;
                _isAmbient = value;
            }
        }


        private static GlobalDataDash firstInstance = null;

        private GlobalDataDash()
        {
            CleanData();
        }

        public static GlobalDataDash GetInstance()
        {
            if (firstInstance == null)
            {
                firstInstance = new GlobalDataDash();
            }
            return firstInstance;
        }


        public long StartTheTime()
        {
            if (isRunning)
            {
                startTime = DateTime.Now.Ticks;
                mainSW.startTime = startTime;
                lapSW.startTime = startTime;
            }
            return startTime;
        }

        public void Pause()
        {
            mainSW.Pause();
            lapSW.Pause();
            isPaused = true;
        }

        public void Resume(long startTime)
        {
            mainSW.Resume(startTime);
            lapSW.Resume(startTime);
            isPaused = false;
        }

        /// <summary>
        /// Resets all data in object, including boolean state values
        /// </summary>
        public void ResetData()
        {
            firstInstance = new GlobalDataDash();
        }


        /// <summary>
        /// Resets data tracking objects without resetting boolean state values or the current RunRecord
        /// TODO: Figure out why this is necessary
        /// </summary>
        public void CleanData()
        {
            str_mainTime = "";
            str_lapTime = "";
            startTime = 0;
            if (currentLapData == null)
                currentLapData = new List<RunData>();
            else
                currentLapData.Clear();
            mainSW = new Stopwatch();
            lapSW = new Stopwatch();
        }


    }
}