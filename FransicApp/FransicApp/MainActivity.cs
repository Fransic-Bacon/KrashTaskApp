using System;
using System.Collections.Generic;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using static Android.Media.Session.MediaSession;
using Newtonsoft.Json;




namespace FransicApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {

        List<task> obj = new List<task>();

        private bool empty = true;
        private AutoCompleteTextView input;
        private Button bt;
        private Handler handler = new Handler();

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.activity_main);
            bt = FindViewById<Button>(Resource.Id.button1);
            LinearLayout dynamicContainer = FindViewById<LinearLayout>(Resource.Id.dynamicContainer);
            input = FindViewById<AutoCompleteTextView>(Resource.Id.input);

            LoadTasks();

            update();

            bt.Click += (sender, e) =>
            {

                if (empty == false)
                {

                    task boxen = new task(this, input.Text, DeleteTask);
                    //"Dynamic CheckBox " + (obj.Count + 1)
                    obj.Add(boxen);

                    dynamicContainer.AddView(boxen.CheckBoxContainer);
                    input.Visibility = Android.Views.ViewStates.Gone;
                    input.Text = "";
                    empty = true;

                    SaveTasks();
                }

                else if (empty == true)
                {
                    input.Visibility = Android.Views.ViewStates.Visible;

                }

            };

        }

        private void update()
        {
            handler.PostDelayed(() =>
            {

                if (input.Text.Length > 0 && empty == true)
                {       
                        bt.Text = "Set task";
                        empty = false;
                }


                update();
            }, 100);
        }

        private void DeleteTask(task task)
        {
            LinearLayout dynamicContainer = FindViewById<LinearLayout>(Resource.Id.dynamicContainer);
            dynamicContainer.RemoveView(task.CheckBoxContainer);
        
        }


        private void SaveTasks()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var editor = prefs.Edit();

            var json = JsonConvert.SerializeObject(obj.Select(t => t.CheckBox.Text).ToList());
            editor.PutString("tasks", json);
            editor.Apply();
        }

   
        private void LoadTasks()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var json = prefs.GetString("tasks", "[]");

            try
            {
               
                var taskDescriptions = JsonConvert.DeserializeObject<List<string>>(json);
                if (taskDescriptions != null)
                {
                    foreach (var description in taskDescriptions)
                    {
                        task boxen = new task(this, description, DeleteTask);
                        obj.Add(boxen);
                        LinearLayout dynamicContainer = FindViewById<LinearLayout>(Resource.Id.dynamicContainer);
                        dynamicContainer.AddView(boxen.CheckBoxContainer);
                    }
                }
            }
            catch (Exception)
            {
          
            }
        }
    }
}
    
