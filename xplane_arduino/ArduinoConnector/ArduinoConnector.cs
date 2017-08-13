using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoConnector.Communication;
using ArduinoConnector.Config;
using ArduinoConnector.Utils;
using NLog;

namespace ArduinoConnector
{
    public class ArduinoConnector
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); 

        private System.Collections.Concurrent.ConcurrentDictionary<byte[], ArduinoDevice> devices;
        private System.Collections.Concurrent.ConcurrentDictionary<string, byte[]> humanReadableAdresses;

        public ArduinoConnector(IConfigHandler _ConfigHandler)
        {
            ConfigRegistry.Instance.InitConfigHandler(_ConfigHandler);
            devices = new System.Collections.Concurrent.ConcurrentDictionary<byte[], ArduinoDevice>();
            humanReadableAdresses = new System.Collections.Concurrent.ConcurrentDictionary<string, byte[]>();
        }

        public ArduinoConnector() :this(new SimpleConfig())
        {

        }

        public bool AppendDevice(ArduinoDevice device)
        {
            if (device == null)
            {
                Exception ex = new ArgumentNullException("Need some device to append");
                logger.Error(ex);
                throw ex;
            }
            if (devices.ContainsKey(device.Adress.realAdress))
            {
                logger.Info("Device with address {0} is already appended", StringUtils.ByteArrayToString(device.Adress.realAdress));
                return false;
            }
            if (humanReadableAdresses.ContainsKey(device.Adress.humanReadableAdress))
            {
                logger.Info("Device with human readable address {0} is already appended", device.Adress.humanReadableAdress);
                return false;
            }
            humanReadableAdresses[device.Adress.humanReadableAdress] = device.Adress.realAdress;
            devices[device.Adress.realAdress] = device;
            logger.Trace("Device {1} with adress {0} appended", StringUtils.ByteArrayToString(device.Adress.realAdress),device.Adress.humanReadableAdress);
            return true;

        }

        private bool _ExistDevice(byte[] address)
        {
            return devices.ContainsKey(address);
        }

        private bool _ExistDevice(string address)
        {
            if (_existHumanReadAble(address))
            {
                return _ExistDevice(humanReadableAdresses[address]);
            }
            return false;
        }

        private byte[] _InsecMatchHraToByte(string hra)
        {
            return humanReadableAdresses[hra];
        }

        private bool _existHumanReadAble(string hra)
        {
            return humanReadableAdresses.ContainsKey(hra);
        }

        public bool Connect(string hra)
        {
            if (_existHumanReadAble(hra))
            {
                return Connect(_InsecMatchHraToByte(hra));
            }
            logger.Error("Can not connect to non existing Device {0}", hra);
            return false;
        }

        public bool Connect(byte[] address)
        {
            if (!_ExistDevice(address))
            {
                logger.Error("Can not connect to non existing Device at {0}", StringUtils.ByteArrayToString(address));
                return false;
            }
            ArduinoDevice _dev = devices[address];
            return _dev.Connect();

        }

        public bool DisConnect(string hra)
        {
            if (_existHumanReadAble(hra))
            {
                return DisConnect(_InsecMatchHraToByte(hra));
            }
            logger.Error("Can not disconnect from non existing Device {0}", hra);
            return false;
        }

        public bool DisConnect(byte[] address)
        {
            if (!_ExistDevice(address))
            {
                logger.Error("Can not disconnect from non existing Device at {0}", StringUtils.ByteArrayToString(address));
                return false;
            }
            ArduinoDevice _dev = devices[address];
            return _dev.Disconnect();

        }

        public bool RemoveDevice(string adress)
        {
            if (!humanReadableAdresses.ContainsKey(adress))
            {
                logger.Error("Device with human readable address {0} is not known", adress);
                return false;
            }
            return RemoveDevice(humanReadableAdresses[adress]);
        }

        public bool RemoveDevice(byte[] adress)
        {
            if (!devices.ContainsKey(adress)){
                logger.Error("Device with address {0} is not appended", StringUtils.ByteArrayToString(adress));
                return false;
            }
            ArduinoDevice dev = devices[adress];
            if (dev.Connected)
            {
                logger.Error("Device with address {0} is already connected", StringUtils.ByteArrayToString(adress));
                return false;

            }
            byte[] res1;
            humanReadableAdresses.TryRemove(dev.Adress.humanReadableAdress, out res1);
            bool res = devices.TryRemove(adress, out dev);
            if (res)
            {
                
                logger.Info("Device {1} with address {0} removed", StringUtils.ByteArrayToString(adress), dev.Adress.humanReadableAdress);
            }
            else
            {
                logger.Error("Device {1} with address {0} not removed", StringUtils.ByteArrayToString(adress), dev.Adress.humanReadableAdress);
            }
            return res;
        }



    }
}
