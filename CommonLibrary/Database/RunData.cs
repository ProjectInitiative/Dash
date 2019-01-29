using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Database
{
    [Preserve (AllMembers = true)]
    public class RunData : RealmObject
    {
        public const int START = 0;
        public const int PAUSE = 1;
        public const int RESUME = 2;
        public const int AUTO = 3;
        public const int MANUEL_LAP = 4;
        public const int GPS_LAP = 5;
        public const int STOP = 6;

        [PrimaryKey]
        public string ID { get; private set; } = Database.GUID();
        public string runID { get; set; }

        public int entryType { get; set; }
        public long time { get; set; }
        public double gpsLat { get; set; }
        public double gpsLong { get; set; }

        public RunData()
        {

        }

        public RunData(string runID)
            :this()
        {
            this.runID = runID;
        }

        public RunData(string runID, int entryType, long time, double gpsLat, double gpsLong)
            :this()
        {
            this.runID = runID;
            this.entryType = entryType;
            this.time = time;
            this.gpsLat = gpsLat;
            this.gpsLong = gpsLong;
        }



        //Pulls the raw data of a run and calculates a list elapsed time between manual laps.
        //public List<long> GetLapTimesOfRun(long runID)
        //{
        //    return GetLapTimesOfRun(GetRunDataOfRun(runID));
        //}


        public static List<long> GetLapTimesOfRun(List<RunData> listRunData)
        {
            /*for(RunData rd:listRunData)
            {
                Log.e("RD:","Time:" + rd.getTime() + "ET:" + rd.getEntryType());
            }*/
            List<long> listElapsedLaps = new List<long>();

            RunData rd;
            int et;
            long startTime = 0;
            long endTime = 0;
            long elapsedTime = 0;
            long pausedStartTime = 0;
            long resumeEndTime = 0;
            long elapsedPausedTime = 0;

            for (int i = 0; i < listRunData.Count; i++)
            {
                rd = listRunData.ElementAt(i);
                et = rd.entryType;

                if (et == START)
                {
                    startTime = rd.time;
                }
                else if (et == MANUEL_LAP || et == STOP)
                {
                    if (et == STOP)
                    {
                        if (listRunData.ElementAt(i - 1).entryType == PAUSE)
                        {
                            resumeEndTime = rd.time;
                            elapsedPausedTime = resumeEndTime - pausedStartTime;
                        }
                    }

                    endTime = rd.time;
                    elapsedTime = (endTime - startTime);
                    elapsedTime -= elapsedPausedTime;
                    listElapsedLaps.Add(elapsedTime);

                    elapsedTime = 0;
                    startTime = rd.time;
                    elapsedPausedTime = 0;
                    pausedStartTime = 0;
                    resumeEndTime = 0;
                }
                else if (et == PAUSE)
                {
                    pausedStartTime = rd.time;
                }
                else if (et == RESUME)
                {
                    resumeEndTime = rd.time;
                    elapsedPausedTime += (resumeEndTime - pausedStartTime);
                }
            }
            return listElapsedLaps;
        }

        //Pulls the raw data of a run and calculates a list of total times.
        //public List<long> GetTotalTimesOfRun(long runID)
        //{
        //    return GetTotalTimesOfRun(GetLapTimesOfRun(runID));
        //}

        public static List<long> GetTotalTimesOfRun(List<long> lapTimes)
        {

            List<long> totalTimes = new List<long>();

            long elapsedTotalTime = 0;
            for (int i = 0; i < lapTimes.Count; i++)
            {
                elapsedTotalTime += lapTimes.ElementAt(i);
                totalTimes.Add(elapsedTotalTime);
            }
            return totalTimes;
        }

        public static long GetTotalElapsedTime(List<RunData> times)
        {
            long totalTime = 0;
            List<long> totalTimes = GetLapTimesOfRun(times);
            for (int i = 0; i < totalTimes.Count; i++)
            {
                totalTime += totalTimes.ElementAt(i);
            }
            return totalTime;
        }

        public static long GetNewElapsedLapTime(List<RunData> times)
        {
            RunData runData = null;
            int et = 0;
            long finalTime = 0;
            long initialTime = 0;
            long resumeEnd = 0;
            long pauseStart = 0;
            long pausedElapsed = 0;


            for (int i = times.Count - 1; i > 1; i--)
            {
                runData = times.ElementAt(i);
                et = runData.entryType;
                if (et == MANUEL_LAP || et == STOP)
                {
                    finalTime = runData.time;
                    for (int j = i - 1; j > 0; j--)
                    {
                        runData = times.ElementAt(j);
                        et = runData.entryType;

                        if (et == RESUME)
                            resumeEnd = runData.time;
                        else if (runData.entryType == PAUSE)
                        {
                            pauseStart = runData.time;
                            pausedElapsed += (resumeEnd - pauseStart);
                        }
                        else if (et == MANUEL_LAP || et == START)
                        {
                            initialTime = times.ElementAt(j).time;
                            return (finalTime - initialTime) - (pausedElapsed);
                        }
                    }
                }
            }
            return -1;
        }
    }
}
