using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoConnector.Exceptions;

namespace ArduinoConnector.Config
{

    public class ConfigRegistry
    {


        private static object syncObj = new Object();
        private static ConfigRegistry instance;
        private bool initialized = false;
        private IConfigHandler _configHandler;
        public IConfigHandler ConfigHandler
        {
            get
            {
                if(!initialized || _configHandler == null)
                {
                    throw new NotInitializedException("configHandler not set or not initilized");
                }
                return _configHandler;
            }
            private set
            {
                this._configHandler = value;
            }
        }
  
        private ConfigRegistry() { }

        public bool InitConfigHandler(IConfigHandler _handler)
        {
            if (initialized)
            {
                return false;
            }
            _configHandler = _handler ?? throw new ArgumentNullException("Set a handler");
            initialized = true;
            return true;
        }


        public static ConfigRegistry Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncObj)
                    {
                        if(instance == null)
                        {
                            instance = new ConfigRegistry();
                        }
                    }
                }
                return instance;
            }
        }

        
    }
}
