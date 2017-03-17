using System.Collections.Generic;
using System.Linq;
using LP.ServiceHost.DataContracts.Common.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class GivenFilterGlossaryItems : BaseGiven
    {
        private List<GlossaryItem> _result;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheFiltersParameterAndSortAreNullButGlossaryItemsHasElements : GivenFilterGlossaryItems
        {
            protected override void When()
            {
                _result = SUT.FilterGlossaryItems(null, null, new List<GlossaryItem>()
                {
                    new GlossaryItem()
                    {
                        Title = "Glossary 2",
                        Description = "Description glossary 2"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 1",
                        Description = "Description glossary 1"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 3",
                        Description = "Description glossary 3"
                    },

                });
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldBeNotNull()
            {
                Assert.IsNotNull(_result);
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldHaveTheCorrectNumberOfElements()
            {
                const int expected = 3;
                Assert.IsTrue(_result.Count == expected);
            }

            [Test]
            public void ThenTheFirstElementIsNotNull()
            {
                Assert.IsNotNull(_result.First());
            }

            [Test]
            public void ThenTheFirstElementTitleIsTheCorrectOne()
            {
                const string firstElementTitle = "Glossary 1";
                Assert.IsTrue(_result.First().Title == firstElementTitle);
            }

            [Test]
            public void ThenTheLastElementIsNotNull()
            {
                Assert.IsNotNull(_result.Last());
            }

            [Test]
            public void ThenTheLastElementTitleIsTheCorrectOne()
            {
                const string lastElementTitle = "Glossary 3";
                Assert.IsTrue(_result.Last().Title == lastElementTitle);
            }
        }

        public class WhenTheFiltersParameterIsNullButSortGlossaryItemsHasElements : GivenFilterGlossaryItems
        {
            protected override void When()
            {
                _result = SUT.FilterGlossaryItems(null, "desc", new List<GlossaryItem>()
                {
                    new GlossaryItem()
                    {
                        Title = "Glossary 2",
                        Description = "Description glossary 2"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 1",
                        Description = "Description glossary 1"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 3",
                        Description = "Description glossary 3"
                    },

                });
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldBeNotNull()
            {
                Assert.IsNotNull(_result);
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldHaveTheCorrectNumberOfElements()
            {
                const int expected = 3;
                Assert.IsTrue(_result.Count == expected);
            }

            [Test]
            public void ThenTheFirstElementIsNotNull()
            {
                Assert.IsNotNull(_result.First());
            }

            [Test]
            public void ThenTheFirstElementTitleIsTheCorrectOne()
            {
                const string firstElementTitle = "Glossary 3";
                Assert.IsTrue(_result.First().Title == firstElementTitle);
            }

            [Test]
            public void ThenTheLastElementIsNotNull()
            {
                Assert.IsNotNull(_result.Last());
            }

            [Test]
            public void ThenTheLastElementTitleIsTheCorrectOne()
            {
                const string lastElementTitle = "Glossary 1";
                Assert.IsTrue(_result.Last().Title == lastElementTitle);
            }
        }

        public class WhenTheFiltersParameterAndSortIsNotNullGlossaryItemsHasElements : GivenFilterGlossaryItems
        {
            protected override void When()
            {
                _result = SUT.FilterGlossaryItems("Glossary 2", "desc", new List<GlossaryItem>()
                {
                    new GlossaryItem()
                    {
                        Title = "Glossary 2",
                        Description = "Description glossary 2"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 1",
                        Description = "Description glossary 1"
                    },
                    new GlossaryItem()
                    {
                        Title = "Glossary 3",
                        Description = "Description glossary 3"
                    },

                });
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldBeNotNull()
            {
                Assert.IsNotNull(_result);
            }

            [Test]
            public void ThenTheGlossaryItemsListShouldHaveTheCorrectNumberOfElements()
            {
                const int expected = 1;
                Assert.IsTrue(_result.Count == expected);
            }

            [Test]
            public void ThenTheFirstElementIsNotNull()
            {
                Assert.IsNotNull(_result.First());
            }

            [Test]
            public void ThenTheFirstElementTitleIsTheCorrectOne()
            {
                const string firstElementTitle = "Glossary 2";
                Assert.IsTrue(_result.First().Title == firstElementTitle);
            }

            [Test]
            public void ThenTheLastElementIsNotNull()
            {
                Assert.IsNotNull(_result.Last());
            }

            [Test]
            public void ThenTheLastElementTitleIsTheCorrectOne()
            {
                const string lastElementTitle = "Glossary 2";
                Assert.IsTrue(_result.Last().Title == lastElementTitle);
            }
        }

        //public class WhenTheFiltersHaveModulesAndSortGlossaryItemsHasElements : GivenFilterGlossaryItems
        //{
        //    protected override void When()
        //    {
        //        _result = SUT.FilterGlossaryItems("Glossary_module1_module2", "desc", new List<GlossaryItem>()
        //        {
        //            new GlossaryItem()
        //            {
        //                Title = "Glossary 2",
        //                Description = "Description glossary 2",
        //                TrainingModules = "module1,module2"
        //            },
        //            new GlossaryItem()
        //            {
        //                Title = "Glossary 1",
        //                Description = "Description glossary 1",
        //                TrainingModules = "module1"
        //            },
        //            new GlossaryItem()
        //            {
        //                Title = "Glossary 3",
        //                Description = "Description glossary 3",
        //                TrainingModules = "module3"
        //            },

        //        });
        //    }

        //    [Test]
        //    public void ThenTheGlossaryItemsListShouldBeNotNull()
        //    {
        //        Assert.IsNotNull(_result);
        //    }

        //    [Test]
        //    public void ThenTheGlossaryItemsListShouldHaveTheCorrectNumberOfElements()
        //    {
        //        const int expected = 2;
        //        Assert.IsTrue(_result.Count == expected);
        //    }

        //    [Test]
        //    public void ThenTheFirstElementIsNotNull()
        //    {
        //        Assert.IsNotNull(_result.First());
        //    }

        //    [Test]
        //    public void ThenTheFirstElementTitleIsTheCorrectOne()
        //    {
        //        const string firstElementTitle = "Glossary 2";
        //        Assert.IsTrue(_result.First().Title == firstElementTitle);
        //    }

        //    [Test]
        //    public void ThenTheLastElementIsNotNull()
        //    {
        //        Assert.IsNotNull(_result.Last());
        //    }

        //    [Test]
        //    public void ThenTheLastElementTitleIsTheCorrectOne()
        //    {
        //        const string lastElementTitle = "Glossary 1";
        //        Assert.IsTrue(_result.Last().Title == lastElementTitle);
        //    }
        //}

        public class WhenFilterNotNullAndGlossaryItemsIsNull : GivenFilterGlossaryItems
        {
            protected override void When()
            {
                _result = SUT.FilterGlossaryItems("Glossary ", "desc", null);
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_result);
            }

            [Test]
            public void ThenResultIsANewList()
            {
                Assert.IsTrue(_result.Count == 0);
            }
        }
    }
}
