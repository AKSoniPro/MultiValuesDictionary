using MultiValueDictionaryContract;
using System;
using System.Collections.Generic;

namespace MultiValueDictionary
{
    internal class MultiValueDictionaryApplication
    {
        private readonly IMultiValueDictionaryProvider<string, string> _dictionary;
        private readonly static string InvalidArgumentMessage = MsgEnums.InvalidArgument.Message();
        private readonly IPrinter _printer;
        public MultiValueDictionaryApplication(IMultiValueDictionaryProvider<string, string> dictionary, IPrinter printer)
        {
            _dictionary = dictionary;
            _printer = printer;
        }

        public void CallMethod(string input)
        {

            var inputArray = input.Split(" ");
            if (inputArray.Length == 0)
            {
                _printer.Print(InvalidArgumentMessage);
            }

            try
            {
                switch (inputArray[0].ToUpperInvariant())
                {
                    case "KEYS":
                        PresentItem(_dictionary.Keys(), _printer);
                        break;

                    case "MEMBERS":
                        if (CheckFirstArgument(inputArray, _printer))
                            PresentItem(_dictionary.Members(inputArray[1]), _printer);
                        break;

                    case "ADD":
                        if (CheckTwoArguments(inputArray, _printer))
                             _printer.Print(_dictionary.Add(inputArray[1], inputArray[2]).Message());
                        break;

                    case "REMOVE":
                        if (CheckTwoArguments(inputArray, _printer))
                             _printer.Print(_dictionary.Remove(inputArray[1], inputArray[2]).Message());
                        break;

                    case "REMOVEALL":
                        if (CheckFirstArgument(inputArray, _printer))
                             _printer.Print(_dictionary.RemoveAll(inputArray[1]).Message());
                        break;

                    case "CLEAR":
                         _printer.Print(_dictionary.Clear().Message());
                        break;

                    case "KEYEXISTS":
                        if (CheckFirstArgument(inputArray, _printer))
                             _printer.Print(_dictionary.KeyExists(inputArray[1]).ToString());
                        break;

                    case "MEMBEREXISTS":
                        if (CheckTwoArguments(inputArray, _printer))
                             _printer.Print(_dictionary.MemberExists(inputArray[1], inputArray[2]).ToString());
                        break;

                    case "ALLMEMBERS":
                        PresentItem(_dictionary.AllMembers(), _printer);
                        break;

                    case "ITEMS":
                        GetAllItems(_dictionary.Items(), _printer);
                        break;

                    case "QUIT":

                    default:
                         _printer.Print("Command not suppoted - " + input);
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                 _printer.Print(ex.Message);
            }
        }


        private static bool CheckFirstArgument(string[] input, IPrinter printer)
        {
            if (input.Length == 2 && !string.IsNullOrEmpty(input[1]))
                return true;
            else
                printer.Print(InvalidArgumentMessage);
            return false;
        }

        private static bool CheckTwoArguments(string[] input, IPrinter printer)
        {

            if (input.Length == 3 && !string.IsNullOrEmpty(input[1]) && !string.IsNullOrEmpty(input[2]))
                return true;
            else

                printer.Print(InvalidArgumentMessage);
            return false;
        }

        private static void PresentItem(IEnumerable<string> keys, IPrinter printer)
        {
            int i = 0;
            foreach (var key in keys)
            {
                printer.Print($"{++i}) {key}");
            }
            CheckEmptySet(i, printer);
        }

        private static void CheckEmptySet(int i, IPrinter printer)
        {
            if (i == 0)
            {
                printer.Print("(empty set)");
            }
        }

        private static void GetAllItems(IEnumerable<(string, string)> items, IPrinter printer)
        {
            int i = 0;
            foreach (var item in items)
            {
                printer.Print($"{++i}) {item.Item1} : {item.Item2}");
            }
            CheckEmptySet(i, printer);
        }

    }
}