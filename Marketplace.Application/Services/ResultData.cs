using System.Net;

namespace Marketplace.Application.Services
{
    public interface IResultData
    {
        public bool Succeeded { get; }
        public string? Message { get; }
        public HttpStatusCode StatusCode { get; }

        public TData? GetData<TData>() where TData : class;
    }

    public class ResultData : IResultData
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ResultData(bool succeeded, string? message, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            Message = message;
            StatusCode = statusCode;
        }

        public virtual T? GetData<T>() where T : class
            => null;

        /// <summary>Retorna um status code 200 (Ok).</summary>
        public static ResultData Ok()
            => new(true, null, HttpStatusCode.OK);

        /// <summary>Retorna um status code 200 (Ok).</summary>
        public static ResultData<T> Ok<T>(T? data) where T : class
            => new(true, null, HttpStatusCode.OK, data);

        /// <summary>Retorna um status code 400 (Bad Request).</summary>
        public static ResultData Error(string? errorMessage)
            => new(false, errorMessage, HttpStatusCode.BadRequest);

        /// <summary>Retorna um status code 400 (Bad Request).</summary>
        public static ResultData<T> Error<T>(T? data) where T : class
            => new(false, null, HttpStatusCode.BadRequest, data);

        /// <summary>Retorna um status code 500 (Internal Server Error).</summary>
        public static ResultData InternalError(string? errorMessage)
            => new(false, errorMessage, HttpStatusCode.InternalServerError);

        /// <summary>Retorna um status code 400 (Bad Request).</summary>
        public static ResultData<T> InternalError<T>(T? data) where T : class
            => new(false, null, HttpStatusCode.InternalServerError, data);
    }

    /// <summary>Armazena os retornos de dados das classes de serviços.</summary>
    public class ResultData<T> : ResultData where T : class
    {   
        public T? Data { get; set; }        

        public ResultData(bool succeeded, string? message, HttpStatusCode statusCode, T? data) 
            : base(succeeded, message, statusCode)
        {
            Data = data;
        }

        public override TData? GetData<TData>() where TData : class
        {
            if (Data is TData tdata)
                return tdata;

            return null;
        }        
    }    
}