﻿using System;
using System.Collections.Specialized;
using ModFreeSwitch.Codecs;
using ModFreeSwitch.Common;
using ModFreeSwitch.Messages;

namespace ModFreeSwitch.Events {
    public class EslEvent {
        private readonly bool _ignoreBody;
        private readonly EslMessage _response;

        public EslEvent(EslMessage response,
            bool ignoreBody) {
            _response = response;
            _ignoreBody = ignoreBody;
        }

        public EslEvent(EslMessage response) : this(response, false) {}

        public string CallerUuid => this["Caller-Unique-ID"];

        public string ChannelCallUuid => this["Channel-Call-UUID"];

        public string ChannelName => this["Channel-Name"];

        public string CoreUuid => this["Core-UUID"];

        public string EventCallingFile => this["Event-Calling-File"];

        public string EventCallingFunction => this["Event-Calling-Function"];

        public string EventCallingLineNumber => this["Event-Calling-Line-Number"];

        public DateTime EventDateGmt => DateTime.Parse(this["Event-Date-GMT"]);

        public DateTime EventDateLocal => DateTime.Parse(this["Event-Date-Local"]);

        public string EventName => this["Event-Name"];

        public string EventSubClass => this["Event-Subclass"];

        public DateTime EventTimeStamp => this["Event-Date-timestamp"].FromUnixTime();

        public string UniqueId => this["Unique-ID"];

        public string this[string headerName] {
            get {
                if (_ignoreBody) {
                    if (_response.HasHeader(headerName))
                        return _response.HeaderValue(headerName);
                    if (_response.HasHeader("variable_" + headerName))
                        return _response.HeaderValue("variable_" + headerName);
                }

                var map = ParseBodyLines();
                if (map.ContainsKey(headerName)) return map[headerName];
                if (map.ContainsKey("variable_" + headerName))
                    return map["variable_" + headerName];
                return null;
            }
        }

        protected StringDictionary ParseBodyLines() {
            var resp = new StringDictionary();
            foreach (var bodyLine in _response.BodyLines) {
                var parsedLines = EslHeaderParser.SplitHeader(bodyLine);
                if (parsedLines == null) continue;
                if (parsedLines.Length == 2) resp.Add(parsedLines[0], parsedLines[1]);
                if (parsedLines.Length == 1) resp.Add("__CONTENT__", bodyLine);
            }
            return resp;
        }
    }
}