using BillBuddy.Common.Entities;
using BillBuddy.Common.Tasks;
using BillBuddy.Common.Utils;
using GigaSpaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.UI.Reports
{
    public class CountPaymentsByCategoryFinder
    {
        private ISpaceProxy _spaceProxy;
        private String categoryName;

        public CountPaymentsByCategoryFinder(ISpaceProxy spaceProxy)
        {
            this._spaceProxy = spaceProxy;
        }

        public void ExecuteReport()
        {
            CategoryType[] categoryTypes = Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>().ToArray();

            CategoryType categoryType = Utility.FetchRandomEnumValue<CategoryType>();

            Utility.LogMessage("Search for merchants with following category: {0}" , categoryType);

            categoryName = categoryType.ToString();

            _spaceProxy.BeginExecute(new CountPaymentsByCategoryTask(categoryType), new AsyncCallback<long>(OnResult), null);
        }

        private void OnResult(IAsyncResult<long> asyncResult)
        {
            long result = _spaceProxy.EndExecute(asyncResult);

            Utility.LogMessage("Found number of Payments : " + result + " for category " + categoryName);
        }
    }
}
