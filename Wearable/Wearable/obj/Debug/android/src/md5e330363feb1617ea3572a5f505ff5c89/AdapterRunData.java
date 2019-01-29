package md5e330363feb1617ea3572a5f505ff5c89;


public class AdapterRunData
	extends md5e330363feb1617ea3572a5f505ff5c89.WearableBaseAdapter_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"";
		mono.android.Runtime.register ("Wearable.src.Helpers.AdapterRunData, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdapterRunData.class, __md_methods);
	}


	public AdapterRunData ()
	{
		super ();
		if (getClass () == AdapterRunData.class)
			mono.android.TypeManager.Activate ("Wearable.src.Helpers.AdapterRunData, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();

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
