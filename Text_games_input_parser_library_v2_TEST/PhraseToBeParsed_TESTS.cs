using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Text_games_input_parser_library_v2;

namespace Text_games_input_parser_library_v2_TESTS
{
    [TestClass]
    public class PhraseToBeParsed_TESTS
    {

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST18()
        {
            string inputData = "pick-up water and food LEAVE TENT,CAR()*&^and Go to THE_woodS coLLecT fire-wood+TIndER RUN to the RiVer DrOp_ropes waLK to the HILLS";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "go", new List<string>() {"woods", "river", "hills"} },
                    { "take", new List<string>() {"water", "food", "water food", "fire-wood", "tinder", "fire-wood tinder"} }, 
                    { "drop", new List<string>() {"tent", "car", "tent car", "ropes"} }
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST21()
        {
            string inputData = "pick-up zero1 zero2 take one two three four collect 1 2 3 4";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "go", new List<string>() { } },
                    { "take", new List<string>() {"zero1", "zero2", "zero1 zero2", 
                                                "one", "two", "one two", "three", "one two three", "four", "one two three four", 
                                                "three four", "two three four", 
                                                "1", "2", "1 2", "3", "1 2 3", "4", "1 2 3 4", "3 4", "2 3 4"} }, 
                    { "drop", new List<string>() { } }
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST22()
        {
            string inputData = "leave zero1 zero2 zero3 take one two three four take five six go 1 2 3 4 5";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "go", new List<string>() {  "1", "2", "1 2", "3", "1 2 3", "4", "1 2 3 4", "5", "1 2 3 4 5", 
                                                "4 5", "3 4 5", "2 3 4 5" } },
                    { "take", new List<string>() { "one", "two", "one two", "three", "one two three", "four", "one two three four", 
                                                "three four", "two three four", "five", "six", "five six" } },
                    { "drop", new List<string>() { "zero1", "zero2", "zero1 zero2", "zero3", "zero1 zero2 zero3", "zero2 zero3" } }
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST23()
        {
            string inputData = "drop zero1 zero2 zero3 pick one two three four pick five six walk 1 2 3 4 5";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "look", new List<string>() {} },
                    { "examine", new List<string> {} },
                    { "go", new List<string>() {  "1", "2", "1 2", "3", "1 2 3", "4", "1 2 3 4", "5", "1 2 3 4 5", 
                                                "4 5", "3 4 5", "2 3 4 5" } },
                    { "take", new List<string>() { "one", "two", "one two", "three", "one two three", "four", "one two three four", 
                                                "three four", "two three four", "five", "six", "five six" } },
                    { "drop", new List<string>() { "zero1", "zero2", "zero1 zero2", "zero3", "zero1 zero2 zero3", "zero2 zero3" } }
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST24()
        {
            string inputData = "pick-up water and food LOOK One,and.tWO!@#\\| LEAVE TENT,CAR()*&^and Go to THE_woodS coLLecT fire-wood+TIndER RUN to the RiVer DrOp_ropes waLK to the HILLS";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "look", new List<string>() {"one", "two", "one two"} },
                    { "examine", new List<string> {} },
                    { "go", new List<string>() {"woods", "river", "hills"} },
                    { "take", new List<string>() {"water", "food", "water food", "fire-wood", "tinder", "fire-wood tinder"} }, 
                    { "drop", new List<string>() {"tent", "car", "tent car", "ropes"} },
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST25()
        {
            string inputData = "pick-up water and food LOOK One,and.tWO!@#_at_\\| LEAVE TENT,CAR()*&^and Go to THE_woodS coLLecT fire-wood+TIndER RUN to the RiVer DrOp_ropes waLK to the HILLS";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "look", new List<string>() {"one", "two", "one two", "at", "one two at", "two at"} },
                    { "examine", new List<string> {"one", "two", "one two", "at", "one two at", "two at"} },
                    { "go", new List<string>() {"woods", "river", "hills"} },
                    { "take", new List<string>() {"water", "food", "water food", "fire-wood", "tinder", "fire-wood tinder"} }, 
                    { "drop", new List<string>() {"tent", "car", "tent car", "ropes"} },
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsFalse(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST26()
        {
            string inputData = "pick-up water and food LOOK One,and.tWO!@#_at_ARouND\\| LEAVE TENT,CAR()*&^and Go to THE_woodS coLLecT fire-wood+TIndER RUN to the RiVer DrOp_ropes waLK to the HILLS";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "look", new List<string>() {"one", "two", "one two", "at", "one two at", "one two at around", "at around", "two at around"} },
                    { "examine", new List<string> {"one", "two", "one two", "at", "one two at", "one two at around", "at around", "two at around"} },
                    { "go", new List<string>() {"woods", "river", "hills"} },
                    { "take", new List<string>() {"water", "food", "water food", "fire-wood", "tinder", "fire-wood tinder"} }, 
                    { "drop", new List<string>() {"tent", "car", "tent car", "ropes"} },
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsTrue(phrase.LookAroundHasBeenCalled);
        }

        [TestMethod]
        public void UpdateProperty_ItemsCorrespondingToKeyWords_TEST27()
        {
            string inputData = "pick-up water and food CHeck leaFs,branchES+and GRASS LOOK One,and.tWO!@#_at_ARouND\\| LEAVE TENT,CAR()*&^and Go to THE_woodS coLLecT fire-wood+TIndER RUN to the RiVer DrOp_ropes waLK to the HILLS InspeCT mountain studY FOREST";
            var expectedResult = new Dictionary<string, List<string>>()
                {
                    { "look", new List<string>() {"one", "two", "one two", "at", "one two at", "one two at around", "at around", "two at around"} },
                    { "examine", new List<string> {"leafs", "branches", "leafs branches", "grass", "leafs branches grass", 
                                                    "branches grass", "mountain", "forest", "one", "two", "one two", "at", "one two at", 
                                                    "one two at around", "at around", "two at around"} },
                    { "go", new List<string>() {"woods", "river", "hills"} },
                    { "take", new List<string>() {"water", "food", "water food", "fire-wood", "tinder", "fire-wood tinder"} }, 
                    { "drop", new List<string>() {"tent", "car", "tent car", "ropes"} },
                };
            PhraseToBeParsed phrase = new PhraseToBeParsed(inputData);

            foreach (string key in expectedResult.Keys)
            {
                Assert.AreEqual(expectedResult[key].Count, phrase.ItemsCorrespondingToKeyWords[key].Count);
                for (int i = 0; i < phrase.ItemsCorrespondingToKeyWords[key].Count; i++)
                    Assert.AreEqual(expectedResult[key][i], phrase.ItemsCorrespondingToKeyWords[key][i]);
            }
            Assert.IsTrue(phrase.LookAroundHasBeenCalled);
        }
    
    }
}
