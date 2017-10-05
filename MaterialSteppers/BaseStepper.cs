using Android.Support.V4.App;
using Android.Support.V4.View;
using Java.Lang;
using MaterialSteppers.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialSteppers
{
    public class BaseStepper
    {
        private ViewPager mViewPager;
        private FragmentStateCurrentPageAdapter fragmentAdapter;
        public int CURRENT_PAGE = 0;
        public int TOTAL_PAGE = 0;

        public BaseStepper(ViewPager viewPager, List<Class> mStepperFragment, FragmentManager fm)
        {

            mViewPager = viewPager;

            fragmentAdapter = new FragmentStateCurrentPageAdapter(fm);
            fragmentAdapter.SetFragments(mStepperFragment);
            mViewPager.Adapter=(fragmentAdapter);

            TOTAL_PAGE = mStepperFragment.Count;
        }

        public void OnNextButtonClicked()
        {
            CURRENT_PAGE = mViewPager.CurrentItem;
            if (ResolveNavigation())
            {
                StepperFragment current = (StepperFragment)fragmentAdapter.GetItem(CURRENT_PAGE);
                if (current != null && current.OnNextButtonHandler())
                {
                    CURRENT_PAGE = CURRENT_PAGE + 1;
                    mViewPager.CurrentItem=(CURRENT_PAGE);
                }
            }
        }

        public void OnBackButtonClicked()
        {
            CURRENT_PAGE = CURRENT_PAGE - 1;
            mViewPager.CurrentItem=(CURRENT_PAGE);

        }

        public bool ResolveNavigation()
        {
            return CURRENT_PAGE != (TOTAL_PAGE - 1);
        }
    }
}
