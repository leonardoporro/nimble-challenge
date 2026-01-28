namespace Calculator.BusinessLogic.Serialization;

public interface IListSerializer
{
    List<double> Deserialize(string serializedNumbers); 
}
