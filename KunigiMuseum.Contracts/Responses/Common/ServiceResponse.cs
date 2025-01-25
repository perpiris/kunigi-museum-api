namespace KunigiMuseum.Contracts.Responses.Common;

public class ServiceResponse<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public List<string> Errors { get; init; } = [];

    public static ServiceResponse<T> Success(T data) =>
        new() { IsSuccess = true, Data = data };

    public static ServiceResponse<T> Failure(params string[] errors) =>
        new() { IsSuccess = false, Errors = errors.ToList() };
}