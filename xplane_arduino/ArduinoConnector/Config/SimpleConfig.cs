using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConnector.Config
{
    public class SimpleConfig : IConfigHandler
    {

        private class ConfigHolder
        {
            public Type Ctype { get; set; }
            public Object Value { get; set; }
        }

        Dictionary<string, ConfigHolder> _masterDict;



        public SimpleConfig()
        {
            _masterDict = new Dictionary<string, ConfigHolder>();
            FillBasicConfig();
        }

        private void FillBasicConfig()
        {
            WriteValue("addressLen", 2);
        }

        public void Close()
        {

        }

        public void Open()
        {

        }

        public T ReadValue<T>(string key)
        {
            if (!_masterDict.ContainsKey(key))
            {
                throw new KeyNotFoundException(String.Format("Key {0} not found in config", key));
            }
            ConfigHolder _tmpconf = _masterDict[key];
            if (typeof(T) != _tmpconf.Ctype)
            {
                throw new InvalidCastException(String.Format("Key {0} stores a value with type {1} but expected type is {2}", key, _tmpconf.Ctype.ToString(), typeof(T).ToString()));
            }
            return (T)_tmpconf.Value;
        }

        public Object UnsafeReadValue(string key)
        {
            if (!_masterDict.ContainsKey(key))
            {
                return null;
            }
            return _masterDict[key];
        }

        public bool WriteValue<T>(string key, T _value, OVERWRITE_MODE mode = OVERWRITE_MODE.OVERWRITE)
        {
            if (mode.Equals(OVERWRITE_MODE.DO_NOT_OVERWRITE) && _masterDict.ContainsKey(key))
            {
                return false;
            }
            if (mode.Equals(OVERWRITE_MODE.TYPESAFE_OVERWRITE) && _masterDict.ContainsKey(key) && _masterDict[key].Ctype != typeof(T))
            {
                return false;
            }
            _masterDict[key] = new ConfigHolder { Ctype = typeof(T), Value = _value };
            return true;
        }


    }



}
