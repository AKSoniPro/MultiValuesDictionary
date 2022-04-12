using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiValueDictionaryContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MultiValueDictionaryProvider.Tests
{
    [TestClass()]
    public class MultiValueDictionaryProviderTests
    {
        private readonly IMultiValueDictionaryProvider<string, string> _dictionary;

        const string clearMsg = ") Cleared";
        const string errorKeyNotExist = ") ERROR, key does not exist";
        const string errorMemberExists = ") ERROR, member already exists for key";
        const string successMsgAdded = ") Added";
        const string successMsgMemberRemoved = ") Removed";
        const string successMsgMemberRemovedAll = ") Removed";
        const string errorMemberNotExists = ") ERROR, member does not exist";


        public MultiValueDictionaryProviderTests()
        {
            _dictionary = new MultiValueDictionaryProvider<string, string>();
        }
        private void Cleanup()
        {
            Assert.AreEqual(clearMsg, _dictionary.Clear().Message());
        }
        [TestMethod()]
        public void MultiValueDictionaryProviderTest()
        {
            Assert.IsNotNull(_dictionary);
        }

        /// <summary>
        //> ADD foo bar
        //) Added
        //> ADD foo baz
        //) Added
        //> ADD foo bar
        //) ERROR, member already exists for key
        /// </summary>
        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(successMsgAdded, _dictionary.Add("foo", "bar").Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add("foo", "baz").Message());
            var exception = Assert.ThrowsException<ArgumentException>(() => _dictionary.Add("foo", "bar"));
            Assert.AreEqual(errorMemberExists, exception.Message);
            Cleanup();
        }


        /// <summary>
        /////> ADD foo bar
        //) Added
        //> ADD foo baz
        //) Added
        //> REMOVE foo bar
        //) Removed
        //> REMOVE foo bar
        //) ERROR, member does not exist
        //> KEYS
        //1) foo
        //> REMOVE foo baz
        //) Removed
        //> KEYS
        //) empty set
        //> REMOVE boom pow
        //) ERROR, key does not exist
        /// </summary>
        [TestMethod()]
        public void RemoveTest()
        {
            var key1 = "foo";
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "bar").Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "baz").Message());
            Assert.AreEqual(successMsgMemberRemoved, _dictionary.Remove(key1, "bar").Message());

            var exception = Assert.ThrowsException<ArgumentException>(() => _dictionary.Remove(key1, "bar"));
            Assert.AreEqual(errorMemberNotExists, exception.Message);

            var keys = _dictionary.Keys();
            Assert.AreEqual(1, keys.Count());
            Assert.AreEqual(key1, keys.First());

            Assert.AreEqual(successMsgMemberRemoved, _dictionary.Remove(key1, "baz").Message());

            keys = _dictionary.Keys();
            Assert.AreEqual(0, keys.Count());

            exception = Assert.ThrowsException<ArgumentException>(() => _dictionary.Remove("boom", "pow"));
            Assert.AreEqual(errorKeyNotExist, exception.Message);

            Cleanup();

        }

        /// <summary>
        ///// > ADD foo bar
        //) Added
        //> ADD baz bang
        //) Added
        //> KEYS
        //1) foo
        //2) baz
        /// </summary>
        [TestMethod()]
        public void KeysTest()
        {
            var key1 = "foo";
            var key2 = "baz";
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "bar").Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, "bang").Message());


            var keys = _dictionary.Keys();
            Assert.AreEqual(2, keys.Count());
            Assert.AreEqual(key1, keys.First());
            Assert.AreEqual(key2, keys.Last());

            Cleanup();
        }

        /// <summary>
        ///// > ADD foo bar
        //> ADD foo baz
        //> MEMBERS foo
        //1) bar
        //2) baz
        //> MEMBERS bad
        //) ERROR, key does not exist.
        /// </summary>
        [TestMethod()]
        public void MembersTest()
        {
            var key1 = "foo";
            var member1 = "bar";
            var member2 = "baz";
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member1).Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member2).Message());

            var members = _dictionary.Members(key1);

            Assert.AreEqual(2, members.Count());
            Assert.AreEqual(member1, members.First());
            Assert.AreEqual(member2, members.Last());


            var exception = Assert.ThrowsException<ArgumentException>(() => _dictionary.Members("bad"));
            Assert.AreEqual(errorKeyNotExist, exception.Message);

            Cleanup();
        }


        /// <summary>
        ///// > ADD foo bar
        //) Added
        //> ADD foo baz
        //) Added
        //> KEYS
        //1) foo
        //> REMOVEALL foo
        //) Removed
        //> KEYS
        //(empty set)
        //REMOVEALL foo
        //) ERROR, key does not exist
        /// </summary>
        [TestMethod()]
        public void RemoveAllTest()
        {
            var key1 = "foo";
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "bar").Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "baz").Message());

            var keys = _dictionary.Keys();
            Assert.AreEqual(1, keys.Count());
            Assert.AreEqual(key1, keys.First());

            Assert.AreEqual(successMsgMemberRemovedAll, _dictionary.RemoveAll(key1).Message());

            keys = _dictionary.Keys();
            Assert.AreEqual(0, keys.Count());

            var exception = Assert.ThrowsException<ArgumentException>(() => _dictionary.RemoveAll(key1));
            Assert.AreEqual(errorKeyNotExist, exception.Message);

            Cleanup();
        }

        /// <summary>
        ///// > ADD foo bar
        //) Added
        //> ADD bang zip
        //) Added
        //> KEYS
        //1) foo
        //2) bang
        //> CLEAR
        //) Cleared
        //> KEYS
        //(empty set)
        //> CLEAR
        //) Cleared
        //> KEYS
        //(empty set)
        /// </summary>
        [TestMethod()]
        public void ClearTest()
        {
            var key1 = "foo";
            var key2 = "bang";
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "bar").Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, "zip").Message());

            var keys = _dictionary.Keys();
            Assert.AreEqual(2, keys.Count());
            Assert.AreEqual(key1, keys.First());
            Assert.AreEqual(key2, keys.Last());

            Assert.AreEqual(clearMsg, _dictionary.Clear().Message());

            keys = _dictionary.Keys();
            Assert.AreEqual(0, keys.Count());

            Assert.AreEqual(clearMsg, _dictionary.Clear().Message());

            keys = _dictionary.Keys();
            Assert.AreEqual(0, keys.Count());

            Cleanup();
        }


        /// <summary>
        /// > KEYEXISTS foo
        //) false
        //> ADD foo bar
        //) Added
        //> KEYEXISTS foo
        //) true
        /// </summary>
        [TestMethod()]
        public void KeyExistsTest()
        {
            var key1 = "foo";
            Assert.IsFalse(_dictionary.KeyExists(key1));

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, "bar").Message());
            Assert.IsTrue(_dictionary.KeyExists(key1));

            Cleanup();
        }

        /// <summary>
        /// > MEMBEREXISTS foo bar
        //) false
        //> ADD foo bar
        //) Added
        //> MEMBEREXISTS foo bar
        //) true
        //> MEMBEREXISTS foo baz
        //) false
        /// </summary>
        [TestMethod()]
        public void MemberExistsTest()
        {
            var key1 = "foo";
            var member1 = "bar";
            var member2 = "baz";

            Assert.IsFalse(_dictionary.MemberExists(key1, member1));

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member1).Message());

            Assert.IsTrue(_dictionary.MemberExists(key1, member1));

            Assert.IsFalse(_dictionary.MemberExists(key1, member2));

            Cleanup();
        }


        /// <summary>
        ///// > ALLMEMBERS
        //(empty set)
        //> ADD foo bar
        //) Added
        //> ADD foo baz
        //) Added
        //> ALLMEMBERS
        //1) bar
        //2) baz
        //> ADD bang bar
        //) Added
        //> ADD bang baz
        //> ALLMEMBERS
        //1) bar
        //2) baz
        //3) bar
        //4) baz

        /// </summary>
        [TestMethod()]
        public void AllMembersTest()
        {
            var members = _dictionary.AllMembers();
            Assert.AreEqual(0, members.Count());

            var key1 = "foo";
            var key2 = "bang";
            var member1 = "bar";
            var member2 = "baz";

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member1).Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member2).Message());

            members = _dictionary.AllMembers();
            Assert.AreEqual(2, members.Count());
            Assert.AreEqual(member1, members.First());
            Assert.AreEqual(member2, members.Last());

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, member1).Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, member2).Message());

            members = _dictionary.AllMembers();
            Assert.AreEqual(4, members.Count());

            CollectionAssert.AreEqual(new List<string>() { member1, member2, member1, member2 }, members.ToList());

            Cleanup();
        }


        /// <summary>
        /// > ITEMS
        //(empty set)
        //> ADD foo bar
        //) Added
        //> ADD foo baz
        //) Added
        //> ITEMS
        //1) foo: bar
        //2) foo: baz
        //> ADD bang bar
        //) Added
        //> ADD bang baz
        //> ITEMS
        //1) foo: bar
        //2) foo: baz
        //3) bang: bar
        //4) bang: baz
        /// </summary>
        [TestMethod()]
        public void ItemsTest()
        {
            var members = _dictionary.Items();
            Assert.AreEqual(0, members.Count());

            var key1 = "foo";
            var key2 = "bang";
            var member1 = "bar";
            var member2 = "baz";

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member1).Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key1, member2).Message());

            members = _dictionary.Items();
            Assert.AreEqual(2, members.Count());
            Assert.AreEqual(key1, members.First().Item1);
            Assert.AreEqual(member1, members.First().Item2);
            Assert.AreEqual(key1, members.Last().Item1);
            Assert.AreEqual(member2, members.Last().Item2);

            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, member1).Message());
            Assert.AreEqual(successMsgAdded, _dictionary.Add(key2, member2).Message());

            members = _dictionary.Items();
            Assert.AreEqual(4, members.Count());

            CollectionAssert.AreEqual(new List<(string, string)>() { (key1, member1), (key1, member2), (key2, member1), (key2, member2) }, members.ToList());
            Cleanup();
        }


        #region Performace Test - must be only enabled for performace fine tuning

#if !DEBUG


        [TestMethod()]
        public void ItemsTestLoad10()
        {
            int maxCount = 10;

            LoadTestAdd(maxCount);
            Cleanup();
        }

        [TestMethod()]
        public void ItemsTestLoad1000()
        {
            int maxCount = 1000;

            LoadTestAdd(maxCount);
            Cleanup();
        }
        [TestMethod()]
        public void ItemsTestLoad10000()
        {
            int maxCount = 10000;

            LoadTestAdd(maxCount);
            Cleanup();
        }

        [TestMethod()]
        public void ItemsTestLoad100000()
        {
            int maxCount = 100000;

            LoadTestAdd(maxCount);
            Cleanup();
        }

        [TestMethod()]
        public void SearchKeyTestLoad100000()
        {
            int maxCount = 100000;

            LoadTestAdd(maxCount);

            var key = _dictionary.Keys().ElementAtOrDefault(200);

            var watch = new Stopwatch();
            watch.Start();
            Assert.IsTrue(_dictionary.KeyExists(key));


            watch.Stop();
            Trace.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, for Search load of {maxCount}");
            Cleanup();
        }

        [TestMethod()]
        public void AllMemberTestLoad100000()
        {
            int maxCount = 100000;

            LoadTestAdd(maxCount);

            var watch = new Stopwatch();
            watch.Start();

            var items = _dictionary.AllMembers();

            watch.Stop();
            Trace.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, for Get All Members load of {maxCount}");
            Assert.IsNotNull(items);
            Cleanup();
        }

        [TestMethod()]
        public void AllItemsTestLoad100000()
        {
            int maxCount = 100000;

            LoadTestAdd(maxCount);



            var watch = new Stopwatch();
            watch.Start();

            var items = _dictionary.Items();

            watch.Stop();
            Trace.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, for Get load of {maxCount}");
            Assert.IsNotNull(items);
            Cleanup();
        }

        private void LoadTestAdd(int maxCount)
        {
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < maxCount; i++)
            {
                try
                {
                    var key = RandomString();
                    _dictionary.Add(key, RandomString());
                    for (int j = 0; j < 100; j++)
                    {
                        _dictionary.Add(key, RandomString());
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }

            watch.Stop();
            Trace.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, for ADD load of {maxCount}");
        }

        private string RandomString()
        {
            return Guid.NewGuid().ToString();
        }
#endif

        #endregion

    }
}