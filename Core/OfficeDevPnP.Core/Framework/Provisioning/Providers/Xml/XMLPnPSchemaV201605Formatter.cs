using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ContentType = OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType;
using OfficeDevPnP.Core.Extensions;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml
{
    internal class XMLPnPSchemaV201605Formatter :
        IXMLSchemaFormatter, ITemplateFormatter
    {
        private TemplateProviderBase _provider;

        public void Initialize(TemplateProviderBase provider)
        {
            this._provider = provider;
        }

        string IXMLSchemaFormatter.NamespaceUri
        {
            get { return (XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2016_05); }
        }

        string IXMLSchemaFormatter.NamespacePrefix
        {
            get { return (XMLConstants.PROVISIONING_SCHEMA_PREFIX); }
        }

        public bool IsValid(Stream template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            // Load the template into an XDocument
            XDocument xml = XDocument.Load(template);

            // Load the XSD embedded resource
            Stream stream = typeof(XMLPnPSchemaV201605Formatter)
                .Assembly
                .GetManifestResourceStream("OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.ProvisioningSchema-2016-05.xsd");

            // Prepare the XML Schema Set
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2016_05,
                new XmlTextReader(stream));

            Boolean result = true;
            xml.Validate(schemas, (o, e) =>
            {
                Diagnostics.Log.Error(e.Exception, "SchemaFormatter", "Template is not valid: {0}", e.Message);
                result = false;
            });

            return (result);
        }

        Stream ITemplateFormatter.ToFormattedTemplate(Model.ProvisioningTemplate template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            V201605.ProvisioningTemplate result = new V201605.ProvisioningTemplate();

            V201605.Provisioning wrappedResult = new V201605.Provisioning();
            wrappedResult.Preferences = new V201605.Preferences
            {
                Generator = this.GetType().Assembly.FullName
            };
            wrappedResult.Templates = new V201605.Templates[] {
                new V201605.Templates
                {
                    ID = String.Format("CONTAINER-{0}", template.Id),
                    ProvisioningTemplate = new V201605.ProvisioningTemplate[]
                    {
                        result
                    }
                }
            };


            #region Localizations

            if (template.Localizations != null && template.Localizations.Count > 0)
            {
                wrappedResult.Localizations =
                (from l in template.Localizations
                 select new LocalizationsLocalization
                 {
                     LCID = l.LCID,
                     Name = l.Name,
                     ResourceFile = l.ResourceFile,
                 }).ToArray();
            }

            #endregion


            // Find all parsers and run them in sequence
            var type = typeof(IBaseElementParser);
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            foreach (var parser in types.OrderBy(p => p.GetCustomAttribute<BaseElementParserAttribute>().Sequence))
            {
                if (
                    parser.GetCustomAttribute<BaseElementParserAttribute>()
                        .SupportedSchemas.HasFlag(XMLPnPSchemaVersion.V201605))
                {
                    var baseElementParser = Activator.CreateInstance(parser) as IBaseElementParser;
                    if (baseElementParser != null)
                    {
                        result =
                            baseElementParser.ParseTemplate(XMLPnPSchemaVersion.V201605, result, template) as
                                V201605.ProvisioningTemplate;
                    }
                }
            }

            XmlSerializerNamespaces ns =
                new XmlSerializerNamespaces();
            ns.Add(((IXMLSchemaFormatter)this).NamespacePrefix,
                ((IXMLSchemaFormatter)this).NamespaceUri);

            var output = XMLSerializer.SerializeToStream<V201605.Provisioning>(wrappedResult, ns);
            output.Position = 0;
            return (output);
        }

        public Model.ProvisioningTemplate ToProvisioningTemplate(Stream template)
        {
            return (this.ToProvisioningTemplate(template, null));
        }

        public Model.ProvisioningTemplate ToProvisioningTemplate(Stream template, String identifier)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            // Crate a copy of the source stream
            MemoryStream sourceStream = new MemoryStream();
            template.CopyTo(sourceStream);
            sourceStream.Position = 0;

            // Check the provided template against the XML schema
            if (!this.IsValid(sourceStream))
            {
                // TODO: Use resource file
                throw new ApplicationException("The provided template is not valid!");
            }

            sourceStream.Position = 0;
            XDocument xml = XDocument.Load(sourceStream);
            XNamespace pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2016_05;

            // Prepare a variable to hold the single source formatted template
            V201605.ProvisioningTemplate source = null;

            // Prepare a variable to hold the resulting ProvisioningTemplate instance
            Model.ProvisioningTemplate result = new Model.ProvisioningTemplate();

            // Determine if we're working on a wrapped SharePointProvisioningTemplate or not
            if (xml.Root.Name == pnp + "Provisioning")
            {
                // Deserialize the whole wrapper
                V201605.Provisioning wrappedResult = XMLSerializer.Deserialize<V201605.Provisioning>(xml);

                // Handle the wrapper schema parameters
                if (wrappedResult.Preferences != null &&
                    wrappedResult.Preferences.Parameters != null &&
                    wrappedResult.Preferences.Parameters.Length > 0)
                {
                    foreach (var parameter in wrappedResult.Preferences.Parameters)
                    {
                        result.Parameters.Add(parameter.Key, parameter.Text != null ? parameter.Text.Aggregate(String.Empty, (acc, i) => acc + i) : null);
                    }
                }

                // Handle Localizations
                if (wrappedResult.Localizations != null)
                {
                    result.Localizations.AddRange(
                        from l in wrappedResult.Localizations
                        select new Localization
                        {
                            LCID = l.LCID,
                            Name = l.Name,
                            ResourceFile = l.ResourceFile,
                        });
                }

                foreach (var templates in wrappedResult.Templates)
                {
                    // Let's see if we have an in-place template with the provided ID or if we don't have a provided ID at all
                    source = templates.ProvisioningTemplate.FirstOrDefault(spt => spt.ID == identifier || String.IsNullOrEmpty(identifier));

                    // If we don't have a template, but there are external file references
                    if (source == null && templates.ProvisioningTemplateFile.Length > 0)
                    {
                        // Otherwise let's see if we have an external file for the template
                        var externalSource = templates.ProvisioningTemplateFile.FirstOrDefault(sptf => sptf.ID == identifier);

                        Stream externalFileStream = this._provider.Connector.GetFileStream(externalSource.File);
                        xml = XDocument.Load(externalFileStream);

                        if (xml.Root.Name != pnp + "ProvisioningTemplate")
                        {
                            throw new ApplicationException("Invalid external file format. Expected a ProvisioningTemplate file!");
                        }
                        else
                        {
                            source = XMLSerializer.Deserialize<V201605.ProvisioningTemplate>(xml);
                        }
                    }

                    if (source != null)
                    {
                        break;
                    }
                }
            }
            else if (xml.Root.Name == pnp + "ProvisioningTemplate")
            {
                var IdAttribute = xml.Root.Attribute("ID");

                // If there is a provided ID, and if it doesn't equal the current ID
                if (!String.IsNullOrEmpty(identifier) &&
                    IdAttribute != null &&
                    IdAttribute.Value != identifier)
                {
                    // TODO: Use resource file
                    throw new ApplicationException("The provided template identifier is not available!");
                }
                else
                {
                    source = XMLSerializer.Deserialize<V201605.ProvisioningTemplate>(xml);
                }
            }

            // Find all parsers and run them in sequence
            var type = typeof(IBaseElementParser);
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            
            foreach (var parser in types.OrderBy(p => p.GetCustomAttribute<BaseElementParserAttribute>().Sequence))
            {
                if (parser.GetCustomAttribute<BaseElementParserAttribute>().SupportedSchemas.HasFlag(XMLPnPSchemaVersion.V201605))
                {
                    var baseElementParser = Activator.CreateInstance(parser) as IBaseElementParser;
                    if (baseElementParser != null)
                    {
                        result = baseElementParser.ParseElement(XMLPnPSchemaVersion.V201605, result, source);
                    }
                }
            }

            return (result);
        }
    }

}

