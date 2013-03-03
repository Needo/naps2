/*
    NAPS2 (Not Another PDF Scanner 2)
    http://sourceforge.net/projects/naps2/
    
    Copyright (C) 2009       Pavel Sorejs
    Copyright (C) 2012       Michael Adams
    Copyright (C) 2012-2013  Ben Olden-Cooligan

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Scan.Twain
{
    public class TwainScanDriver : IScanDriver
    {
        public const string DRIVER_NAME = "twain";

        public string DriverName
        {
            get { return DRIVER_NAME; }
        }

        public ScanSettings ScanSettings { get; set; }

        public System.Windows.Forms.IWin32Window DialogParent { get; set; }

        public ScanDevice PromptForDevice()
        {
            if (DialogParent == null)
            {
                throw new InvalidOperationException("IScanDriver.DialogParent must be specified before calling PromptForDevice().");
            }
            var deviceId = TwainApi.SelectDeviceUI();
            var deviceName = deviceId;
            return new ScanDevice(deviceId, deviceName, DRIVER_NAME);
        }

        public List<IScannedImage> Scan()
        {
            if (ScanSettings == null)
            {
                throw new InvalidOperationException("IScanDriver.ScanSettings must be specified before calling Scan().");
            }
            if (DialogParent == null)
            {
                throw new InvalidOperationException("IScanDriver.DialogParent must be specified before calling Scan().");
            }
            var api = new TwainApi(ScanSettings);
            // TODO: Progress
            // TODO: Cool idea: maybe return an IEnumerable (via yield)
            return api.Scan();
        }
    }
}