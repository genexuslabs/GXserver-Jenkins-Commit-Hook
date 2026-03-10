using System;
using System.Text.RegularExpressions;

namespace GeneXus.Server.ExternalTool.Jenkins
{
    public class InfoCommitData
    {
        public InfoCommitData(CommitData commit)
        {
            ParseComments(commit);
        }

        public CommitInfo CommitInformation
        {
            get;
            set;
        }

        public class CommitInfo
        {

            public string build { get; set; }
            public string GxUser { get; set; }

            public CommitInfo(string b)
            : this(b, string.Empty)
            {
            }
            public CommitInfo()
            {
            }
            public CommitInfo(string b, string u)
            {
                build = b;
                GxUser = u;
            }

        }
        private void ParseComments(CommitData commit)
        {
            try
            {
                CommitInformation = GetCommitInfo(commit);
            }
            catch
            {
                throw new ArgumentException("Error parsing the information to be sent.");
            }

        }

        private CommitInfo GetCommitInfo(CommitData commit)
        {
            try
            {

                CommitInfo CommitInformation = new CommitInfo();
                Regex rx = new Regex(@"(?i)(#build:(?<build>[yes|y]))");
                MatchCollection matches = rx.Matches(commit.Comment);
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    if (groups["build"].Value != string.Empty)
                        CommitInformation.build = groups["build"].Value;
                }

                Regex rxUser = new Regex(@"((?i)(#GXuser:(?<gxuser>[\w]*$)))");
                MatchCollection matchesuser = rxUser.Matches(commit.Comment);
                foreach (Match match in matchesuser)
                {
                    GroupCollection groups = match.Groups;
                    if (groups["gxuser"].Value != string.Empty)
                        CommitInformation.GxUser = groups["gxuser"].Value;

                }

                return CommitInformation;
            }
            catch
            {
                throw new ArgumentException("Parsing of the comment has failed.");
            }
        }
    }
}
