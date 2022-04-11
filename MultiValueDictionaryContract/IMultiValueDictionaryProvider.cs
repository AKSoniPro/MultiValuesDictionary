using System.Collections.Generic;

namespace MultiValueDictionaryContract
{
    public interface IMultiValueDictionaryProvider<T, K>
    {
        MsgEnums Add(T key, K val);
        MsgEnums Remove(T key, K val);
        IEnumerable<T> Keys();
        IEnumerable<K> Members(T key);
        MsgEnums RemoveAll(T key);
        MsgEnums Clear();
        bool KeyExists(T key);
        bool MemberExists(T key, K val);
        IEnumerable<K> AllMembers();
        IEnumerable<(T, K)> Items();
    }
}
