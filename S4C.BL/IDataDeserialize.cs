namespace S4C.BL
{
    public interface IDataDeserialize<T>  where T : class, new()
    {

    /// <summary>
    /// Deserializes the specified serialized data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializedData">The serialized data.</param>
    /// <returns></returns>
        T Deserialize(string serializedData);
    }
}
