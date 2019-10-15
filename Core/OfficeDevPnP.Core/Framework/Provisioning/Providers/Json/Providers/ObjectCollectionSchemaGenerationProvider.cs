using NJsonSchema.Generation;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Providers
{
    //public class ObjectCollectionSchemaGenerationProvider : JsonSchemaGenerator
    //{
    //    public override JsonSchema GetSchema(JSchemaTypeGenerationContext context)
    //    {
    //        if(context.ObjectType.BaseType.IsGenericType)
    //        {
    //            return GenerateCollectionSchema(context.ObjectType.BaseType.GenericTypeArguments[0]);
    //        }
    //        return null;
    //    }

    //    private JSchema GenerateCollectionSchema(Type baseType)
    //    {
    //        JSchemaGenerator generator = new JSchemaGenerator();
    //        var listType = typeof(List<>);
    //        var constructedListType = listType.MakeGenericType(baseType);
    //        return generator.Generate(constructedListType);
    //    }
    //}
}
