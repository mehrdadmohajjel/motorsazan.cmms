using System.Runtime.Serialization;

namespace ClientApp.Models
{
    public class Reason
    {
        private string log;
        private string user;

        [DataMember]
        public string Log
        {
            set { log = value; }
            get
            {
                if(log == null)
                {
                    log = InitLog();
                }

                return log;
            }
        }

        [DataMember]
        public string User
        {
            set { user = value; }
            get
            {
                if(user == null)
                {
                    user = InitUser();
                }

                return user;
            }
        }

        [DataMember]
        public string Code { set; get; }

        private string InitLog()
        {
            return log;
        }

        private string InitUser()
        {
            return user;
        }

        public Reason(string messageIndex, string moreInfo = null)
        {
            Code = messageIndex;
            Log = moreInfo;
        }
    }
}