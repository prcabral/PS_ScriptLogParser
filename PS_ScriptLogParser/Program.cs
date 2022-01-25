using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using System.IO;
namespace PS_ScriptLogParser
{
    class Program
    {
        private static readonly string Help =
            "\r\n\t " +
            @"Example: PS_ScriptlogParser.exe -f ""C:\Windows\System32\winevt\Logs\Microsoft-Windows-PowerShell%4Operational.evtx"" -o ""c:\temp\out"" " +
            "\r\n\t ";
        public static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine(Help);
                return;
            }
            
            string pattern = @"\d+";
            Regex rg = new Regex(pattern);
            string query = "*[System/EventID=4104]";
            EventLogQuery eventsQuery = new EventLogQuery(args[1], PathType.FilePath, query);
            try
            {
                EventLogReader logReader = new EventLogReader(eventsQuery);
                if (!Directory.Exists(args[3]))
                {
                    System.IO.Directory.CreateDirectory(args[3]);
                }
                for (EventRecord eventdetail = logReader.ReadEvent(); eventdetail != null; eventdetail = logReader.ReadEvent())
                {
                    string eventlog = eventdetail.FormatDescription();
                    string[] lines = eventlog.Split('\n');

                    List<string> listLines = new List<string>(lines);

                    string firstLine = listLines.First();
                    Console.WriteLine(firstLine);
                    MatchCollection matchedInt = rg.Matches(firstLine);
                    Console.WriteLine(eventdetail.TimeCreated.ToString());
                    listLines.RemoveAt(0);
                    listLines.RemoveAt(listLines.Count - 1);
                    listLines.RemoveAt(listLines.Count - 1);
                    string stringFile = "";

                    string date = String.Format("{0:yyyy-MM-dd HH-mm-ss}", eventdetail.TimeCreated);
                    foreach (string i in listLines)
                    {
                        stringFile += i + '\n';
                    }

                    string path = Path.Combine(args[3] + "\\" + date + ".txt");

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.Write(stringFile);
                    }
                }
            }
            catch(Exception ex)
            {
                if (ex is EventLogNotFoundException)
                {
                    Console.WriteLine("Error while reading the event logs");
                    return;
                }
                if (ex is System.UnauthorizedAccessException)
                {
                    Console.WriteLine("Please use an administrator shell to read the Powershell logs.");
                    return;
                }
            }
        }
    }
}
