using Newtonsoft.Json;
using System;

namespace LabManagamentSchedule.Core.Messages
{
    public abstract class Message
    {
        public string Dominio { get; set; }
        public string MessageType { get; protected set; }
        public Guid CurrelationId { get; protected set; }

        protected Message(string dominio)
        {
            this.Dominio = dominio;
            this.CurrelationId = Guid.NewGuid();
            this.MessageType = GetType().Name;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
