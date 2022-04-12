using MultiValueDictionaryContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionaryProvider
{
    /// <summary>
    /// Generic class which is multivalue dictionary provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class MultiValueDictionaryProvider<T, K> : IMultiValueDictionaryProvider<T, K>
    {
        #region ctor
        public MultiValueDictionaryProvider()
        {
            CreateNewDictionary();
        }

        private void CreateNewDictionary()
        {
            _dict = new Dictionary<T, HashSet<K>>();
        }

        #endregion

        #region private members

        private Dictionary<T, HashSet<K>> _dict;
        private bool CheckKeyExist(T key)
        {
            return _dict.ContainsKey(key) ? true : throw new ArgumentException(MsgEnums.ErrorKeyNotExist.Message());
        }

        private bool CheckMemberNotExist(T key, K val)
        {
            return _dict[key].Contains(val) ? throw new ArgumentException(MsgEnums.ErrorMemberExists.Message()) : true;
        }

        private bool CheckMemberExist(T key, K val)
        {
            return _dict[key].Contains(val) ? true : throw new ArgumentException(MsgEnums.ErrorMemberNotExists.Message());
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds a member to a collection for a given key. Displays an error if the member already exists for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public MsgEnums Add(T key, K val)
        {
            if (_dict.ContainsKey(key) && CheckMemberNotExist(key, val))
            {
                _dict[key].Add(val);
            }
            else
            {
                _dict.Add(key, new HashSet<K>() { val });
            };
            return MsgEnums.SuccessMsgAdded;
        }

        /// <summary>
        /// Removes a member from a key. If the last member is removed from the key, the key is removed from the dictionary. 
        /// If the key or member does not exist, displays an  error.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public MsgEnums Remove(T key, K val)
        {
            if (CheckKeyExist(key) && CheckMemberExist(key, val))
            {
                if (_dict[key].Count == 1)
                    _dict.Remove(key);
                else
                    _dict[key].Remove(val);
            }
            return MsgEnums.SuccessMsgMemberRemoved;
        }


        /// <summary>
        /// Returns all the keys in the dictionary. Order is not guaranteed.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Keys()
        {
            return _dict.Keys;
        }

        /// <summary>
        /// Returns the collection of strings for the given key.Return order is not guaranteed.Returns an error if the key does not exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<K> Members(T key)
        {
            if (CheckKeyExist(key))
                return _dict[key];
            else
                throw new ArgumentException(nameof(MsgEnums.ErrorKeyNotExist));
        }

        /// <summary>
        /// Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        /// <returns></returns>
        public MsgEnums RemoveAll(T key)
        {
            if (CheckKeyExist(key))
            {
                _dict.Remove(key);
                return MsgEnums.SuccessMsgMemberRemovedAll;
            }
            else
                throw new ArgumentException(MsgEnums.ErrorKeyNotExist.Message());
        }

        /// <summary>
        /// Removes all keys and all members from the dictionary.
        /// </summary>
        /// <returns></returns>
        public MsgEnums Clear()
        {
            CreateNewDictionary();
            return MsgEnums.ClearMsg;
        }

        /// <summary>
        /// Returns whether a key exists or not.
        /// </summary>
        /// <returns></returns>
        public bool KeyExists(T key)
        {
            return _dict.ContainsKey(key);
        }

        /// <summary>
        /// Returns whether a member exists within a key. Returns false if the key does not exist.
        /// </summary>
        /// <returns></returns>
        public bool MemberExists(T key, K val)
        {
            return _dict.ContainsKey(key) && _dict[key].Contains(val);
        }

        /// <summary>
        /// Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<K> AllMembers()
        {
            return _dict.SelectMany(x => x.Value.Select(y => y));
        }

        /// <summary>
        /// Returns all keys in the dictionary and all of their members. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(T, K)> Items()
        {
            return _dict.SelectMany(x => x.Value.Select(y => (x.Key, y)));
        }
        #endregion
    }
}
