using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Models.Common
{
    public class ValidationResult
    {
        public ValidationResult() { }

        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess => !Errors.Any();

        public void AddError(string error)
        {
            var castToList = Errors.ToList();
            castToList.Add(error);
            Errors = castToList;
        }

        public void AddRangeOfErrors(IEnumerable<string> errors)
        {
            var errorsTemp = Errors.ToList();
            errorsTemp.AddRange(errors);
            Errors = errorsTemp;
        }
    }
}
