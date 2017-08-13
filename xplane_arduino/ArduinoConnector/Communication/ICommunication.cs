using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoConnector.Communication
{
    public interface ICommunication
    {
        ECommunicationState Connect(ICommunicationConfig config);
        ECommunicationState DisConnect();
        ECommunicationState CommunicationState { get; }
        CommunicationResult SendCommand(CommunicationSafeGuard overRideSafeGuard = CommunicationSafeGuard.USEDEFAULT);
        CommunicationResult HandShake(HandshakeType handShakeType);
    }

    public interface ICommunicationConfig
    {
        CommunicationSafeGuard DefaultSafeGuard { get; }
    }

    public enum HandshakeType
    {
        HEARTBEAT,
        READYTORECV,
        CONFIG
    }

    public enum CommunicationResult
    {
        SUCCESS,
        ERROR,
        PENDINGFORRESULT
    }

    public enum CommunicationSafeGuard
    {
        ACK,
        NAK,
        FIREANDFORGET,
        USEDEFAULT
    }

    public enum ECommunicationState
    {
        CONNECTED,
        DISCONNECTED,
        UNKNOWN,
        ERROR
    }
}
