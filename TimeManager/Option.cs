using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager
{
    public class Option
    {
        public int EventLength { get; private set; }
        public int BreakLength { get; private set; }

        public Option(int eventLength, int breakLength)
        {
            EventLength = eventLength;
            BreakLength = breakLength;
        }

        public Option()
        {

        }
    }
}
