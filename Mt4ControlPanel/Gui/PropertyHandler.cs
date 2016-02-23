using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mt4ControlPanelGui
{
    public class PropertyHandler
    {
        #region Set Properties
        public static void SetProperties(PropertyInfo[] fromFields,
                                         object fromRecord,
                                         object toRecord)
        {
            PropertyInfo fromField = null;

            if (fromFields == null) return;

            for (int f = 0; f < fromFields.Length; f++)
            {

                fromField = (PropertyInfo)fromFields[f];

                if (fromField.CanRead && fromField.CanWrite)
                {
                    fromField.SetValue(toRecord,
                                       fromField.GetValue(fromRecord, null),
                                       null);
                }
            }

        }
        #endregion


        public static DateTime FromUnixTime(int unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static int ToUnixTime(DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)(time - epoch).TotalSeconds;
        }
    }
}
