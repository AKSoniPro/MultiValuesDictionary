namespace MultiValueDictionaryContract
{
    /// <summary>
    /// Response contract to same uniform understanding of response code
    /// </summary>
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

    /// <summary>
    /// Default messages mapped to response code provide to be used by client, which support globalization and patch release.
    /// </summary>
    public static class EventTypeExtension
    {
        public static string Message(this MsgEnums msg)
        {
            return Resources.ResourceManager.GetString(msg.ToString());
        }
    }
}
