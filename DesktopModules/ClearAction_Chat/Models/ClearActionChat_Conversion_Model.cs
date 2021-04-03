using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction_Chat.Models
{
    public class ClearActionChat_Conversion_Model
    {
        public long ConversionId { get; set; }
        public long ConversionSendToId { get; set; }
        public string SentByUserId { get; set; }
        public string ReceiveByUserId { get; set; }
        public string ConversionText { get; set; }
        public Nullable<bool> IsReadByReceiveByUserId { get; set; }
        public string SentDateTime { get; set; }
        public Nullable<bool> IsAttachment { get; set; }
        public string OriginalFileName { get; set; }
        public string StoreFileName { get; set; }
    }
}