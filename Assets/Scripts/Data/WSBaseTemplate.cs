using Newtonsoft.Json;

public class WSBaseTemplate
{
    public string action;
    public object data;

    public T parseData<T>() {
      return JsonConvert.DeserializeObject<T>(this.data.ToString());
    }
}
