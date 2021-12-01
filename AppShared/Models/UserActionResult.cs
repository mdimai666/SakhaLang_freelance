using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    public class UserActionResult
    {
        public bool Ok {  get; set; }
        public string Message {  get; set; }
    }

    public class UserActionResult<TData>
    {
        public bool Ok {  get; set; }
        public string Message {  get; set; }
        public TData Data {  get; set; }
    }
}
