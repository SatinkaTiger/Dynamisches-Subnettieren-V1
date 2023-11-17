namespace Dynamisches_Subnettieren_V1.IPv4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Dynamisches_Subnettieren_V1.UserInterface;

    internal class DynamischIPv4
    {
        public static int[] NetAdress = new int[4];
        public static int Präfix;
        public static List<Tuple<int[]>> SubNetAdress = new();
        public static List<Tuple<int[]>> SubNetBrodcarst = new();
        public static List<Tuple<string, int>> SubNetNameAndHosts = new();
        public static void Start()
        {
            //Usereingabe
            int[] TempNetAdress = UserInterfaceEingabe.GetNetAdressFromUser();
            int TempPräfix = UserInterfaceEingabe.GetPräfixFormUser();
            List<Tuple<string, int>> TempSubNetNameAndHosts = new();
            while (true)
            {
                TempSubNetNameAndHosts.Add(UserInterfaceEingabe.GetSubnetDataFormUser(TempPräfix));
                Console.Write("Möchten Sie ein weiteres Teilnetzwerk eingeben? [j/n]: ");
                if (!(Console.Read() == 'j'))
                    break;
            }
            //Verarbeitung
            TempSubNetNameAndHosts = NetworksSort(TempSubNetNameAndHosts);
            SubNetAdress.Add(new Tuple<int[]>(TempNetAdress));
            foreach (var Element in TempSubNetNameAndHosts)
                SubNetAdress.Add(NetworkJump(Element, TempNetAdress));
            Präfix = TempPräfix;
        }
        static List<Tuple<string, int>> NetworksSort(List<Tuple<string, int>> NetworkItem)
        {
            //Die Teilnetzwerke werden absteigend Sortiert
            for (int i = 0; i < NetworkItem.Count; i++)
            {
                string TempString;
                int TempInt;
                for (int j = i; j < NetworkItem.Count; j++)
                {
                    TempString = NetworkItem[j].Item1;
                    TempInt = NetworkItem[j].Item2;
                    if (NetworkItem[i].Item2 < NetworkItem[j].Item2)
                    {
                        NetworkItem[j] = new Tuple<string, int>(NetworkItem[i].Item1, NetworkItem[i].Item2);
                        NetworkItem[i] = new Tuple<string, int>(TempString, TempInt);
                    }
                }
            }
            return NetworkItem;
        }
        static Tuple<int[]> NetworkJump(Tuple<string, int> NameHostFromNetwork, int[] NullNet)
        {
            //Ermittelt das Neue Netzwerk und gibt es als Tuple zurück
            int[] TempNet = new LinkedList<int>(NullNet).ToArray();
            int[] NextNet = TempNet;
            int TempPräfix = 32 - MethodenIPv4.GetBitToAdress(NameHostFromNetwork.Item2);
            int TargetOktett = MethodenIPv4.GetTargetOktettAndBit(TempPräfix).Item1;
            int TempOktett = NextNet[TargetOktett];
            NextNet[TargetOktett] = MethodenIPv4.GiveValueOfBit(MethodenIPv4.GiveStartBitFromOktett(TempOktett), TempOktett) + 1;
            if (UserInterfaceEingabe.CheckOktett(NextNet[TargetOktett]))
            {
                NextNet[TargetOktett - 1] += 1;
                NextNet[TargetOktett] = 0;

            }
            return new Tuple<int[]>(NextNet);
        }
    }
}
