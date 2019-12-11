using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CA2_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Creates list 
        List<Activity> allActivities;
        List<Activity> selectedActivities;
        List<Activity> filteredActivities;
        decimal totalCost = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allActivities = GetActivities();
            selectedActivities = new List<Activity>();
            filteredActivities = new List<Activity>();

            //Sorts activities by activity date
            allActivities.Sort();
            //Display list in listbox
            lbxActivities.ItemsSource = allActivities;

        }

        private List<Activity> GetActivities()
        {
            //Create objects
            List<Activity> allActivities = new List<Activity>();
            Activity l1 = new Activity()
            {
                Name = "Treking",
                Description = "Instructor led group trek through local mountains.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Land,
                Cost = 20m
            };

            Activity l2 = new Activity()
            {
                Name = "Mountain Biking",
                Description = "Instructor led half day mountain biking.  All equipment provided.",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Land,
                Cost = 30m
            };

            Activity l3 = new Activity()
            {
                Name = "Abseiling",
                Description = "Experience the rush of adrenaline as you descend cliff faces from 10-500m.",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Land,
                Cost = 40m
            };

            Activity w1 = new Activity()
            {
                Name = "Kayaking",
                Description = "Half day lakeland kayak with island picnic.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Water,
                Cost = 40m
            };

            Activity w2 = new Activity()
            {
                Name = "Surfing",
                Description = "2 hour surf lesson on the wild atlantic way",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Water,
                Cost = 25m
            };

            Activity w3 = new Activity()
            {
                Name = "Sailing",
                Description = "Full day lakeland kayak with island picnic.",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Water,
                Cost = 50m
            };

            Activity a1 = new Activity()
            {
                Name = "Parachuting",
                Description = "Experience the thrill of free fall while you tandem jump from an airplane.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Air,
                Cost = 100m
            };

            Activity a2 = new Activity()
            {
                Name = "Hang Gliding",
                Description = "Soar on hot air currents and enjoy spectacular views of the coastal region.",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Air,
                Cost = 80m
            };

            Activity a3 = new Activity()
            {
                Name = "Helicopter Tour",
                Description = "Experience the ultimate in aerial sight-seeing as you tour the area in our modern helicopters",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Air,
                Cost = 200m
            };

            //Add those to a list

            allActivities.Add(l1);
            allActivities.Add(l2);
            allActivities.Add(l3);

            allActivities.Add(w1);
            allActivities.Add(w2);
            allActivities.Add(w3);

            allActivities.Add(a1);
            allActivities.Add(a2);
            allActivities.Add(a3);

            return allActivities;
        }

        //For getting the description for a activity
        private void lbxActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity selectedActivity = lbxActivities.SelectedItem as Activity;
            if (selectedActivity != null)
                tblkDescription.Text = selectedActivity.Description;
        }

        //This function is for when the add button is pressed
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Figure out which item selected
            Activity selectedActivity = lbxActivities.SelectedItem as Activity;
            //Checking if NULL
            if (selectedActivity != null)
            {

                //check no date conflicts
                bool dateConflict = false;
                foreach (var activity in selectedActivities)
                {
                    if (selectedActivity.ActivityDate == activity.ActivityDate)
                    {
                        MessageBox.Show("Date conflict");
                        dateConflict = true;
                    }
                }

                if (!dateConflict)
                {
                    allActivities.Remove(selectedActivity);
                    selectedActivities.Add(selectedActivity);

                    //reset
                    lbxActivities.ItemsSource = null;
                    lbxUserActivities.ItemsSource = null;
                    lbxActivities.ItemsSource = allActivities;

                    //maintain filter view
                    if (rbAll.IsChecked == true) FilterType("All");
                    else if (rbLand.IsChecked == true) FilterType("Land");
                    else if (rbAir.IsChecked == true) FilterType("Air");
                    else if (rbWater.IsChecked == true) FilterType("Water");

                    selectedActivities.Sort();
                    lbxUserActivities.ItemsSource = selectedActivities;

                    //update total
                    totalCost += selectedActivity.Cost;
                    tblkCost.Text = totalCost.ToString("C");

                }

            }
            else
            {
                MessageBox.Show("Nothing selected");
            }
        }

        //This function is for when the remove button is pressed
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Figure out which item selected
            Activity selectedActivity = lbxUserActivities.SelectedItem as Activity;
            //Checking if NULL
            if (selectedActivity != null)
            {
                selectedActivities.Remove(selectedActivity);
                allActivities.Add(selectedActivity);

                //reset
                lbxActivities.ItemsSource = null;
                lbxUserActivities.ItemsSource = null;
                allActivities.Sort();
                rbAll.IsChecked = true;
                lbxActivities.ItemsSource = allActivities;
                lbxUserActivities.ItemsSource = selectedActivities;

                //update total
                totalCost -= selectedActivity.Cost;
                tblkCost.Text = totalCost.ToString("C");
            }
            else
                MessageBox.Show("Nothing selected");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRB = sender as RadioButton;
            string filterBy = selectedRB.Content as string;

            FilterType(filterBy);

        }

        private void FilterType(string filterBy)
        {

            filteredActivities.Clear();

            switch (filterBy)
            {
                case "Land":
                    
                    //NOTE: You can use the line below instead of the foreach loop - it's quicker
                    //lbxActivities.ItemsSource = allActivities.Where(a => a.TypeOfActivity == ActivityType.Land);

                    foreach (Activity activity in allActivities)
                    {
                        if (activity.TypeOfActivity == ActivityType.Land)
                            filteredActivities.Add(activity);
                    }

                    lbxActivities.ItemsSource = null;
                    lbxActivities.ItemsSource = filteredActivities;

                    break;
                case "Water":
                    
                    //lbxActivities.ItemsSource = allActivities.Where(a => a.TypeOfActivity == ActivityType.Water);

                    foreach (Activity activity in allActivities)
                    {
                        if (activity.TypeOfActivity == ActivityType.Water)
                            filteredActivities.Add(activity);
                    }

                    lbxActivities.ItemsSource = null;
                    lbxActivities.ItemsSource = filteredActivities;

                    break;
                case "Air":
                    //lbxActivities.ItemsSource = allActivities.Where(a => a.TypeOfActivity == ActivityType.Air);

                    foreach (Activity activity in allActivities)
                    {
                        if (activity.TypeOfActivity == ActivityType.Air)
                            filteredActivities.Add(activity);
                    }

                    lbxActivities.ItemsSource = null;
                    lbxActivities.ItemsSource = filteredActivities;

                    break;
                default:
                    lbxActivities.ItemsSource = allActivities;
                    break;

            }
        }

        //For getting the description for a activity
        private void lbxUserActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity selectedActivity = lbxUserActivities.SelectedItem as Activity;
            if (selectedActivity != null)
                tblkDescription.Text = selectedActivity.Description;
        }
    }
}
