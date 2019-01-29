package md5e330363feb1617ea3572a5f505ff5c89;


public class WearableBaseAdapter_1_NewViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Wearable.src.Helpers.WearableBaseAdapter`1+NewViewHolder, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WearableBaseAdapter_1_NewViewHolder.class, __md_methods);
	}


	public WearableBaseAdapter_1_NewViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == WearableBaseAdapter_1_NewViewHolder.class)
			mono.android.TypeManager.Activate ("Wearable.src.Helpers.WearableBaseAdapter`1+NewViewHolder, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
