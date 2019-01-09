using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Timers;
using System.ComponentModel;


namespace TimeManager
{
    public class EventDirector
    {
        private string path = "events.xml";
        
        
        public ObservableCollection<Event> Events { get; set; }
        
        private XmlSerializer serializer;

        public EventDirector()
        {
            Events = new ObservableCollection<Event>();
            if (File.Exists(path)) //Checks if file events.xml exists
            {
                //Load from the file
                serializer = new XmlSerializer(Events.GetType());
                using (StreamReader sr = new StreamReader(path))
                {
                    Events = (ObservableCollection<Event>)serializer.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// Adds event created by AddEventWindow to the Events list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="note"></param>
        /// <param name="repeat"></param>
        public void Add(string name, string note, bool repeat)
        {
            Events.Add(new Event(name, note, repeat));
            Save();
        }

        /// <summary>
        /// Saves the Events collection as Events.xml
        /// </summary>
        public void Save()
        {
            serializer = new XmlSerializer(Events.GetType());
            using (StreamWriter sw = new StreamWriter(path))
            {
                serializer.Serialize(sw, Events);
            }
        }

        

        public void EventDone()
        {
            Event ev = Events[0];

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                Events.RemoveAt(0);
            });            

            if (ev.Repeat == true) //If the repeat bool of the finished event is true, saves the same Event at the end of Events
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Events.Add(ev);
                }); 
            }
            Save();
        }

       

    }
}
