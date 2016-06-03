using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.Common.Azure
{
    public class PMapAzureLog
    {
        public static void LogToAzure(string p_type, DateTime p_timestamp, string p_text)
        {
            PMapLog pl = new PMapLog()
            {
                LOG_IP = PMapCommonVars.Instance.IPAddress,
                LOG_TYPE = p_type,
                LOG_TIMESTAMP = p_timestamp.ToString(Global.DATETIMEFORMAT),
                LOG_TEXT = p_text,
                LOG_VALUE = ""
            };
            sendToParse(URL, pl);
        }

    }
}
