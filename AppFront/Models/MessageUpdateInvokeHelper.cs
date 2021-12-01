using Microsoft.JSInterop;
using System;

namespace AppFront
{
    public class MessageUpdateInvokeHelper
    {
        private Action action;

        public MessageUpdateInvokeHelper(Action action)
        {
            this.action = action;
        }

        [JSInvokable("AppFront")]
        public void UpdateMessageCaller()
        {
            action.Invoke();
        }
    }
}