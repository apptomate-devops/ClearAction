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


namespace ClearAction.Modules.Dashboard.Components
{

    [TableName("ClearAction_UserFavorite")]
    [PrimaryKey("FavoriteId")]
    public class UserFavoriteInfo
    {

        public int FavoriteId { get; set; }

        public int RelatedUserID { get; set; }

        public int cUserId { get; set; }

        public bool IsFav { get; set; }

      
    }
}
