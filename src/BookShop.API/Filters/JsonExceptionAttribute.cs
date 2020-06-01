﻿using BookShop.API.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookShop.API.Filters
{
    public class JsonExceptionAttribute : TypeFilterAttribute
    {
        public JsonExceptionAttribute() :base(typeof(HttpCustomExceptionFilterImpl))
        {
        }

        private class HttpCustomExceptionFilterImpl : IExceptionFilter
        {
            private readonly IWebHostEnvironment _env;
            private readonly ILogger<HttpCustomExceptionFilterImpl>
            _logger;
            public HttpCustomExceptionFilterImpl(IWebHostEnvironment env,
            ILogger<HttpCustomExceptionFilterImpl> logger)
            {
                _env = env;
                _logger = logger;
            }
            public void OnException(ExceptionContext context)
            {
                var eventId = new EventId(context.Exception.HResult);
                _logger.LogError(eventId,
                context.Exception,
                context.Exception.Message);
                var json = new JsonErrorPayload { EventId = eventId.Id };
                if (_env.IsDevelopment())
                {
                    json.DetailedMessage = context.Exception;
                }
                var exceptionObject = new ObjectResult(json)
                {
                    StatusCode =
                500
                };
                context.Result = exceptionObject; context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
        
    }
}
