using static MyVillas_Utility.SD;

namespace MyVillas_Web.Models
{
    public class APIRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public string url { get; set; }
        public Object Data {  get; set; }
    }
}
