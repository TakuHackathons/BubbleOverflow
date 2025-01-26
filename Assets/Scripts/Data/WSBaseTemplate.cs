using Newtonsoft.Json;

public class WSBaseTemplate
{
    public ActionNames action;
    public string data;

    public T parseData<T>() {
      return JsonConvert.DeserializeObject<T>(this.data);
    }
}
