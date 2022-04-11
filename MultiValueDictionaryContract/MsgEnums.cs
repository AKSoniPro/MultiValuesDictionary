namespace MultiValueDictionaryContract
{
    public enum MsgEnums
    {
        SuccessMsgAdded,
        ErrorMemberExists,
        SuccessMsgMemberRemoved,
        SuccessMsgMemberRemovedAll,
        ErrorKeyNotExist,
        EmptySetMsg,
        ClearMsg,
        ErrorMemberNotExists,
        InvalidArgument
    }
    public static class EventTypeExtension
    {
        public static string Message(this MsgEnums msg)
        {
            return Resources.ResourceManager.GetString(msg.ToString());
        }
    }
}
