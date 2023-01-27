using FluentValidation;
using PiecykPolHurt.Model.Commands;
using System.Xml;

namespace PiecykPolHurt.Validation.Validators
{
    public class UpdateReportDefinitionCommandValidator : AbstractValidator<UpdateReportDefinitionCommand>
    {
        public UpdateReportDefinitionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Version).NotEmpty();

            RuleFor(x => x.XmlDefinition)
                .NotEmpty()
                .Must(x =>
                {
                    try
                    {
                        var doc = new XmlDocument();
                        doc.LoadXml(x);
                        var query = doc.GetElementsByTagName("query");
                        if (query.Count == 0)
                        {
                            return false;
                        }
                        var queryParams = doc.GetElementsByTagName("param");
                        for (int i = 0; i < queryParams.Count; i++)
                        {
                            var queryParam = queryParams[i];
                            var hasName = false;
                            var hasType = false;
                            var hasCaption = false;
                            var hasCondition = false;
                            for (int j = 0; j < queryParam?.ChildNodes.Count; j++)
                            {
                                //<name> node
                                if (queryParam?.ChildNodes[j]?.Name == "name")
                                {
                                    hasName = true;
                                }
                                //<type> node
                                else if (queryParam?.ChildNodes[j]?.Name == "type")
                                {
                                    hasType = true;
                                }
                                //<condition> node
                                else if (queryParam?.ChildNodes[j]?.Name == "condition")
                                {
                                    hasCondition = true;
                                }
                                //<caption> node
                                else if (queryParam?.ChildNodes[j]?.Name == "caption")
                                {
                                    hasCaption = true;
                                }
                            }
                            if (!(hasCaption && hasCondition && hasName && hasType)) { return false; }
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Group).NotEmpty();
            RuleFor(x => x.MaxRow).NotEmpty();
        }

    }
}
