using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary.Database
{
    [Preserve(AllMembers = true)] 
    public class RunRecord : RealmObject
    {
        [PrimaryKey]
        public string ID { get; private set; } = Database.GUID();

        public string dor { get; set; }
        public string rwStartTime { get; set; }


        public RunRecord()
        {
        }

        public RunRecord(String dor, String rwStartTime)
            :this()
        {
            this.dor = dor;
            this.rwStartTime = rwStartTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runID"></param>
        /// <returns></returns>
        public static List<RunData> GetRunDataFromRun(string runID)
        {
            return Realm.GetInstance().All<RunData>().Where(rd => rd.runID == runID).ToList(); 
        }


        public static void DeleteRunDataFromRun(string runID)
        {
            Database DB = new Database();
            foreach(RunData rd in GetRunDataFromRun(runID))
            {
                DB.Delete(rd);
            }
        }
    }
}
