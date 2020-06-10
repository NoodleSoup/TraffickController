namespace TraffickController.TrafficLight
{
    public interface IValidateJson
    {
        (bool, string) Validate(string json);
    }
}
