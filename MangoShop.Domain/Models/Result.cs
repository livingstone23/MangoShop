


namespace MangoShop.Domain.Models;



/// <summary>
/// Represents the result of an operation
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{

    /// <summary>
    /// Field for present the value of the result
    /// </summary>
    /// <value></value>
    public T Value { get;}

    /// <summary>
    /// Field for present the error message of the result if the operation fails
    /// </summary>
    /// <value></value>
    public string Error { get; }

    /// <summary>
    /// Field for present if the operation was successful
    /// </summary>
    public bool IsSuccess => Error == null;

    /// <summary>
    /// Field for present if the operation was failed
    /// </summary>
    /// <param name="value"></param>
    /// <param name="error"></param> <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="error"></param>
    protected Result(T value, string error)
    {
        Value = value;
        Error = error;
    }

    /// <summary>
    /// Factory method for create a successful result
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Result<T> Success(T value) => new Result<T>(value, null);

    /// <summary>
    /// Factory method for create a failed result
    /// </summary>
    /// <param name="error"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Result<T> Failure(string error) => new Result<T>(default(T), error);


}