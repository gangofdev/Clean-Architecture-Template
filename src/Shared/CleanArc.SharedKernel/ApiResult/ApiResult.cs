﻿using System.Diagnostics;
using CleanArc.SharedKernel.Extensions;

namespace CleanArc.SharedKernel.ApiResult;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }

    public string Message { get; set; }
    public string RequestId { get;  }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
        RequestId = Activity.Current?.TraceId.ToHexString() ?? string.Empty;
    }

    
}

public class ApiResult<TData> : ApiResult
    where TData : class
{
    public TData Data { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }

    
}