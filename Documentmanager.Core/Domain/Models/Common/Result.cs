using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Models.Common
{
    public class Result<T>
    {
        public Result() { }

        private Result(T successData)
        {
            SuccessData = successData;
        }

        public T SuccessData { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess => !Errors.Any();

        public void AddError(string error)
        {
            var castToList = Errors.ToList();
            castToList.Add(error);
            Errors = castToList;
        }

        public void AddSuccessData(T t)
        {
            if (Errors.Any())
            {
                throw new Exception("Cannot add success data with errors.");
            }
            SuccessData = t;
        }
    }
}
