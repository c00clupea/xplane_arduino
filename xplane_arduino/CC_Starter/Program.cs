using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            var iface = new ExtPlaneNet.ExtPlaneInterface();
            iface.Connect();

            //iface.Subscribe<float[]>("x737/systems/landinggear/noseGearAnn1_red");

            // this does the same, but specified an accuracy of 0.3 and a delegate method that should be called automatically whenever the dataref value changes
            iface.Subscribe<float>("x737/systems/landinggear/noseGearAnn1_red", 0.3f, (dataRef) =>
            {
                Console.WriteLine(string.Format("Dataref changed: {0} = {1}", dataRef.Name, string.Join(",", dataRef.Value)));
            });
            iface.Subscribe<float>("x737/systems/landinggear/noseGearAnn1_green", 0.3f, (dataRef) =>
            {
                Console.WriteLine(string.Format("Dataref changed: {0} = {1}", dataRef.Name, string.Join(",", dataRef.Value)));
            });

            iface.Subscribe<int>("x737/systems/electricMeter/DCVolts", 0.3f, (dataRef) =>
            {
                Console.WriteLine(string.Format("Dataref changed: {0} = {1}", dataRef.Name, string.Join(",", dataRef.Value)));
            });

            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

        }
    }
}
