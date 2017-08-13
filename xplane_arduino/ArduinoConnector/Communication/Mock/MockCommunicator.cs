using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConnector.Communication.Mock
{
    public class MockCommunicator : ICommunication
    {
        public ECommunicationState CommunicationState => throw new NotImplementedException();

        public ECommunicationState Connect(ICommunicationConfig config)
        {
            throw new NotImplementedException();
        }

        public ECommunicationState DisConnect()
        {
            throw new NotImplementedException();
        }

        public CommunicationResult HandShake(HandshakeType handShakeType)
        {
            throw new NotImplementedException();
        }

        public CommunicationResult SendCommand(CommunicationSafeGuard overRideSafeGuard = CommunicationSafeGuard.USEDEFAULT)
        {
            throw new NotImplementedException();
        }
    }
}
