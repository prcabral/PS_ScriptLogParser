using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace PS_ScriptLogParser
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(args[1]);

            string query = "*[System/EventID=4104]";
            EventLogQuery eventsQuery = new EventLogQuery("Microsoft-Windows-PowerShell/Operational", PathType.LogName, query);
            try
            {
                EventLogReader logReader = new EventLogReader(eventsQuery);

                for (EventRecord eventdetail = logReader.ReadEvent(); eventdetail != null; eventdetail = logReader.ReadEvent())
                {
                    // Read Event details
                    Console.WriteLine(eventdetail.FormatDescription());
                    Thread.Sleep(50000);
                    break;
                }
            }
            catch (EventLogNotFoundException e)
            {
                Console.WriteLine("Error while reading the event logs");
                return;
            }

        }
    }
}
