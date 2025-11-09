using Newtonsoft.Json;

namespace BartugWeb.WebApi.Middlewares.Utils;

public class ErrorStatusCode
{
    public int StatusCode { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}