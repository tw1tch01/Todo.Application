using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Todo.WebAPI.Areas;

namespace Todo.WebAPI.Docs
{
    public class TagDescriptionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = AreaNames.Items, Description = "Resource endpoints for managing all the Todo items." },
                new OpenApiTag { Name = AreaNames.Notes, Description = "Resource endpoints for managing all the notes linked to a Todo item." },
            };
        }
    }
}