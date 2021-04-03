using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.Connections.Components
{
    public  static class AlertEnum
    {
        public enum Status
        {
            CREATED = 1,
            DELIVERED = 2,
            VIEWED = 3,
            DISMISSED = 4,
            DELETED = 5

        }

    }
    public static class Util
    {
        public static string GetStatusById(int CurrentStatus)
        {
            switch (CurrentStatus)
            {
                case 1:
                    return "Created";

                case 2:
                    return "Delivered";

                case 3:
                    return "Viewed";

                case 4:
                    return "Dismissed";

                case 5:
                    return "Deleted";

            }
            return "Created";
        }
    }
}