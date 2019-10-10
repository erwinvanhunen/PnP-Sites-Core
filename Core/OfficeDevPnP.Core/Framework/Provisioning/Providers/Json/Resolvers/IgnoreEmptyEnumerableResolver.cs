using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OfficeDevPnP.Core.Framework.Provisioning.Model.Teams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Resolvers
{
    public class IgnoreEmptyEnumerableResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) &&
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    IEnumerable enumerable = null;
                    // this value could be in a public field or public property
                    switch (member.MemberType)
                    {
                        case MemberTypes.Property:
                            enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                ?.GetValue(instance, null) as IEnumerable;
                            break;
                        case MemberTypes.Field:
                            enumerable = instance
                                .GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as IEnumerable;
                            break;
                    }

                    return enumerable == null ||
                           enumerable.GetEnumerator().MoveNext();
                    // if the list is null, we defer the decision to NullValueHandling
                };
            }
            else if (property.PropertyType == typeof(XElement))
            {
                property.ShouldSerialize = instance =>
                {
                    XElement element = null;
                    switch (member.MemberType)
                    {
                        case MemberTypes.Property:
                            element = instance
                                .GetType()
                                .GetProperty(member.Name)
                                ?.GetValue(instance, null) as XElement;
                            break;
                        case MemberTypes.Field:
                            element = instance
                                .GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as XElement;
                            break;
                    }
                    return element == null || !string.IsNullOrEmpty(element.ToString());
                };
            } else if (property.PropertyType == typeof(TeamSpecialization))
            {
                property.ShouldSerialize = instance =>
                {

                    TeamSpecialization specialization = ((Team)instance).Specialization;

                    return specialization != TeamSpecialization.None;
                };
            }

            return property;
        }
    }
}
