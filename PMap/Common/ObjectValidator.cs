using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using System.Xml.Serialization;

    public static class ObjectValidator
    {

        public class ValidationError
        {
            public string Field { get; set; }
            public string Message { get; set; }
        }


        public static List<ValidationError> ValidateObject(object p_obj)
        {
            List<ValidationError> Errors = new List<ValidationError>();

            //Annotációk alapján levalidáljuk az input tartalmát
            var context = new ValidationContext(p_obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(p_obj, context, results);
            if (!isValid)
            {

                foreach (var validationResult in results)
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        ValidationError err = new ValidationError()
                        {
                            Field = p_obj.GetType().Name + "." + memberName,
                            Message = validationResult.ErrorMessage
                        };

                        Errors.Add(err);
                    }
                }
            }
            return Errors;
        }
    }
}
