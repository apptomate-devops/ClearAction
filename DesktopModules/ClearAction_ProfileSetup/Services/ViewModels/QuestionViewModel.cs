/*
' Copyright (c) 2017 Ajit Jha
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using ClearAction.Modules.ProfileClearAction_ProfileSetup.Components;
using Newtonsoft.Json;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Services.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QuestionViewModel
    {

        
        public QuestionViewModel(Question t)
        {
            QuestionID = t.QuestionID;
            QuestionText = t.QuestionText;
            Size = t.Size;
            Hint = t.Hint;
            QuestionOrder = t.QuestionOrder;
            IsRequire = t.IsRequire;
            IsActive = t.IsActive;
        }

        public QuestionViewModel(Question t, string editUrl)
        {
            QuestionID = t.QuestionID;
            QuestionText = t.QuestionText;
            Size = t.Size;
            Hint = t.Hint;
            QuestionOrder = t.QuestionOrder;
            IsRequire = t.IsRequire;
            IsActive = t.IsActive;
        }


        public QuestionViewModel() { }

        [JsonProperty("QuestionID")]
        public int QuestionID { get; set; }

        [JsonProperty("QuestionText")]
        public string QuestionText { get; set; }

        [JsonProperty("Size")]
        public int Size { get; set; }

        [JsonProperty("Hint")]
        public string Hint { get; set; }

        [JsonProperty("QuestionOrder")]
        public int QuestionOrder { get; set; }

        [JsonProperty("IsRequire")]
        public bool IsRequire { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }
    }
}