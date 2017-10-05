using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Util;
using Android.Views;
using Java.Lang;

namespace MaterialSteppers.Adapter
{
    public class FragmentStateCurrentPageAdapter: FragmentStatePagerAdapter,ViewPager.IOnPageChangeListener
    {
        int currentPage = 0;
        List<Java.Lang.Class> mStepperFragment;

        private SparseArray<Fragment> mPageReferenceMap = new SparseArray<Fragment>();

        public FragmentStateCurrentPageAdapter(Android.Support.V4.App.FragmentManager fm): base(fm)
        {
            
        }

        public override int Count =>  mStepperFragment.Count;



        public void SetFragments(List<Java.Lang.Class> fragments)
        {
            mStepperFragment = fragments;
        }

        public override Fragment GetItem(int index)
        {
            if (mPageReferenceMap.Get(index) != null)
            {
                return GetItemAtIndex(index);
            }
            else
            {
                try
                {
                    Fragment obj = (Fragment)mStepperFragment[index].NewInstance();
                    mPageReferenceMap.Put(index, obj);
                    return obj;
                }
                catch (InstantiationException e)
                {
                    e.PrintStackTrace();
                    return null;
                }
                catch (IllegalAccessException e)
                {
                    e.PrintStackTrace();
                    return null;
                }
            }
        }
        public Fragment GetItemAtIndex(int index)
        {
            return mPageReferenceMap.Get(index);
        }

        public override void DestroyItem(View container, int position, Object objectValue)
        {
            base.DestroyItem(container, position, objectValue);
            mPageReferenceMap.Remove(position);
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageSelected(int position)
        {
            currentPage = position;
        }
    }
}