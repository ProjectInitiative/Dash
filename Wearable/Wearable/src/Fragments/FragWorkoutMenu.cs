using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Wearable.src.Fragments
{
    public class FragWorkoutMenu : Fragment
    {
        private View rootView;
        private GlobalDataDash app;

        private Button btn_pause,
                       btn_stop;

        private IMenuButtonListener menuButtonListener;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            app = GlobalDataDash.GetInstance();
            rootView = inflater.Inflate(Resource.Layout.workout_menu, container, false);
            btn_pause = (Button)rootView.FindViewById(Resource.Id.btn_pause);
            if (app.isPaused)
                btn_pause.Text = GetString(Resource.String.btn_pause);
            btn_stop = (Button)rootView.FindViewById(Resource.Id.btn_stop);
            SetupPauseBtn();
            SetupStopBtn();

            return rootView;
        }

        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);
            try
            {
                menuButtonListener = (IMenuButtonListener)activity;
            }
            catch
            {
                throw new InvalidCastException(activity.ToString()
                    + " must implement MenuButtonListner");
            }
        }

        public void SetupPauseBtn()
        {
            btn_pause.Click += delegate
            {
                menuButtonListener.MenuButtonClicked(btn_pause);
            };
        }

        public void SetupStopBtn()
        {
            btn_stop.Click += delegate
            {
                menuButtonListener.MenuButtonClicked(btn_stop);
            };
        }

        public interface IMenuButtonListener
        {
            void MenuButtonClicked(Button btn);
        }
    }
}