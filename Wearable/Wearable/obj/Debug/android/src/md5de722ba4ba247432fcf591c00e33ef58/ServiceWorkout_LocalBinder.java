package md5de722ba4ba247432fcf591c00e33ef58;


public class ServiceWorkout_LocalBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Wearable.src.ServiceWorkout+LocalBinder, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ServiceWorkout_LocalBinder.class, __md_methods);
	}


	public ServiceWorkout_LocalBinder ()
	{
		super ();
		if (getClass () == ServiceWorkout_LocalBinder.class)
			mono.android.TypeManager.Activate ("Wearable.src.ServiceWorkout+LocalBinder, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ServiceWorkout_LocalBinder (md5de722ba4ba247432fcf591c00e33ef58.ServiceWorkout p0)
	{
		super ();
		if (getClass () == ServiceWorkout_LocalBinder.class)
			mono.android.TypeManager.Activate ("Wearable.src.ServiceWorkout+LocalBinder, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Wearable.src.ServiceWorkout, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
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
