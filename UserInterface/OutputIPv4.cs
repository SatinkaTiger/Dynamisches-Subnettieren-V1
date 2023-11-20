namespace Dynamisches_Subnettieren_V1.UserInterface
{
    using Dynamisches_Subnettieren_V1.IPv4;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class OutputIPv4
    {

        public static void Start()
        {
            InputIPv4.ProgarmHead();
            Console.WriteLine($"Netzadresse: {MethodenIPv4.ConvertAdressArrayToString(DynamischIPv4.NetAdress)}\n");
            foreach (var Element in DynamischIPv4.SubNetAdress)
            {
                foreach (var Item in DynamischIPv4.SubNetNameAndHosts)
                {
                    Console.WriteLine($"{Item.Item1}\nSubnetzardesse: {MethodenIPv4.ConvertAdressArrayToString(Element.Item1)}\nBrodcast: {MethodenIPv4.ConvertAdressArrayToString(Element.Item2)}\nPräfix: {Element.Item3}\n");
                    break;
                }
            }

        }
    }
}
