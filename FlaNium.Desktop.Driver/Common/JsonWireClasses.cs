﻿
namespace FlaNium.Desktop.Driver.Common
{
    #region

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion

    public class JsonElementContent
    {
        #region Constructors and Destructors

        public JsonElementContent(string element)
        {
            this.Element = element;
        }

        #endregion

        #region Public Properties

        [JsonProperty("ELEMENT")]
        public string Element { get; set; }

        #endregion
    }

    public class JsonResponse
    {
        #region Constructors and Destructors

        public JsonResponse(string sessionId, ResponseStatus responseCode, object value)
        {
            this.SessionId = sessionId;
            this.Status = responseCode;

            this.Value = responseCode == ResponseStatus.Success ? value : this.PrepareErrorResponse(value);
        }

        private object PrepareErrorResponse(object value)
        {
            var result = new Dictionary<string, string> { { "error", JsonErrorCodes.Parse(this.Status) } };

            string message;
            var exception = value as Exception;
            if (exception != null)
            {
                message = exception.Message;
                result.Add("stacktrace", exception.StackTrace);
            }
            else
            {
                message = value.ToString();
            }

            result.Add("message", message);
            return result;
        }

        #endregion

        #region Public Properties

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        #endregion
    }
}
