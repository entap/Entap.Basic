﻿using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class MyTabbedPageViewModel : PageViewModelBase
    {
        public MyTabbedPageViewModel()
        {
        }

        public PageManagerPageViewModel PageManagerPageViewModel { get; set; } = new PageManagerPageViewModel();

        public MainPageViewModel MainPageViewModel { get; set; } = new MainPageViewModel();
    }
}
