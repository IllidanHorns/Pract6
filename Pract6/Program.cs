using Newtonsoft.Json;
using Pract6;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Net.Security;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Drawing;

public class MainGlobal
{
    public static List<string> FilmsSet = new List<string>();
    static void Main()
    {
        SerilOrDesirelClass.Seril(StartWay());
        StrelochkiClass.MenuText();

    }
    private static string StartWay()
    {
        Console.WriteLine("Введите путь до файла, который хотите открыть");
        Console.WriteLine("--------------------------------------------------------------------------------------------------");
        string Way;
        Way = Console.ReadLine();
        return Way;
    }

    public static class SerilOrDesirelClass
    {
        public static void Seril(string Way)
        {
            if (Way.EndsWith(".txt") == true)
            {
                TxtSer(Way);
            }
            else if (Way.EndsWith(".json") == true)
            {
                JsonSer(Way);
            }
            else if (Way.EndsWith(".xml") == true)
            {
                XmlSer(Way);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }

        private static void TxtSer(string Way)
        {
            try
            {
                string[] filmtxt;
                filmtxt = File.ReadAllLines(Way);
                for (int i = 0; i < filmtxt.Length; i++)
                {
                    MainGlobal.FilmsSet.Add(filmtxt[i]);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }
        private static void JsonSer(string Way)
        {
                string txt = File.ReadAllText(Way);
                List<Film> result = JsonConvert.DeserializeObject<List<Film>>(txt);
                foreach (Film i in result)
                {
                    MainGlobal.FilmsSet.Add(i.name);
                    MainGlobal.FilmsSet.Add(i.duration.ToString());
                    MainGlobal.FilmsSet.Add(i.rating.ToString());
                }
        }
        private static void XmlSer(string Way)
        {
                List<Film> a;
                XmlSerializer xml = new XmlSerializer(typeof(List<Film>));
                using (FileStream fs = new FileStream(Way, FileMode.Open))
                {
                    a = (List<Film>)xml.Deserialize(fs);
                }
                foreach (Film i in a)
                {
                    MainGlobal.FilmsSet.Add(i.name);
                    MainGlobal.FilmsSet.Add(i.duration.ToString());
                    MainGlobal.FilmsSet.Add(i.rating.ToString());
                }
        }
        public static void Desirel(string Way)
        {
            if (Way.EndsWith(".txt") == true)
            {
                foreach (string i in MainGlobal.FilmsSet)
                {
                    File.AppendAllText(Way, i + "\n");
                }
                Environment.Exit(0);
            }
            else if (Way.EndsWith(".json") == true)
            {
                List<Film> Films = new List<Film>();
                int duration;
                int rating;
                for (int i = 0; i < MainGlobal.FilmsSet.Count();  i = i+3)
                {
                    Film a = new Film(MainGlobal.FilmsSet[i], duration = Convert.ToInt32(MainGlobal.FilmsSet[i + 1]), rating = Convert.ToInt32(MainGlobal.FilmsSet[i + 2])) ; 
                    Films.Add(a);
                }
                string json = JsonConvert.SerializeObject(Films);
                File.WriteAllText(Way, json);
                Environment.Exit(0);
            }
            else if (Way.EndsWith(".xml") == true)
            {
                List<Film> Films = new List<Film>();
                int duration;
                int rating;
                for (int i = 0; i < MainGlobal.FilmsSet.Count(); i = i + 3)
                {
                    Film a = new Film(MainGlobal.FilmsSet[i], duration = Convert.ToInt32(MainGlobal.FilmsSet[i + 1]), rating = Convert.ToInt32(MainGlobal.FilmsSet[i + 2]));
                    Films.Add(a);
                }
                XmlSerializer xml = new XmlSerializer(typeof(List<Film>));
                using (FileStream fs = new FileStream(Way, FileMode.OpenOrCreate)) 
                {
                    xml.Serialize(fs, Films);
                }
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }

    }

    public static class StrelochkiClass
    {
        public static void MenuText()
        {
            Console.Clear();
            Console.WriteLine("F1 - сохранить файл в одном из форматов: txt, json, xml. Escape - закрыть программу.");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            foreach (string i in MainGlobal.FilmsSet)
            {
                Console.WriteLine("  " + i);
            }
            var position = 2;
            while (true)
            {
                ConsoleKeyInfo UserButton;
                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");
                UserButton = Console.ReadKey(); 
                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");
                if (UserButton.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь, куда хотите сохранить измененный файл");
                    Console.WriteLine("--------------------------------------------------------------------");
                    string Way;
                    Way = Console.ReadLine();
                    SerilOrDesirelClass.Desirel(Way);
                }
                else if (UserButton.Key == ConsoleKey.DownArrow & position != MainGlobal.FilmsSet.Count() + 1)
                {
                    position++;
                }
                else if (UserButton.Key == ConsoleKey.UpArrow & position != 2)
                {
                    position--;
                }
                else if (UserButton.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (UserButton.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("                                                                                                                ");
                    Console.SetCursorPosition(2, position);
                    MainGlobal.FilmsSet[position-2]=Console.ReadLine();
                    
                }

            }
        }
    }
}


