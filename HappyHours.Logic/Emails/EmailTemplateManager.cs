using HappyHours.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Emails
{
    public static class EmailTemplateManager
    {
        public static string CreateActivationEmailBody(ActivationEmailParameters parameters)
        {
            return GenerateStringFromTemplate(ConfigHelper.Config.EmailTemplates.ActivationEmail, parameters);
        }

        public static string CreateArrivalEmailBody(ArrivalEmailParameters parameters)
        {
            return GenerateStringFromTemplate(ConfigHelper.Config.EmailTemplates.ArrivalEmail, parameters);
        }

        public static string CreateExitEmailBody(ExitEmailParameters parameters)
        {
            return GenerateStringFromTemplate(ConfigHelper.Config.EmailTemplates.ExitEmail, parameters);
        }

        private static string GenerateStringFromTemplate(string templatePath, IEmailTemplateParameters parameters)
        {
            var fileContent = File.ReadAllText(templatePath);
            var properties = parameters.GetType().GetProperties();
            foreach(var property in properties)
            {
                var param = "{" + property.Name + "}";
                var parameterValue = property.GetValue(parameters).ToString();
                fileContent = fileContent.Replace(param, parameterValue);
            }
            return fileContent;
        }
    }
}
