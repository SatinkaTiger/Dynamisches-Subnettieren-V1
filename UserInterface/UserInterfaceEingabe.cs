namespace Dynamisches_Subnettieren_V1.UserInterface
{
    using Dynamisches_Subnettieren_V1.IPv4;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UserInterfaceEingabe
    {
        static string Version = "V0.0.1";


        public static void ProgarmHead()
        {
            Console.Clear();
            Console.WriteLine($"Subnetting {Version}\nCopyright Rick Kummer\n\n");
        }
        public static bool UserDialog(List<Tuple<string, int>> NetworkItem)
        {
            //Programmkopf mit Teilnetzwerken
            ProgarmHead();
            foreach (var item in NetworkItem)
                Console.WriteLine($"{item.Item1}: {item.Item2} Host's");
            Console.WriteLine("Bestätigen Sie die Richtigkeit der Eingabe\nIst Ihre Eingabe korrekt? [j/n]: ");
            if (Console.ReadLine() == "j")
                return true;
            else
                return false;
        }

        public static int GetPräfixFormUser()
        {
            //Präfix Abfragen, prüfen und zurückgeben
            while (true)
            {
                int IntPräfix;
                ProgarmHead();
                Console.Write("Bitte geben Sie ihren Präfix an: ");
                if (int.TryParse(Console.ReadLine(), out IntPräfix))
                {
                    if (IntPräfix >= 8 && IntPräfix <= 32)
                    {
                        return IntPräfix;
                    }
                }
                ErrorInputPräfix();
            }
        }
        public static Tuple<string, int> GetSubnetDataFormUser(int Präfix)
        {
            //Fragt den Nutzer nach dem Namen des Teilnetzwerks und der Anzahl der Hosts. Im Anschluss wird die Eingabe als Tupel Zurückgegeben
            string Name;
            int Hosts;
            while (true)
            {
                ProgarmHead();
                Console.Write("Bitte geben Sie ein den Namen des Teilnetzwerks an: ");
                Name = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(Name))
                    break;
            }
            while (true)
            {
                ProgarmHead();
                Console.Write("Bitte geben sie die Anzahl der Hosts an: ");
                if (int.TryParse(Console.ReadLine(), out Hosts))
                    if (MethodenIPv4.GetBitToAdress(Hosts) + Präfix < 32)
                        break;
                ErrorInputPräfix(Name);

            }
            return Tuple.Create(Name, Hosts);
        }
        public static int[] GetNetAdressFromUser()
        {
            //Fordert den Nutzer auf, die Netzadresse einzugeben und gibt sie als Array aus Integern zurück 
            while (true)
            {
                ProgarmHead();
                Console.Write("Bitte geben Sie die Netzadresse an: ");
                Tuple<bool, int[]> Input = GetOktetts(Console.ReadLine() ?? "");
                if (!Input.Item1)
                    return Input.Item2;
                ErrorInputIp();
            }
        }
        static Tuple<bool, int[]> GetOktetts(string InputIP)
        {
            //Zerlegt den String in Vier Oktetten und gibt eien Tuple mit Bool (Eror) und einen Array mit den Oktetten zurück
            int[] Oktestts = new int[4];
            int Count = 0;
            string Temp = "";
            for (int i = 0; i < Oktestts.Length; i++)
                for (int j = Count; j < InputIP.Length; j++)
                {
                    if (InputIP[j] == '.')
                    {
                        if (int.TryParse(Temp, out Oktestts[i]))
                        {
                            if (CheckOktett(Oktestts[i]))
                                return new Tuple<bool, int[]>(true, Oktestts);
                            else
                            {
                                Temp = "";
                                Count++;
                                break;
                            }
                        }
                    }
                    else if (j == 3)
                    {
                        if (int.TryParse(Temp, out Oktestts[i]))
                        {
                            if (CheckOktett(Oktestts[i]))
                                return new Tuple<bool, int[]>(true, Oktestts);
                            else
                            {
                                Temp = "";
                                Count++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Temp += InputIP[j];
                        Count++;
                    }
                }
            return new Tuple<bool, int[]>(false, Oktestts);
        }
        public static bool CheckOktett(int Input)
        {
            //Checkt auf den richtigen Bereich der Adressierung
            if (Input >= 0 && Input < 256)
                return false;
            else
                return true;
        }
        static void ErrorInputIp()
        {
            //Fehlerbehandlung bei Falscher Ip
            ProgarmHead();
            Console.WriteLine("Sie haben die Adresse nicht korrekt angegeben.\nWeiter mit beliebiger Taste");
            Console.ReadKey();
        }
        public static void ErrorInputPräfix()
        {
            //Fehlermeldung falscher Präfix
            ProgarmHead();
            Console.WriteLine("Der Präfix wurde nicht korrek angegeben\nWeiter mit beliebiger Taste");
            Console.ReadKey();
        }
        public static void ErrorInputPräfix(string NetworkName)
        {
            Console.WriteLine($"Fehler bei der Berechnung des neuen Netzwerks\nNutzer hat bei der Netzadress den falschen Präfix angegeben\n\nProgramm wird beendet");
            Console.ReadKey();

        }
        public static int GetIpModeFromUser()
        {
            //Fordert den Nutzer auf zwischen IPv4 und IPv6 zu wählen und gibt 1 für IPv4 und 2 für IPv6 zurück
            while (true)
            {
                ProgarmHead();
                Console.Write("Bitte wählen sie aus\nIPv4 [1] oder IPv6 [2]: ");
                int TypOfIPAdress;
                if (int.TryParse(Console.ReadLine(), out TypOfIPAdress))
                    if (TypOfIPAdress > 0 && TypOfIPAdress < 3)
                        return TypOfIPAdress;
            }
        }

    }
}
