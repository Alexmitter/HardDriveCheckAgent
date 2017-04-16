using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace HardDriveCheckIFB
{
    class Program
    {

        public static int zähler;

        static void Main(string[] args)
        {
            foreach (string element in args)
            {
                if (element.Contains("/name="))
                {
                    var element1 = element.Replace("/name=", "");
                    Name = element1.ToString();
                    continue;
                }

                if (element.ToString().Length == 1)
                {
                    Pfad = element.ToString();
                    pfad2 = element.ToString();
                }
                else
                {
                    

                    if (element.ToString() == "/?")
                    {
                        Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine(" ║ Programmpfad.exe C C:\\Ergebnisspeicherort\\ /name=test              ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║  Prorammpfad.exe Repressentiert dieses Programm                      ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║  C Repressentiert den zu Überwachenden Laufwerksbuchstaben           ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║  C:\\Ergebnisspeicherort\\ Repressentiert den Speicherort des Logs   ║");
                        Console.WriteLine(" ║  Auf Lokalem Datenträger oder Server                                 ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║  /name= dann Name, in dem fall test, das ist der PC Name             ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║                                                                      ║");
                        Console.WriteLine(" ║  von Alexander Mittermeier                                           ║");
                        Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════╝");
                    }
                    else
                    {        
                        PfadtoServer = element.ToString();
                    }

                    

                }
                
            }

            NewChecker();
            //Checker();
            Schreiber();
        }


        public static string Name;

        public static string PfadtoServer;
        public static string Pfad;


        public static string space;
        public static long spaceint;
        public static long spaceint2;


        public static void Checker()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                try
                {
                    Pfad = Pfad + ":\\";
                    if (d.Name == Pfad)
                    {
                        spaceint = d.TotalFreeSpace / (1024 * 1024 * 1024);
                        spaceint2 = d.TotalSize / (1024 * 1024 * 1024); ;
                        space = spaceint + " GB von insgesamt " + spaceint2 + " GB frei";
                        Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + " - " + space);
                    }   
                }
                catch (Exception exep)
                {
                    Console.WriteLine(exep.ToString());
                    ergebnis = exep.ToString();
                }

            }
        }

        public static string pfad2;
        public static string ergebnis2;
        public static string ergebnis;        
        

        public static void Schreiber()
        {

            string PfadAufServer = PfadtoServer + Name +"_speicherlog_" +".log";
            //string PfadAufServer = PfadtoServer + "_speicherlog_" + Name + "_.log";
            if (zähler == 1)
            {
                ergebnis = DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + " - " + space;
                zähler = 2;
            }

            if (File.Exists(PfadAufServer))
            {
                using (StreamReader sr = new StreamReader(PfadAufServer))
                {
                    String line = sr.ReadToEnd();
                    ergebnis2 = line;
                }
            }

            ergebnis = ergebnis + "\r\n" + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + " - " + space + ergebnis2;

            try
            {
                using (StreamWriter sw = new StreamWriter(PfadAufServer))
                {
                    sw.WriteLine(ergebnis);
                }
            }
            catch        
            {
                PfadAufServer.Replace("/name=", "");
                string Pfad = PfadtoServer;
                DirectoryInfo di = Directory.CreateDirectory(Pfad);

                using (StreamWriter sw = new StreamWriter(PfadAufServer))
                {
                    sw.WriteLine(ergebnis);
                }
            }
        }


        [DllImport("kernel32")]
        public static extern int GetDiskFreeSpaceEx(string lpDirectoryName, ref long lpFreeBytesAvailable, ref long lpTotalNumberOfBytes, ref long lpTotalNumberOfFreeBytes);
        private static void NewChecker()
        {
            long freeBytesAvailable = 0;
            long totalNumberOfBytes = 0;
            long totalNumberOfFreeBytes = 0;

            GetDiskFreeSpaceEx(Pfad, ref freeBytesAvailable, ref
               totalNumberOfBytes, ref totalNumberOfFreeBytes);

            long InterneLong = freeBytesAvailable;
            long ergebnis3 = InterneLong  / (1024 * 1024 * 1024);
            string ergebnis11 = ergebnis3.ToString();

            long InternetLong2 = totalNumberOfBytes;
            long ergebnis32 = InternetLong2 / (1024 * 1024 * 1024);
            string ergebnis12 = ergebnis32.ToString();

            space = ergebnis11 + " GB von insgesamt " + ergebnis12 + " GB frei";
            string internergebnis =  DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + " - " + space;
            Console.WriteLine(internergebnis);
        }
        
    }
}
