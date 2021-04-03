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

    [TableName("ClearAction_UserAlertHistory")]
    [PrimaryKey("Id")]
    public class UserAlertHistory
    {

        public int Id { get; set; }

        public int AlertId { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Notes { get; set; }



        /// <summary>
        /// Later we can make / move status to dyanmic tables
        /// </summary>
        [ReadOnlyColumn]
        public string StatusText
        {
            get
            {
                return Components.Util.GetStatusById(StatusId);
            }
      //      set {; }
        }

    }


}
