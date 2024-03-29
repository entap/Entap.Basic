﻿using System;
using System.Collections.Generic;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;
using Xamarin.Forms;

namespace Sample
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel()
        {
            
        }

        public ProcessCommand PageManagerCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushAsync<MyTabbedPage>(new MyTabbedPageViewModel()));

        public ProcessCommand GuideCommand =>new ProcessCommand(async () =>
        {
            var contents = new List<GuideContent>()
            {
                new GuideContent { Title = "SHIRO.COへようこそ", Description = $"SHIRO.COアプリを使って{Environment.NewLine}より快適なお買い物をお楽しみください。", IsDescriptionCentering = true, Next = "次へ", Source = "image_guide01.png"},
                new GuideContent { Title = "SHIRO.お得なクーポンをもらおう", Description = "会員登録をすると、さらにお得なクーポンが配信されます。", Next = "次へ", Source = "image_guide02.png" },
                new GuideContent { Title = "ギャラリーに展示しよう", Description = "毎月、SHIROアプリ内で共有された写真の中からカフェギャラリーに展示される写真が選ばれます。", Next = "はじめる", Source = "image_guide03.png" },
                new GuideContent { ContentType = GuideContentType.Animation, Title = "サンプル", Description = "アニメーション", Next = "はじめる", Source = "splash.json" },
            };
            await PageManager.Navigation.PushAsync<GuidePage>(new GuidePageViewModel(contents));
        });

        public ProcessCommand AppleSignInCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushAsync<AppleSignInPage>(new AppleSignInPageViewModel()));

        
    }
}
