using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeneXus.Server.ExternalTool.Jenkins
{
    public class Configuration
    {
        [JsonProperty("instance")]
        public List<JenkinsServer> JenkinsInstance { get; set; }
    }
}
