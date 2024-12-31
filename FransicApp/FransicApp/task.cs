using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using Android.Content;
using Android.Widget;
using Android.Graphics;

namespace FransicApp 
{
    public class task
    {
        public CheckBox CheckBox { get; private set; }
        public Button DeleteButton { get; private set; }
        public LinearLayout CheckBoxContainer { get; private set; }

        public task(Context context, string description,Action<task> onDelete)
        {
            CheckBox = new CheckBox(context)
            {
                Text = description
            };

            DeleteButton = new Button(context)
            {
                Text = "Delete"
            };

            // Set the Delete button to be on the right side with a little padding and styling
            DeleteButton.SetBackgroundColor(Color.Red); // Set the background color to red for contrast
            DeleteButton.SetTextColor(Color.White); // Set text color to white
            DeleteButton.SetPadding(20, 10, 20, 10); // Add padding to the button for better appearance
            DeleteButton.SetTypeface(Typeface.DefaultBold, TypefaceStyle.Bold); // Set bold font for emphasis


            CheckBoxContainer = new LinearLayout(context)
            {
                Orientation = Orientation.Horizontal, 
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };

            CheckBoxContainer.SetMinimumHeight((int)(CheckBox.TextSize * 2));
            CheckBoxContainer.AddView(CheckBox);
            CheckBoxContainer.SetBackgroundColor(Color.Gray);
            CheckBoxContainer.AddView(DeleteButton);

            CheckBox.CheckedChange += (sender, e) =>
            {
                if (e.IsChecked)
                {
                    CheckBoxContainer.SetBackgroundColor(Color.Green); 
                }
                else
                {
                    CheckBoxContainer.SetBackgroundColor(Color.Transparent); 
                }
            };

            DeleteButton.Click += (sender, e) =>
            {
                onDelete(this);  
            };
        }
    }
    
}
