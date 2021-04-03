/*
' Copyright (c) 2017 Ajit jha
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using DotNetNuke.ComponentModel.DataAnnotations;
using System;

namespace ClearAction.Modules.Connections
{

    [TableName("CoreMessaging_Messages")]
    [PrimaryKey("MessageID")]
    public class MessageInfo
    {
        public int MessageID { get; set; }
        public int PortalID { get; set; }

        public int NotificationTypeID { get; set; }

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        public int ConversationID { get; set; }

        public bool ReplyAllAllowed { get; set; }

        public int SenderUserID { get; set; }

        public DateTime ExpirationDate { get; set; }
        public string Context { get; set; }

        public bool IncludeDismissAction { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public int LastModifiedByUserID { get; set; }

        public int ToAccount { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        [ReadOnlyColumn]
        public int UserId { get; set; }
        [ReadOnlyColumn]
        public bool Read { get; set; }

        [ReadOnlyColumn]
        public bool Archived { get; set; }

        [ReadOnlyColumn]
        public bool EmailSent { get; set; }

        [ReadOnlyColumn]
        public DateTime EmailSentDate { get; set; }

        [ReadOnlyColumn]
        public string DisplayDateTime
        {
            get
            {
                return Helper.ToRelativeDateString(LastModifiedOnDate, true);
            }
        }

    }
}
