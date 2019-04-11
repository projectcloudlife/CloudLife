
using Client.Command.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class LoginViewModel : ViewModel
    {

        public string Message { get; set; } = "123";
        private string newMessage = "";

        public string NewMessage
        {
            get { return newMessage; }
            set { Notify("NewMessage"); newMessage = value; }
        }

        [CommandExecute]
        void ClickMe(object param)
        {
            Message = (string)param;
            Notify(nameof(Message));
        }

        [CommandCanExecute]
        bool CanClickMe()
        {
            if(NewMessage.Length > 5)
            {
                return true;
            }
            return false;
        }

    }
}
