namespace Dynamisches_Subnettieren_V1.IPv4
{
    using Dynamisches_Subnettieren_V1.UserInterface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DynamischIPv4
    {
        public static int[] NetAdress = new int[4];
        public static int Präfix;
        public static List<Tuple<string, int>> SubNetNameAndHosts = new();
        public static List<Tuple<int[], int[], int>> SubNetAdress = new();
        static List<Tuple<int[], int>> SubNetAdressNextPräfix = new();
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
            foreach (var Element in TempSubNetNameAndHosts)
                SubNetAdressNextPräfix.Add(NetworkJump(Element, TempNetAdress));
            Präfix = TempPräfix;
            for (int i = 0; i < SubNetAdressNextPräfix.Count; i++)
            {
                int[] NullNet = NetAdress;
                if (i > 0)
                    NullNet = SubNetAdressNextPräfix[i-1].Item1;

            }
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
        static Tuple<int[], int> NetworkJump(Tuple<string, int> NameHostFromNetwork, int[] NullNet)
        {
            //Ermittelt das Neue Netzwerk und gibt es als Tuple zurück
            int[] NextNet = new LinkedList<int>(NullNet).ToArray();
            int TempPräfix = 32 - MethodenIPv4.GetBitToAdress(NameHostFromNetwork.Item2 + 2);
            int TargetOktett = MethodenIPv4.GetTargetOktettAndBit(TempPräfix).Item1;
            NextNet[TargetOktett] += MethodenIPv4.GiveValueOfBit(NameHostFromNetwork.Item2);
            if (MethodenIPv4.CheckOktett(NextNet[TargetOktett]))
            {
                NextNet[TargetOktett - 1] += 1;
                NextNet[TargetOktett] = 0;

            }
            return new Tuple<int[], int>(NextNet, TempPräfix);
        }
    }
}
