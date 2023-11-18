namespace HangmanResponse.Notifications
{
    public class Notification
    {
        public string SourseName { get; private set; }
        public string Message { get; private set; }
        public string? InnerMessage { get; private set; }
        public ActionType Action { get; private set; }
        public DateTime When { get; private set; }
        public bool IsException { get; private set; }

        public Notification(string sourseName, string message, ActionType actionType, bool isExcption = false)
        {
            SourseName = sourseName;
            Message = message;
            Action = actionType;
            IsException = isExcption;

            When = DateTime.Now;
        }

        public Notification(string sourseName, string message, string innerMessage, ActionType actionType, bool isExcption = false)
        {
            SourseName = sourseName;
            Message = message;
            InnerMessage = innerMessage;
            Action = actionType;
            IsException = isExcption;

            When = DateTime.Now;
        }
    }

    public enum ActionType : int
    {
        INIT,
        READ,
        VALIDATE,
        CHECK,
        NONE
    }
}
