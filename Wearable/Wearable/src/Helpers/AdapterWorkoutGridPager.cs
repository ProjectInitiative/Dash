using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Support.Wearable.Views;

namespace Wearable.src.Helpers
{
    class AdapterWorkoutGridPager : FragmentGridPagerAdapter
    {

        private Context context;
        private List<Row> rows;

        public AdapterWorkoutGridPager(Context context, FragmentManager fm, params Fragment[] frags) : base(fm)
        {
            this.context = context;
            rows = new List<Row>();
            rows.Add(new Row(frags));
        }


        private class Row
        {
            List<Fragment> col= new List<Fragment>();

            public Row(params Fragment[] frags)
            {
                foreach(Fragment f in frags)
                    Add(f);
            }

            public void Add(Fragment frag)
            {
                col.Add(frag);
            }

            public Fragment GetColumn(int index)
            {
                return col.ElementAt(index);
            }

            public int GetColumnCount()
            {
                return col.Count;
            }
        }


        public override int RowCount
        {
            get
            {
                return rows.Count;
            }
        }

        public override int GetColumnCount(int rowNum)
        {
            return rows.ElementAt(rowNum).GetColumnCount();
        }

        public override Fragment GetFragment(int row, int col)
        {
            Row adapterRow = rows.ElementAt(row);
            return adapterRow.GetColumn(col);
        }
    }
}