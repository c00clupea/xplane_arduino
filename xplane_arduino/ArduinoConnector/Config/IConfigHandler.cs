using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConnector.Config
{
    public interface IConfigHandler
    {
        T ReadValue<T>(string key);
        Object UnsafeReadValue(string key);
        bool WriteValue<T>(string key, T value, OVERWRITE_MODE mode);
        void Close();
        void Open();
    }

    public enum OVERWRITE_MODE
    {
        OVERWRITE,
        DO_NOT_OVERWRITE,
        TYPESAFE_OVERWRITE
    }
}
