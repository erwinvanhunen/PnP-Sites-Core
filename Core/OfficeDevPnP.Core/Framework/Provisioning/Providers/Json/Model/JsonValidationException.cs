using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Model
{
    public class JsonValidationException : Exception
    {
        public List<JsonValidationError> ValidationErrors { get; set; } = new List<JsonValidationError>();

        public JsonValidationException() : base() { }

        public JsonValidationException(string message) : base(message)
        {
        }

        public JsonValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JsonValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class JsonValidationError
    {
        public bool HasLineInfo { get; set; }
        public JsonValidationErrorKind Kind { get; set; }

        public int LineNumber { get; set; }

        public int LinePosition { get; set; }

        public string Path { get; set; }

        public string Property { get; set; }
    }

    public enum JsonValidationErrorKind
    {
        Unknown = 0,
        StringExpected = 1,
        NumberExpected = 2,
        IntegerExpected = 3,
        BooleanExpected = 4,
        ObjectExpected = 5,
        PropertyRequired = 6,
        ArrayExpected = 7,
        NullExpected = 8,
        PatternMismatch = 9,
        StringTooShort = 10,
        StringTooLong = 11,
        NumberTooSmall = 12,
        NumberTooBig = 13,
        IntegerTooBig = 14,
        TooManyItems = 15,
        TooFewItems = 16,
        ItemsNotUnique = 17,
        DateTimeExpected = 18,
        DateExpected = 19,
        TimeExpected = 20,
        TimeSpanExpected = 21,
        UriExpected = 22,
        IpV4Expected = 23,
        IpV6Expected = 24,
        GuidExpected = 25,
        NotAnyOf = 26,
        NotAllOf = 27,
        NotOneOf = 28,
        ExcludedSchemaValidates = 29,
        NumberNotMultipleOf = 30,
        IntegerNotMultipleOf = 31,
        NotInEnumeration = 32,
        EmailExpected = 33,
        HostnameExpected = 34,
        TooManyItemsInTuple = 35,
        ArrayItemNotValid = 36,
        AdditionalItemNotValid = 37,
        AdditionalPropertiesNotValid = 38,
        NoAdditionalPropertiesAllowed = 39,
        TooManyProperties = 40,
        TooFewProperties = 41,
        Base64Expected = 42,
        NoTypeValidates = 43
    }
}
