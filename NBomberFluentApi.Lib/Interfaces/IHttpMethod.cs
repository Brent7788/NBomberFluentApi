namespace NBomberFluentApi.Lib.Interfaces;

public interface IHttpMethod
{
    /// <summary>
    /// Set HTTP Client to GET method
    /// </summary>
    /// <returns></returns>
    public ILoadSimulation GetMethod();
    
    /// <summary>
    /// Set HTTP Client to POST method
    /// </summary>
    /// <returns></returns>
    public IHttpSetBody PostMethod();
    
    /// <summary>
    /// Set HTTP Client to PUT method
    /// </summary>
    /// <returns></returns>
    public IHttpSetBody PutMethod();
}