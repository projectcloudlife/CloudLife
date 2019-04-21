using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLogic.Interfaces
{
    public interface IMessagesService
    {
        void ShowMessage(string title,string content);
    }
}
