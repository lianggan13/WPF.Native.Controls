using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using WPF.Virtualization.DataVirtualization.CollectionProvider;

namespace WPF.Virtualization.DataVirtualization.Model
{
    /// <summary>
    /// Demo implementation of IItemsProvider returning dummy customer items after
    /// a pause to simulate network/disk latency.
    /// </summary>
    public class CustomersProvider : IItemsProvider<Customer>
    {
        private readonly int _count;
        private readonly int _fetchDelay;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersProvider"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="fetchDelay">The fetch delay.</param>
        public CustomersProvider(int count, int fetchDelay)
        {
            _count = count;
            _fetchDelay = fetchDelay;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            Trace.WriteLine("FetchCount");
            Thread.Sleep(_fetchDelay);
            return _count;
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<Customer> FetchRange(int startIndex, int count)
        {
            Trace.WriteLine("FetchRange: " + startIndex + "," + count);
            Thread.Sleep(_fetchDelay);

            List<Customer> list = new List<Customer>();
            for (int i = startIndex; i < startIndex + count; i++)
            {
                Customer customer = new Customer { Id = i + 1, Name = "Customer " + (i + 1) };
                list.Add(customer);
            }
            return list;
        }
    }
}
