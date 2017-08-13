using System;
using ArduinoConnector;
using ArduinoConnector.Communication;
using ArduinoConnector.Communication.Mock;
using ArduinoConnector.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SomeTestScenarios
{
    [TestClass]
    public class CommunicationTests
    {
        [TestMethod]
        public void BasicInitTest()
        {
            ArduinoConnector.ArduinoConnector connector = new ArduinoConnector.ArduinoConnector();
            ICommunication comm = new MockCommunicator();
            ArduinoDevice dev = new ArduinoDevice(comm, new DeviceAddress("ardu1",new byte[] { 0x00,0x01}),ArdType.UNO_R3);
            connector.AppendDevice(dev);
            connector.RemoveDevice(dev.Adress.realAdress);
        }
    }
}
