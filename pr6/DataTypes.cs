using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr6
{
        [Serializable]
        public class DayChoice
    {
        public string FirstSelectedItemImagePath { get; set; }
        public DateTime Date { get; set; }
        public bool CheckBox1State { get; set; }
        public bool CheckBox2State { get; set; }
        public bool CheckBox3State { get; set; }
        public bool CheckBox4State { get; set; }
        public bool CheckBox5State { get; set; }
        public bool CheckBox6State { get; set; }
    }

    [Serializable]
        public class ItemChoice
        {
            public string Name { get; set; }
            public string ImagePath { get; set; }
            public bool IsSelected { get; set; }
        }
    
}
