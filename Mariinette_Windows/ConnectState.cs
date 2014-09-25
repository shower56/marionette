using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mariinette2
{
    interface ConnectState
    {
        void SerialConnect();
        void SerialConnectOFF();
        void BluetoothConnect();
        void BluetoothConnectOFF();

    }
}
