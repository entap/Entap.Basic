﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Java.Lang;

namespace Entap.Basic.Core.Android
{
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class StarterActivity : Activity
    {
        const string launchedExtra = "launched";
        const string actualIntentExtra = "actual_intent";
        const string guidExtra = "guid";
        const string requestCodeExtra = "request_code";

        static readonly ConcurrentDictionary<string, IntentTask> pendingTasks =
            new ConcurrentDictionary<string, IntentTask>();

        bool launched;
        Intent actualIntent;
        string guid;
        int requestCode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var extras = savedInstanceState ?? Intent.Extras;

            launched = extras.GetBoolean(launchedExtra, false);
            actualIntent = extras.GetParcelable(actualIntentExtra) as Intent;
            guid = extras.GetString(guidExtra);
            requestCode = extras.GetInt(requestCodeExtra, -1);

            if (!launched)
                StartActivityForResult(actualIntent, requestCode);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean(launchedExtra, true);

            outState.PutParcelable(actualIntentExtra, actualIntent);
            outState.PutString(guidExtra, guid);
            outState.PutInt(requestCodeExtra, requestCode);

            base.OnSaveInstanceState(outState);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (GetIntentTask(guid, true) is IntentTask task)
            {
                if (resultCode == Result.Canceled)
                {
                    task.TaskCompletionSource.TrySetCanceled();
                }
                else
                {
                    try
                    {
                        data ??= new Intent();
                        task.TaskCompletionSource.TrySetResult(data);
                    }
                    catch (System.Exception ex)
                    {
                        task.TaskCompletionSource.TrySetException(ex);
                    }
                }
            }

            Finish();
        }

        /// <summary>
        /// 指定したIntentを起動し、結果を返す
        /// </summary>
        /// <returns>ActivityResult</returns>
        public static Task<Intent> StartAsync(Activity activity, Intent intent, int requestCode)
        {
            var data = new IntentTask();
            pendingTasks[data.Id] = data;

            var intermediateIntent = new Intent(activity, typeof(StarterActivity));
            intermediateIntent.PutExtra(actualIntentExtra, intent);
            intermediateIntent.PutExtra(guidExtra, data.Id);
            intermediateIntent.PutExtra(requestCodeExtra, requestCode);

            activity.StartActivityForResult(intermediateIntent, requestCode);

            return data.TaskCompletionSource.Task;
        }

        static IntentTask GetIntentTask(string guid, bool remove = false)
        {
            if (string.IsNullOrEmpty(guid))
                return null;

            if (remove)
            {
                pendingTasks.TryRemove(guid, out var removedTask);
                return removedTask;
            }

            pendingTasks.TryGetValue(guid, out var task);
            return task;
        }

        class IntentTask
        {
            public IntentTask()
            {
                Id = Guid.NewGuid().ToString();
                TaskCompletionSource = new TaskCompletionSource<Intent>();
            }

            public string Id { get; }

            public TaskCompletionSource<Intent> TaskCompletionSource { get; }
        }
    }
}
