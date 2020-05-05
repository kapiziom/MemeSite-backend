using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace MemeSite.Data.Models.Common
{
    public class Result : Result<object>
    {
        public Result() : base(null) { }
    }

    public class Result<T> where T : class
    {
        public Result(ValidationResult validationResult)
        {
            Errors = validationResult?.Errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>();
        }

        [JsonIgnore]
        public T Value { get; set; }
        public IList<string> Errors { get; set; }
        public bool Succeeded { get { return Errors.Count == 0; } }
    
    }


}