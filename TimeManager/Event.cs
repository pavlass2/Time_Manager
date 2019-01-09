using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager
{
    public class Event
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Repeat { get; set; }

        public Event(string name, string note, bool repeat)
        {
            Name = name;
            Note = note;
            Repeat = repeat;
        }

        public Event()
        {

        }

        public override string ToString()
        {
            if (this.Note == "")
            {
                return this.Name;
            }
            else return (this.Name + " Note: " + this.Note);
        }

    }
}
