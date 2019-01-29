using System;

namespace CommonLibrary.Helpers
{
    public class Stopwatch
    {
        public long startTime { protected get; set; }
        public long oldTime = 0;

        private long _elapsedT;
        public long elapsedT
        {
            get
            {
                //long curTime = System.currentTimeMillis();
                long curTime = DateTime.Now.Ticks;
                _elapsedT = (oldTime + (curTime - startTime));
                hrs = (_elapsedT / (36000000000));
                remain = (_elapsedT % (36000000000));
                mins = (remain / (600000000));
                remain = (remain % (600000000));
                secs = (remain / 10000000);
                remain = (remain % 10000000);
                millis = remain;
                tenths = millis / 10000;

                return _elapsedT;
            }
            set { _elapsedT = value; }
        }
        private long millis;
        private long tenths;
        private long secs;
        private long mins;
        private long hrs;
        private long remain;

        //private long pace;
        public long pace { get; set; } = 0;
        
        private String elapsedTime;

        public void ResetTime()
        {
            //String time=getElapsedMilliTime();
            //setStartTime(System.currentTimeMillis());
            startTime = DateTime.Now.Ticks;
            oldTime = 0;
        }

        public String GetElapsedTime()
        {
            elapsedT = elapsedT;
            elapsedTime = FormatTime(_elapsedT);
            return elapsedTime;
        }

        public String GetElapsedMilliTime()
        {
            elapsedT = elapsedT;
            elapsedTime = FormatMilliTime(_elapsedT);
            return elapsedTime;
        }

        //Returns a String formatted in: H:MM:SS
        public static String FormatTime(long time)
        {
            long hrs = (time / (36000000000));
            long remain = (time % (36000000000));
            long mins = (remain / (600000000));
            remain = (remain % (600000000));
            long secs = (remain / 10000000);
            remain = (remain % 10000000);
            long millis = remain;
            long tenths = millis / 100000;
            String elapsedTime;
            if (hrs > 0)
            {
                if (mins < 10 && secs < 10)
                    elapsedTime = (hrs + ":0" + mins + ":0" + secs);
                else if (mins < 10)
                    elapsedTime = (hrs + ":0" + mins + ":" + secs);
                else if (secs < 10)
                    elapsedTime = (hrs + ":" + mins + ":0" + secs);

                else
                    elapsedTime = (hrs + ":" + mins + ":" + secs);
            }
            else
            {
                if (mins < 10 && secs < 10)
                    elapsedTime = ("0" + mins + ":0" + secs);
                else if (mins < 10)
                    elapsedTime = ("0" + mins + ":" + secs);
                else if (secs < 10)
                    elapsedTime = (mins + ":0" + secs);

                else
                    elapsedTime = (mins + ":" + secs);
            }
            return elapsedTime;
        }

        //Returns a String formatted in: H:MM:SS.ms
        public static String FormatMilliTime(long time)
        {
            long hrs = (time / (36000000000));
            long remain = (time % (36000000000));
            long mins = (remain / (600000000));
            remain = (remain % (600000000));
            long secs = (remain / 10000000);
            remain = (remain % 10000000);
            long millis = remain;
            long tenths = millis / 100000;
            String elapsedTime;
            if (hrs > 0)
            {
                if (mins < 10 && secs < 10 && tenths < 10)
                    elapsedTime = (hrs + ":0" + mins + ":0" + secs + ".0" + tenths);
                else if (mins < 10 && tenths < 10)
                    elapsedTime = (hrs + ":0" + mins + ":" + secs + ".0" + tenths);
                else if (secs < 10 && tenths < 10)
                    elapsedTime = (hrs + ":" + mins + ":0" + secs + ".0" + tenths);
                else if (mins < 10 && secs < 10)
                    elapsedTime = (hrs + ":0" + mins + ":0" + secs + "." + tenths);

                else if (mins < 10)
                    elapsedTime = (hrs + ":0" + mins + ":" + secs + "." + tenths);
                else if (secs < 10)
                    elapsedTime = (hrs + ":" + mins + ":0" + secs + "." + tenths);
                else if (tenths < 10)
                    elapsedTime = (hrs + ":" + mins + ":" + secs + ".0" + tenths);

                else
                    elapsedTime = (hrs + ":" + mins + ":" + secs + "." + tenths);
            }
            else
            {
                if (mins < 10 && secs < 10 && tenths < 10)
                    elapsedTime = ("0" + mins + ":0" + secs + ".0" + tenths);
                else if (mins < 10 && tenths < 10)
                    elapsedTime = ("0" + mins + ":" + secs + ".0" + tenths);
                else if (secs < 10 && tenths < 10)
                    elapsedTime = (mins + ":0" + secs + ".0" + tenths);
                else if (mins < 10 && secs < 10)
                    elapsedTime = ("0" + mins + ":0" + secs + "." + tenths);

                else if (mins < 10)
                    elapsedTime = ("0" + mins + ":" + secs + "." + tenths);
                else if (secs < 10)
                    elapsedTime = (mins + ":0" + secs + "." + tenths);
                else if (tenths < 10)
                    elapsedTime = (mins + ":" + secs + ".0" + tenths);

                else
                    elapsedTime = (mins + ":" + secs + "." + tenths);
            }
            return elapsedTime;
        }

        public void Pause()
        {
            oldTime = _elapsedT;
        }

        public void Resume(long newStartTime)
        {
            startTime = newStartTime;
        }
    }
}
