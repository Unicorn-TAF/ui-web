﻿using System.Collections.Generic;
using System.Linq;

namespace Unicorn.Core.Testing.Verification.Matchers.CollectionMatchers
{
    public class HasItemsMatcher : Matcher
    {
        private IEnumerable<object> expectedObjects;
        
        public HasItemsMatcher(IEnumerable<object> expectedObjects)
        {
            this.expectedObjects = expectedObjects;
        }

        public override string CheckDescription
        {
            get
            {
                string itemsList = string.Join(", ", this.expectedObjects);

                if (itemsList.Length > 200)
                {
                    itemsList = itemsList.Substring(0, 200) + " etc . . .";
                }

                return "Collection has items: " + itemsList;
            }
        }

        public override bool Matches(object collectionObj)
        {
            if (collectionObj == null)
            {
                DescribeMismatch("null");
                return Reverse;
            }

            IEnumerable<object> collection = (IEnumerable<object>)collectionObj;

            IEnumerable<object> mismatchItems = this.Reverse ?
                collection.Where(i => expectedObjects.Contains(i)) :
                expectedObjects.Where(i => !collection.Contains(i));

            if (mismatchItems.Any())
            {
                DescribeMismatch($"items {(this.Reverse ? "" : "not ")}presented: {string.Join(", ", mismatchItems)}");
                return Reverse;
            }

            return !this.Reverse;
        }
    }
}
