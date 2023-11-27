using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ST10084623_PROG7312
{
    public class Controls
    {
        public static void SwapIndexes(int change, ListBox listbox)
        {
            //firsr ensure the item is selected
            if (listbox.SelectedItem == null || listbox.SelectedIndex < 0)
            {
                //if either of the lists have no items selected
                return;
            }
            //target destination
            int newIndex = listbox.SelectedIndex + change;
            //ensure new destination exists
            if (newIndex < 0 || newIndex >= listbox.Items.Count)

                return;


            //object selected
            object selected = listbox.SelectedItem;

            //insert into a new location
            listbox.Items.Remove((string)selected);
            listbox.Items.Insert(newIndex, (string)selected);
            listbox.SelectedIndex = newIndex;





        }//end of method

        public static void randomizeList(ListBox listbox)
        {
            //new list type of
            var list = new List<string>();

            Random random = new Random(); // to gen random list every time

            list = listbox.Items.Cast<String>().ToList();

            //shuffle the list of items
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n);
                n--; //decrements the value
                string value = list[k];
                list[k] = list[n]; //swapping
                list[n] = value;


            }

            listbox.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listbox.Items.Add(list[i]);
            }
        }
    }

}
