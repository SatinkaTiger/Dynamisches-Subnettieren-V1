namespace Dynamisches_Subnettieren_V1.IPv4
{
    using Dynamisches_Subnettieren_V1.UserInterface;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    internal class MethodenIPv4
    {
        public static int[] Bits = new int[]
        {
            128,
            64,
            32,
            16,
            8,
            4,
            2,
            1,
            0,
        };
        public static bool CheckOktett(int Input)
        {
            //Checkt auf den richtigen Bereich der Adressierung
            if (Input >= 0 && Input < 256)
                return false;
            else
                return true;
        }
        public static int GetBitToAdress(int Hosts)
        {
            //Ermittelt die Betötigten Bits anhand der angegebenen Hostanzahl
            int Count = 1;
            while (true)
            {
                if (Math.Pow(2, Count) < Hosts)
                    Count++;
                else
                    return Count;
            }
        }
        public static Tuple<int, int> GetTargetOktettAndBit(int TempPräfix)
        {
            //Bestimmt das Oktett, welches Netz-/Host-Adresse trennt
            decimal Temp = TempPräfix / 8;
            return new Tuple<int, int>((int)Math.Floor(Temp), TempPräfix % 8);
        }
        public static int GiveValueOfBit(int Host)
        {
            //Berechnen der Adresse im Oktett durch gesetzte Bits
            return (int)Math.Pow(2, GetBitToAdress(Host));
        }
        public static int GiveStartBitFromOktett(int Oktett)
        {
            //Ermittelt den gesetzten Bit im Oktett
            int Value = Oktett;
            int count = 0;
            for (int i = 0; i < Bits.Length; i++)
            {
                if (Value <=0)
                    break;
                Value -= Bits[i];
                count++;
            }
            return count;
        }
        public static Tuple<int[], int> NetworkJump(Tuple<string, int> NameHostFromNetwork, int[] NullNet)
        {
            //Ermittelt das Neue Netzwerk und gibt es als Tuple zurück
            int[] NextNet = new LinkedList<int>(NullNet).ToArray();
            int TempPräfix = 32 - GetBitToAdress(NameHostFromNetwork.Item2 + 2);
            int TargetOktett = GetTargetOktettAndBit(TempPräfix).Item1;
            NextNet[TargetOktett] += GiveValueOfBit(NameHostFromNetwork.Item2);
            if (CheckOktett(NextNet[TargetOktett]))
            {
                NextNet[TargetOktett - 1] += 1;
                NextNet[TargetOktett] = 0;

            }
            return new Tuple<int[], int>(NextNet, TempPräfix);
        }
        public static List<Tuple<string, int>> NetworksSort(List<Tuple<string, int>> NetworkItem)
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
        static string ConvertAdressArrayToString(int[] Array)
        {
            //Setzt die vier Oktetten aus dem Array zu einer Netzadresse zusammen und gibt diese als String wieder aus
            string TempString = "";
            for (int i = 0; i < Array.Length; i++)
            {
                TempString += $"{Array[i]}";
                if (i < 3)
                    TempString += ".";
            }
            return TempString;
        }
        public static int[] GetBrodcast(Tuple<int[], int> NetAdressPräfix)
        {
            int[] TempNextNet = NetAdressPräfix.Item1;
            for(int i = TempNextNet.Length - 1; i >= 0; i--)
            {
                if (TempNextNet[i] == 0) 
                    TempNextNet[i] = 255;
                 if (TempNextNet[i] > 0)
                {

                    TempNextNet[i] --;
                    break;
                }      
            }
            return TempNextNet;
        }
    }
}
