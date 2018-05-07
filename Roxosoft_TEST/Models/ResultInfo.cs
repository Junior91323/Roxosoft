namespace Roxosoft_TEST.Models
{
    public class ResultInfo
    {
        public ResultInfo(string error = "", string code = "200")
        {
            Code = code;
            Error = error;
        }

        public string Code { get; set; }

        public string Error { get; set; }
    }
}
