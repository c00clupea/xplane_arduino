using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConnector.Communication
{
    public class DeviceAddress
    {
        public byte[] realAdress { get; private set; }
        public String humanReadableAdress { get; private set; }
        private DeviceAddress() { }
        public DeviceAddress(String _humanReadable, byte[] _real)
        {
            realAdress = _real;
            humanReadableAdress = _humanReadable ?? throw new ArgumentNullException("Need some human readable adress");
        }
    }
}
