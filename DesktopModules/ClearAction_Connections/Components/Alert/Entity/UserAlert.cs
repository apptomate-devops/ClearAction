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
using System.Collections.Generic;
using System.Linq;
namespace ClearAction.Modules.Connections
{

    [TableName("ClearAction_UserAlert")]
    [PrimaryKey("Id")]
    [Cacheable("ClearAction_UserAlert", System.Web.Caching.CacheItemPriority.Normal, 20)]
    public class UserAlert
    {

        public int Id { get; set; }

        public string Context { get; set; }

        public int CategoryId { get; set; }

        public int RecipentId { get; set; }

        public int StatusId { get; set; }

        [ReadOnlyColumn]
        public List<UserAlertHistory> AlertHistory
        {
            get
            {
                return new DbController().AlertStatusGet(Id);
            }
        }


        /// <summary>
        /// Later we can make / move status to dyanmic tables
        /// </summary>
        [ReadOnlyColumn]
        public string CurrentStatusText
        {
            get
            {
                return Components.Util.GetStatusById(StatusId);
            }
            set {; }
        }


    }


}
