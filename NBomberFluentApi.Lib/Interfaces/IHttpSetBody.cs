namespace NBomberFluentApi.Lib.Interfaces;

public interface IHttpSetBody
{
    /// <summary>
    /// Set HTTP Client body/content
    /// Note* That is body will be converted to JSON Content
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public ILoadSimulation SetBody(object obj);

    /// <summary>
    /// Set HTTP Client body/content with generic body/data.
    /// Example:
    ///     HttpBody = new
    ///     {
    ///       testArgOne = "testing",
    ///       testArgTwo = 7
    ///     };
    /// </summary>
    /// <returns></returns>
    public ILoadSimulation UseGenericBody();

    /// <summary>
    /// Set HTTP Client body/content with body.
    /// Example:
    ///     HttpBody = new
    ///     {
    ///     };
    /// </summary>
    /// <returns></returns>
    public ILoadSimulation UseEmptyBody();
}