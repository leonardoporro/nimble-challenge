namespace Calculator.BusinessLogic.Serialization;

public interface IListSerializer
{
    public List<double> Deserialize(string serializedNumbers);
}
