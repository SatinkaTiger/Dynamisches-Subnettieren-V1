namespace Dynamisches_Subnettieren_V1
{
    using Dynamisches_Subnettieren_V1.IPv4;
    using Dynamisches_Subnettieren_V1.UserInterface;

    internal class Program
    {
        static void Main(string[] args)
        {
            //User Eingaben
            switch (UserInterfaceEingabe.GetIpModeFromUser())
            {
                case 1:
                    DynamischIPv4.Start();
                    break;
                case 2:
                    Console.WriteLine("IPv6 nicht implementiert qwq");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Fehlerhafte Eingabe\n\nWeiter mit beliebiger Taste");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
