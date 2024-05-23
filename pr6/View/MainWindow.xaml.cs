using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace pr6
{
    public partial class MainWindow : Window
    {
        private const string DataFilePath = "UserData.json"; // Путь к файлу данных
        private DayChoice currentDayChoice;

        DateTime dateTime = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();
            Generate();
            LoadData(); // Вызов метода загрузки данных при инициализации окна
        }
        private void LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                currentDayChoice = SerializationHelper.DeserializeFromJson<DayChoice>(DataFilePath);

                Vibor viborPage = PageFrame.Content as Vibor;

                if (viborPage != null)
                {
                    viborPage.CheckBox1.IsChecked = currentDayChoice.CheckBox1State;
                    viborPage.CheckBox2.IsChecked = currentDayChoice.CheckBox2State;
                    viborPage.CheckBox3.IsChecked = currentDayChoice.CheckBox3State;
                    viborPage.CheckBox4.IsChecked = currentDayChoice.CheckBox4State;
                    viborPage.CheckBox5.IsChecked = currentDayChoice.CheckBox5State;
                    viborPage.CheckBox6.IsChecked = currentDayChoice.CheckBox6State;
                }
            }
            else
            {
                currentDayChoice = new DayChoice();
            }
        }

        private void SaveData()
        {
            Vibor viborPage = PageFrame.Content as Vibor;

            if (viborPage != null)
            {
                currentDayChoice.CheckBox1State = viborPage.CheckBox1.IsChecked ?? false;
                currentDayChoice.CheckBox2State = viborPage.CheckBox2.IsChecked ?? false;
                currentDayChoice.CheckBox3State = viborPage.CheckBox3.IsChecked ?? false;
                currentDayChoice.CheckBox4State = viborPage.CheckBox4.IsChecked ?? false;
                currentDayChoice.CheckBox5State = viborPage.CheckBox5.IsChecked ?? false;
                currentDayChoice.CheckBox6State = viborPage.CheckBox6.IsChecked ?? false;

                // Save the first selected item's image path
                var checkBoxes = new CheckBox[] { viborPage.CheckBox1, viborPage.CheckBox2, viborPage.CheckBox3, viborPage.CheckBox4, viborPage.CheckBox5, viborPage.CheckBox6 };
                var images = new string[] { "images/shampoo.jpg", "images/kond.jpg", "images/ytuzhok.jpg", "images/maska.jpg", "images/vse.jpg", "images/пох.png" };

                for (int i = 0; i < checkBoxes.Length; i++)
                {
                    if (checkBoxes[i].IsChecked == true)
                    {
                        currentDayChoice.FirstSelectedItemImagePath = images[i];
                        break;
                    }
                }
            }

            // Serialize the currentDayChoice to JSON and save it to the file
            SerializationHelper.SerializeToJson(DataFilePath, currentDayChoice);
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            SaveData();
            if (PageFrame.Content != null && PageFrame.Content.GetType() == typeof(Vibor))
            {
                PageFrame.Content = null;
            }
            dateTime = dateTime.AddMonths(-1);
            Generate();
            
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            dateTime = dateTime.AddMonths(1);
            Generate();
        }
        private void UpdateCalendarUI()
        {
            daytxt.Text = dateTime.ToString("MMMM yyyy г.");
            calendarik.Children.Clear();
            for (int i = 1; i <= DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i++)
            {
                Calendar calendar = new Calendar();
                calendar.Day = i.ToString();
                calendarik.Children.Add(calendar);
            }
        }
        public void Generate()
        {
            UpdateCalendarUI();

            // Update the image based on the first selected item's image path
            Calendar calendar = calendarik.Children[dateTime.Day - 1] as Calendar;
            if (calendar != null)
            {
                calendar.UpdateImage(currentDayChoice.FirstSelectedItemImagePath);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveData();

        }

        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (PageFrame.Content != null && PageFrame.Content.GetType() == typeof(Vibor))
            {
                // Скрыть кнопку "Сюда"
                Suda.Visibility = Visibility.Collapsed;

                // Показать кнопку "Сохранить"
                Save.Visibility = Visibility.Visible;
            }
            else
            {
                // Показать кнопку "Сюда"
                Suda.Visibility = Visibility.Visible;

                // Скрыть кнопку "Сохранить"
                Save.Visibility = Visibility.Collapsed;
            }
        }
    }
}
