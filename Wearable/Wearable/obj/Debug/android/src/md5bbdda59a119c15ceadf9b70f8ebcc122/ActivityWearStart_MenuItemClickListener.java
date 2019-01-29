package md5bbdda59a119c15ceadf9b70f8ebcc122;


public class ActivityWearStart_MenuItemClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.support.wearable.view.drawer.WearableActionDrawer.OnMenuItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMenuItemClick:(Landroid/view/MenuItem;)Z:GetOnMenuItemClick_Landroid_view_MenuItem_Handler:Android.Support.Wearable.View.Drawer.WearableActionDrawer/IOnMenuItemClickListenerInvoker, Xamarin.Android.Wear\n" +
			"";
		mono.android.Runtime.register ("Wearable.src.Activities.ActivityWearStart+MenuItemClickListener, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivityWearStart_MenuItemClickListener.class, __md_methods);
	}


	public ActivityWearStart_MenuItemClickListener ()
	{
		super ();
		if (getClass () == ActivityWearStart_MenuItemClickListener.class)
			mono.android.TypeManager.Activate ("Wearable.src.Activities.ActivityWearStart+MenuItemClickListener, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ActivityWearStart_MenuItemClickListener (md5bbdda59a119c15ceadf9b70f8ebcc122.ActivityWearStart p0)
	{
		super ();
		if (getClass () == ActivityWearStart_MenuItemClickListener.class)
			mono.android.TypeManager.Activate ("Wearable.src.Activities.ActivityWearStart+MenuItemClickListener, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Wearable.src.Activities.ActivityWearStart, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public boolean onMenuItemClick (android.view.MenuItem p0)
	{
		return n_onMenuItemClick (p0);
	}

	private native boolean n_onMenuItemClick (android.view.MenuItem p0);

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
