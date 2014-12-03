/*
' Copyright (c) 2014 Christoc.com Software Solutions
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
' and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
' SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
' ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
' OR OTHER DEALINGS IN THE SOFTWARE.
' 
*/

using System.IO.Ports;
using System;

namespace Christoc.Modules.HomeController.Components
{

    public class Communication
    {
        public static void DeviceOn(string insteonAddress)
        {
            var serialPlm = new SerialPort("COM4", 19200, Parity.None, 8, StopBits.One) { Handshake = Handshake.None };
            var sdata = new Byte[8];
            string[] a = insteonAddress.Split('.');
            sdata[0] = 2;
            sdata[1] = 98;
            /*split up the address of the device */
            sdata[2] = Convert.ToByte(Convert.ToInt32(a[0], 16));
            sdata[3] = Convert.ToByte(Convert.ToInt32(a[1], 16));
            sdata[4] = Convert.ToByte(Convert.ToInt32(a[2], 16));

            sdata[5] = 15;
            sdata[6] = 17;
            sdata[7] = 255;
            serialPlm.Open();
            serialPlm.Write(sdata, 0, 8);
            serialPlm.Close();

        }

        public static void DeviceOff(string insteonAddress)
        {
            //TODO: put serial info into Module Settings
            var serialPlm = new SerialPort("COM4", 19200, Parity.None, 8, StopBits.One) { Handshake = Handshake.None };

            var sdata = new Byte[8];
            string[] a = insteonAddress.Split('.');
            sdata[0] = 2;
            sdata[1] = 98;
            /*split up the address of the device */
            sdata[2] = Convert.ToByte(Convert.ToInt32(a[0], 16));
            sdata[3] = Convert.ToByte(Convert.ToInt32(a[1], 16));
            sdata[4] = Convert.ToByte(Convert.ToInt32(a[2], 16));

            sdata[5] = 15;
            sdata[6] = 19;
            sdata[7] = 255;
            serialPlm.Open();
            serialPlm.Write(sdata, 0, 8);
            serialPlm.Close();
        }
    }
}