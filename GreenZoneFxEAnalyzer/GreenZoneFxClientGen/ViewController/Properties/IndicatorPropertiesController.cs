﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class IndicatorPropertiesController : IndicatorPropertiesControllerBase
    {
        public IndicatorPropertiesController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
    }
}
