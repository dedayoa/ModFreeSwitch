/*
    Copyright [2016] [Arsene Tochemey GANDOTE]

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/

using ModFreeSwitch.Messages;

namespace ModFreeSwitch.Events {
    public class ChannelExecuteComplete : EslEvent {
        public ChannelExecuteComplete(EslMessage message) : base(message) {}

        public string Application {
            get { return this["Application"]; }
        }

        public string ApplicationData {
            get { return this["Application-Data"]; }
        }

        /// <summary>
        ///     Gets reponse from the application
        /// </summary>
        public string ApplicationResponse {
            get { return this["Application-Response"]; }
        }

        public override string ToString() {
            return "ExecuteComplete(" + Application + ", '" + ApplicationData + "')." +
                   base.ToString();
        }
    }
}
