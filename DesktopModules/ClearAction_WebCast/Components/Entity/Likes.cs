
using DotNetNuke.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[TableName("WCC_Likes")]
[PrimaryKey("Id", AutoIncrement = true)]
[Scope("PostId")]
[Cacheable("WCC_Likes", CacheItemPriority.Normal)]
public class Likes
{
    public int Id
    {
        get;  set;
    }

   
    public int PostId
    {
        get;set;
    }

    
    public int UserId
    {
        get;set;
    }

   
    public bool Checked
    {
        get;set;
    }

     
    public string ItemType
    {
        get;set;
    }
} 
