using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoConnector.Communication;
using ArduinoConnector.Config;

namespace ArduinoConnector
{
    public class ArduinoDevice
    {
        public Communication.ICommunication Channel { get; private set; }
        public DeviceAddress Adress { get; private set; }
        public ArdType DeviceType { get; set; }

        //protected ConcurrentQueue<Message> _outGoingMessages;




        private ArduinoDevice()
        {

        }

        public ArduinoDevice(Communication.ICommunication _channel, DeviceAddress _address, ArdType _deviceType = ArdType.UNSPEC) :this()
        {
            Channel = _channel ?? throw new ArgumentNullException("_channel", "Need some channel to communicate");
            Adress = _address ?? throw new ArgumentNullException("_address", "Need some unique ID");
            DeviceType = _deviceType;
            if (_address.realAdress.Length != ConfigRegistry.Instance.ConfigHandler.ReadValue<int>("addressLen"))
            {
                throw new ArgumentOutOfRangeException("_address", String.Format("Adress should be {0} bytes long", ConfigRegistry.Instance.ConfigHandler.ReadValue<int>("addressLen")));
            }


        }

        public bool Connected { get; }

        public bool Connect()
        {
            return true;
        }

        public bool Disconnect()
        {
            return true;
        }

        bool TestAlive()
        {
            return true;
        }

        bool SendCommand()
        {
            return true;
        }

        int OnGetCommand()
        {
            return 0;
        }
    }

    
    

    public enum ArdType
    {
        UNO_R3, NANO, MEGA, UNSPEC

    }
}
