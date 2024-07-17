using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Product.Api.Configurations.Swagger;

public class DefaultParametersFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (parameter is { } nonBodyParameter)
        {
            if (nonBodyParameter.Description == null)
                nonBodyParameter.Description = context.ApiParameterDescription.ModelMetadata?.Description;

            if (context.ApiParameterDescription.RouteInfo != null)
                parameter.Required |= !context.ApiParameterDescription.RouteInfo.IsOptional;
        }
    }
}