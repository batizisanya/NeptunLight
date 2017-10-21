﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Java.Lang;
using JetBrains.Annotations;
using NeptunLight.Droid.Utils;
using NeptunLight.ViewModels;
using ReactiveUI;

namespace NeptunLight.Droid.Views
{
    public class CoursesPage : ReactiveFragment<CoursesPageViewModel>, IActionBarProvider
    {
        private ViewPager Pager { get; [UsedImplicitly] set; }

        public override View OnCreateView(LayoutInflater inflater, [CanBeNull] ViewGroup container, [CanBeNull] Bundle savedInstanceState)
        {
            View layout = inflater.Inflate(Resource.Layout.CoursesPage, container, false);

            this.WireUpControls(layout);

            this.WhenAnyValue(x => x.ViewModel.Tabs).Subscribe(tabs =>
            {
                Pager.Adapter = new TabAdapter(ChildFragmentManager, tabs);
            });

            return layout;
        }

        private class TabAdapter : TabListAdapter<CoursesTabViewModel, CoursesTab>
        {
            public TabAdapter(FragmentManager fm, IEnumerable<CoursesTabViewModel> items) : base(fm, items)
            {
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(Items[position].SemesterName);
            }
        }
    }
}