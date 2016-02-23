using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    [GreenRmi]
    public interface IIndicatorPanelController : ITabController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TabPageController TabPage1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TabPageController TabPage2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TabPageController TabPage3
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IIndicatorPropertiesController IndicatorRuntimePanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        BufferedPropertyGridController IndexesPrgrd
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        BufferedPropertyGridController LevelsPrgrd
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController Reset1Button
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController Reset2Button
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController Reset3Button
        {
            get;
            set;
        }
    }
}
