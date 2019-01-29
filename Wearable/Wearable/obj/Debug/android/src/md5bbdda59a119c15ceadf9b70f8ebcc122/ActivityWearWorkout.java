package md5bbdda59a119c15ceadf9b70f8ebcc122;


public class ActivityWearWorkout
	extends android.support.wearable.activity.WearableActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onEnterAmbient:(Landroid/os/Bundle;)V:GetOnEnterAmbient_Landroid_os_Bundle_Handler\n" +
			"n_onUpdateAmbient:()V:GetOnUpdateAmbientHandler\n" +
			"n_onExitAmbient:()V:GetOnExitAmbientHandler\n" +
			"n_onKeyDown:(ILandroid/view/KeyEvent;)Z:GetOnKeyDown_ILandroid_view_KeyEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("Wearable.src.Activities.ActivityWearWorkout, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivityWearWorkout.class, __md_methods);
	}


	public ActivityWearWorkout ()
	{
		super ();
		if (getClass () == ActivityWearWorkout.class)
			mono.android.TypeManager.Activate ("Wearable.src.Activities.ActivityWearWorkout, Wearable, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onEnterAmbient (android.os.Bundle p0)
	{
		n_onEnterAmbient (p0);
	}

	private native void n_onEnterAmbient (android.os.Bundle p0);


	public void onUpdateAmbient ()
	{
		n_onUpdateAmbient ();
	}

	private native void n_onUpdateAmbient ();


	public void onExitAmbient ()
	{
		n_onExitAmbient ();
	}

	private native void n_onExitAmbient ();


	public boolean onKeyDown (int p0, android.view.KeyEvent p1)
	{
		return n_onKeyDown (p0, p1);
	}

	private native boolean n_onKeyDown (int p0, android.view.KeyEvent p1);

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
