using Android.Views;

using Android.Support.Wearable.Views;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;

namespace Wearable.src.Helpers
{
    abstract class WearableBaseAdapter<T> : RecyclerView.Adapter
    {
        private Context context;
        protected LayoutInflater inflater;
        private List<T> list = null;
        private WearableRecyclerView listView;
        private int layout;
        private bool itemLongClickable;

        private int centerIndex = 0;

        public WearableBaseAdapter(Context context, WearableRecyclerView listView, int layout, List<T> list, bool itemLongClickable)
        {
            inflater = LayoutInflater.From(context);
            this.context = context;
            this.listView = listView;
            this.layout = layout;
            this.list = list;
            this.itemLongClickable = itemLongClickable;

            //listView.CentralPositionChanged += ListView_CentralPositionChanged;
        }

        //private void ListView_CentralPositionChanged(object sender, WearableListView.CentralPositionChangedEventArgs e)
        //{
        //    centerIndex = e.P0; 
        //}

        public T GetItem(int pos)
        {
            return list[pos] == null ? default(T) : list[pos];
        }

        public List<T> GetAllItems()
        {
            return list;
        }

        //Use this method in OnResume methods of fragments that contain live and updating lists, sometimes the ref to the list is lost.
        public void LinkList(List<T> list)
        {
            this.list = list;
        }
        

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View rootview = inflater.Inflate(layout, parent, false);
            return new NewViewHolder(rootview);
        }

        protected class NewViewHolder : RecyclerView.ViewHolder
        {
            public int itemType { set; get; }
            public NewViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PopulateUI(holder, position, centerIndex);
            holder.ItemView.Click += delegate
            {
                ItemClicked(holder, position, centerIndex);
                //This does not work for some reason
                //if (position == centerIndex)
                //{
                //    ItemClicked(holder, position, centerIndex);
                //}
                //else
                //{
                //    listView.SmoothScrollToPosition(position);
                //}
            };
            //FIXME: The long click only works on the the top item.
            //if(position == centerIndex)
            if (itemLongClickable)
            {
                holder.ItemView.LongClickable = itemLongClickable;
                holder.ItemView.LongClick += delegate
                {
                    ItemLongClicked(holder, position, centerIndex);
                };
            }
        }


        public override int ItemCount
        {
            get
            {
                return list == null ? 0 : list.Count;
            }
        }

        public abstract void PopulateUI(RecyclerView.ViewHolder holder, int position, int centerIndex);

        public abstract void ItemClicked(RecyclerView.ViewHolder holder, int position, int centerIndex);

        public abstract void ItemLongClicked(RecyclerView.ViewHolder holder, int position, int centerIndex);

    }
}