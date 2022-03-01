using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WPFTextBox.Model;

namespace WPFTextBox.ViewModel
{
    public class SingleTextBoxVM
    {
        public SingleTextBoxModel Model { get; set; }

        public List<ValidationError> Errors { get; private set; }
        private Action Save { get; set; }
        public SingleTextBoxVM()
        {
            Model = new SingleTextBoxModel();
            Messages = new Dictionary<string, bool>();
            RecvMsgHandlers = new Dictionary<string, Action<object>>();
            Errors = new List<ValidationError>();
        }

        public void FillData(string name, string value, string unit = null, string preRegex = null, Action save = null)
        {
            Model.Name = name;
            Model.Value = value;
            Model.Unit = unit;
            Model.PreviewTextInputRegex = preRegex;
            Model.PropertyChanged -= SingleTextBoxModel_PropertyChanged;
            Model.PropertyChanged += SingleTextBoxModel_PropertyChanged;
            Save = save;
        }

        public void SaveData()
        {
            Save?.Invoke();
        }

        public void AddValidator(Action<string> validator)
        {
            Model.Validator = validator;
        }

        private void SingleTextBoxModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.Value))
            {
                SingleTextBoxModel tm = sender as SingleTextBoxModel;

                foreach (string msg in Messages.Keys)
                {
                    NotifyMsg(msg, tm.Value);
                }

            }
        }


        public Dictionary<string, bool> Messages { get; private set; }
        public void AddNotifyMsg(string message, bool withParam = false)
        {
            Messages[message] = withParam;
        }

        public void NotifyMsg(string message, object para)
        {
            if (Messages.ContainsKey(message))
            {
                if (Messages[message])
                {
                    // ViewModelCommunication.Messaging.NotifyColleagues(message, para);  // Notify message with param
                }
                else
                {
                    // ViewModelCommunication.Messaging.NotifyColleagues(message);
                }
            }
        }


        public Dictionary<string, Action<object>> RecvMsgHandlers { get; set; }
        public void AddRegisterMessage(string msg, Action<object> p)
        {
            RecvMsgHandlers[msg] = p;
            // ViewModelCommunication.Messaging.Register(msg, p);
        }
    }
}
