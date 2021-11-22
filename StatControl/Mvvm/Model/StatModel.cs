using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model
{
    public class StatModel
    {
        public StatModel(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public int Value { get; set; }
    }
}
