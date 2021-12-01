using System;
using System.Collections.Generic;



namespace AppShared.Models
{
    public class LogEntity : SimpleEntity
    {
        public int Code;
        public string Message;
        public string Stacktrace;
        public List<string> Tags;

        public Guid ObjectId;
        public string ObjectEntity;
    }
}