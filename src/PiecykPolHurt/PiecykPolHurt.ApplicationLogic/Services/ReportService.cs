using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.ApplicationLogic.Extensions;
using PiecykPolHurt.ApplicationLogic.Helpers;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Dto.Report;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;
using System.Data;
using System.Text;
using System.Xml;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface IReportService
    {
        Task<bool> CreateReportDefinitionAsync(CreateReportDefinitionCommand command);
        Task<byte[]> GenerateReportAsync(GeneratorQuery query);
        Task<ReportDefinitionDto> GetReportById(int id);
        Task<ReportGenerationPageDto> GetReportPageAsync(int reportId);
        Task<PaginatedList<ReportListItemDto>> GetReportsAsync(ReportQuery query, bool isAdmin = false);
        Task<bool> UpdateReportDefinitionAsync(UpdateReportDefinitionCommand command);
    }

    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReportDefinitionCommand> _createReportValidator;
        private readonly IValidator<UpdateReportDefinitionCommand> _updateReportValidator;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateReportDefinitionCommand> createReportValidator, IValidator<UpdateReportDefinitionCommand> updateReportValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createReportValidator = createReportValidator;
            _updateReportValidator = updateReportValidator;
        }

        public async Task<PaginatedList<ReportListItemDto>> GetReportsAsync(ReportQuery query, bool isAdmin = false)
        {
            var reports = _unitOfWork.ReportDefinitionRepository.GetAll().AsNoTracking();

            reports = !string.IsNullOrEmpty(query.Title)
                ? reports.Where(x => x.Title.ToLower().Contains(query.Title.ToLower()))
                : reports;

            if (!isAdmin)
            {
                reports = reports.Where(x => x.IsActive);
            }

            if (query.SortOption.HasValue)
            {
                if (query.SortOption.Value == ReportSortOption.TitleAsc)
                    reports = reports.OrderBy(x => x.Title);
                else if (query.SortOption.Value == ReportSortOption.TitleDesc)
                    reports = reports.OrderByDescending(x => x.Title);
                else if (query.SortOption.Value == ReportSortOption.GroupDesc)
                    reports = reports.OrderByDescending(x => x.Group);
                else
                    reports = reports.OrderBy(x => x.Group);

            }
            else
            {
                reports = reports.OrderBy(x => x.Title);
            }

            return await reports.ProjectTo<ReportListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<ReportDefinitionDto> GetReportById(int id)
            => await _unitOfWork.ReportDefinitionRepository
            .GetById(id)
            .AsNoTracking()
            .ProjectTo<ReportDefinitionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<ReportGenerationPageDto> GetReportPageAsync(int reportId)
        {
            var definition = await _unitOfWork.ReportDefinitionRepository.GetById(reportId).AsNoTracking().FirstOrDefaultAsync();

            if (definition != null)
            {
                var paramsList = new List<ReportParamDto>();
                var doc = new XmlDocument();
                doc.LoadXml(definition.XmlDefinition);
                // get all <param> nodes
                var reportParams = doc.GetElementsByTagName("param");
                if (reportParams.Count > 0)
                {
                    //connection for params values
                    var conn = new SqlConnection(_unitOfWork.ConnectionString);
                    conn.Open();
                    for (int i = 0; i < reportParams.Count; i++)
                    {
                        var isRequired = false;
                        var param = reportParams[i];
                        var childNodes = param.ChildNodes;
                        var name = string.Empty;
                        var tag = string.Empty;
                        var type = string.Empty;
                        var caption = string.Empty;
                        for (int j = 0; j < childNodes.Count; j++)
                        {
                            //<name> node
                            if (childNodes[j].Name == "name")
                            {
                                name = childNodes[j].InnerText;
                            }
                            //<type> node
                            else if (childNodes[j].Name == "type")
                            {
                                type = childNodes[j].InnerText;
                            }
                            //<tag> node
                            else if (childNodes[j].Name == "tag")
                            {
                                tag = childNodes[j].InnerText;
                            }
                            //<caption> node
                            else if (childNodes[j].Name == "caption")
                            {
                                caption = childNodes[j].InnerText;
                            }
                            //<required> node
                            else if (childNodes[j].Name == "required")
                            {
                                isRequired = childNodes[j].InnerText == "1";
                            }
                        }
                        if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(caption))
                        {
                            var paramValues = new List<ReportParamValueDto>();
                            if (!string.IsNullOrEmpty(tag))
                            {
                                //for type lst available items are set in convention value1^label1#value2^label2
                                if (type == "lst")
                                {
                                    //get all value-label pairs
                                    var items = tag.Split("#");
                                    foreach (var item in items)
                                    {
                                        var elements = item.Split("^");
                                        if (elements.Length == 2)
                                        {
                                            paramValues.Add(new ReportParamValueDto { Value = elements[0], Label = elements[1].Replace("_", " ") });
                                        }
                                    }
                                }
                                else
                                {
                                    //retrieve values from query in tag node
                                    using var cmd = new SqlCommand(tag, conn);
                                    using var valuesTable = new DataTable();
                                    using (var da = new SqlDataAdapter(cmd))
                                    {
                                        da.Fill(valuesTable);
                                    }
                                    var indexOfLabel = valuesTable.Columns.IndexOf("label");
                                    var indexOfValue = valuesTable.Columns.IndexOf("value");
                                    if (indexOfLabel != -1 && indexOfValue != -1)
                                    {
                                        foreach (DataRow item in valuesTable.Rows)
                                        {
                                            paramValues.Add(new ReportParamValueDto { Label = item[indexOfLabel].ToString().Replace("_", " "), Value = item[indexOfValue].ToString() });
                                        }
                                    }
                                }
                            }
                            var typeEnum = type switch
                            {
                                "dis" => ParamType.Dis,
                                "dat" => ParamType.Dat,
                                "lik" => ParamType.Lik,
                                "lst" => ParamType.Lst,
                                "dic" => ParamType.Dic,
                                _ => ParamType.Unknown
                            };
                            paramsList.Add(new ReportParamDto
                            {
                                Name = name,
                                Type = typeEnum,
                                AvailableValues = paramValues,
                                IsRequired = isRequired,
                                Caption = caption
                            });
                        }

                    }
                    conn.Close();
                    return new ReportGenerationPageDto
                    {
                        Title = definition.Title,
                        Description = definition.Description,
                        MaxRow = definition.MaxRow,
                        ReportId = reportId,
                        Params = paramsList
                    };
                }
            }
            return null;
        }

        public async Task<byte[]> GenerateReportAsync(GeneratorQuery query)
        {
            var definition = await _unitOfWork.ReportDefinitionRepository
            .GetById(query.Reportid)
            .AsNoTracking()
            .FirstOrDefaultAsync();

            var doc = new XmlDocument();
            doc.LoadXml(definition.XmlDefinition);
            var builder = new StringBuilder();
            var queryParam = doc.GetElementsByTagName("query");
            if (queryParam.Count > 0)
            {
                builder.Append(queryParam[0].InnerText);
                var reportParams = doc.GetElementsByTagName("param");
                for (int i = 0; i < reportParams.Count; i++)
                {
                    var param = reportParams[i];
                    var childNodes = param.ChildNodes;
                    var name = string.Empty;
                    var paramSql = string.Empty;
                    var type = string.Empty;
                    for (int j = 0; j < childNodes.Count; j++)
                    {
                        if (childNodes[j].Name == "name")
                        {
                            name = childNodes[j].InnerText;
                        }
                        else if (childNodes[j].Name == "type")
                        {
                            type = childNodes[j].InnerText;
                        }
                        else if (childNodes[j].Name == "condition")
                        {
                            paramSql = childNodes[j].InnerText;
                        }
                    }
                    if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(paramSql))
                    {
                        var value = query.ParamsValues.FirstOrDefault(x => x.Key == name && x.Values.Any(y => y != null));
                        if (value != null && value.Values.Any())
                        {
                            switch (type)
                            {
                                case "lik":
                                    paramSql = paramSql.Replace($":{name}", $"\'%{value.Values.First().ToLower()}%\'");
                                    break;
                                case "dat":
                                    paramSql = paramSql.Replace($":{name}", $"\'{value.Values.First()}\'");
                                    break;
                                case "lst":
                                case "dic":
                                case "dis":
                                    paramSql = paramSql.Replace($":{name}", String.Join(',', value.Values));
                                    break;
                                default:
                                    break;
                            }

                            builder.Append(paramSql);
                        }
                    }
                }
                if (builder.Length > 0)
                {
                    var orderBy = doc.GetElementsByTagName("order");
                    if (orderBy.Count > 0)
                    {
                        builder.Append($" ORDER BY {orderBy[0].InnerText}");
                    }
                }
                var sql = builder.ToString();
                int firstSelectIndex = sql.ToLower().IndexOf("select");
                if (query.MaxRows.HasValue)
                {

                    //replace select with select top MaxRows - for sql server database

                    sql = sql.Remove(firstSelectIndex, 6).Insert(firstSelectIndex, $"SELECT TOP {query.MaxRows.Value}");
                }
                else
                {
                    var maxRowsNode = doc.GetElementsByTagName("maxrec");
                    if (maxRowsNode.Count != 0)
                    {
                        sql = sql.Remove(firstSelectIndex, 6).Insert(firstSelectIndex, $"SELECT TOP {maxRowsNode[0].InnerText}");
                    }
                    else
                    {
                        //insert default max rows
                        sql = sql.Remove(firstSelectIndex, 6).Insert(firstSelectIndex, $"SELECT TOP 100000");
                    }
                }
                using var dataTable = new DataTable();
                var conn = new SqlConnection(_unitOfWork.ConnectionString);
                var cmd = new SqlCommand(sql, conn);

                conn.Open();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                    conn.Close();
                }

                var columns = dataTable.Columns.Cast<DataColumn>()
                                                 .Select(x => x.ColumnName)
                                                 .ToList();
                var rowsData = new List<string[]>();
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item.ItemArray != null && item.ItemArray.Any())
                    {
                        rowsData.Add(item.ItemArray.Select(x => x != null ? x.ToString() : "").ToArray());
                    }
                }

                return rowsData.GetGeneratorExportData(columns);
            }
            else
            {
                throw new Exception("Empty query");
            }
        }

        public async Task<bool> CreateReportDefinitionAsync(CreateReportDefinitionCommand command)
        {
            var validationResult = await _createReportValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var definition = _mapper.Map<ReportDefinition>(command);
                _unitOfWork.ReportDefinitionRepository.Add(definition);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateReportDefinitionAsync(UpdateReportDefinitionCommand command)
        {
            var validationResult = await _updateReportValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var definition = await _unitOfWork.ReportDefinitionRepository.GetById(command.Id).FirstOrDefaultAsync();
                definition.Title = command.Title;
                definition.Description = command.Description;
                definition.IsActive = command.IsActive ?? definition.IsActive;
                definition.XmlDefinition = command.XmlDefinition;
                definition.Version = command.Version;
                definition.Group = command.Group;
                definition.MaxRow = command.MaxRow;
                _unitOfWork.ReportDefinitionRepository.Update(definition);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
