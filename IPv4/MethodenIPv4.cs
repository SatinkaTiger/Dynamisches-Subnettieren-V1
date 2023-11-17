namespace Dynamisches_Subnettieren_V1.IPv4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public static int GetBitToAdress(int Hosts)
        {
            //Ermittelt die Betötigten Bits anhand der angegebenen Hostanzahl
            int Count = 1;
            while (true)
            {
                if (Math.Pow(2, Count) < Hosts + 2)
                    Count++;
                else
                    return Count;
            }
        }
        public static Tuple<int, int> GetTargetOktettAndBit(int TempPräfix)
        {
            //Bestimmt das Oktett, welches Netz-/Host-Adresse trennt
            decimal Temp = TempPräfix / 8;
            if (TempPräfix % 8 > 0)
                Temp++;
            return new Tuple<int, int>((int)Math.Floor(Temp), TempPräfix % 8);
        }
        public static int GiveValueOfBit( int StartBit,int SetValue)
        {
            //Berechnen der Adresse im Oktett durch gesetzte Bits
            int Value = SetValue;
                for (int i = StartBit; i < Bits.Length; i++)
                    Value += Bits[i];
            return Value;
        }
        public static int GiveStartBitFromOktett(int Oktett)
        {
            //Ermittelt den gesetzten Bit im Oktett
            int Value = Oktett;
            int count = 0;
            for (int i = 0; i < Bits.Length; i++)
            {
                if (Value <= 0)
                    break;
                Value -= Bits[i];
                count++;
            }
            return count;
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
    }
}
