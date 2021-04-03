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
 
    [TableName("UserRelationships")]
    [PrimaryKey("UserRelationshipID")]
    public class UserRelationships
    {

        public int UserRelationshipID { get; set; }

        public int UserID { get; set; }

        public int RelatedUserID { get; set; }

        public int RelationshipID { get; set; }

        public int Status { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public int LastModifiedByUserID { get; set; }

        public DateTime LastModifiedOnDate { get; set; }


        [ReadOnlyColumn]
        public string Requestdateformat
        {

            get
            {
                return Helper.ToRelativeDateString(CreatedOnDate, true);
            }
        }




    }
}
