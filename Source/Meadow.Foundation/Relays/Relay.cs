using Meadow.Hardware;
using Meadow.Peripherals.Relays;

namespace Meadow.Foundation.Relays
{
    /// <summary>
    /// Electrical switch (usually mechanical) that switches on an isolated circuit.
    /// </summary>
    public class Relay : IRelay
    {
        /// <summary>
        /// Returns digital output port
        /// </summary>
        public DigitalOutputPort DigitalOut { get; protected set; }

        /// <summary>
        /// Returns type of relay.
        /// </summary>
        public RelayType Type { get; protected set; }

        /// <summary>
        /// Whether or not the relay is on. Setting this property will turn it on or off.
        /// </summary>
        public bool IsOn {
            get => _isOn; 
            set
            {
                // if turning on,
                _isOn = value;
                DigitalOut.State = _isOn ? _onValue : !_onValue;
                
            }
        } protected bool _isOn = false;
        protected bool _onValue = true;

        /// <summary>
        /// Creates a new Relay on an IDigitalOutputPort. Allows you 
        /// to use any peripheral that exposes ports that conform to the
        /// IDigitalOutputPort, such as the MCP23008.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="type"></param>
        public Relay(DigitalOutputPort port, RelayType type = RelayType.NormallyOpen)
        {
            // if it's normally closed, we have to invert the "on" value
            Type = type;
            if (Type == RelayType.NormallyClosed)
            {
                _onValue = false;
            }

            DigitalOut = port;
        }

        /// <summary>
        /// Creates a new Relay on an IDigitalOutputPort.
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="type"></param>
        public Relay(IDigitalPin pin, RelayType type = RelayType.NormallyOpen)
        {
            // if it's normally closed, we have to invert the "on" value
            Type = type;
            if (Type == RelayType.NormallyClosed)
            {
                _onValue = false;
            }

            // create a digital output port shim
            DigitalOut = new DigitalOutputPort(pin, !_onValue);
        }

        /// <summary>
        /// Toggles the relay on or off.
        /// </summary>
        public void Toggle()
        {
            IsOn = !IsOn;
        }
    }
}