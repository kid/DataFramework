
namespace DDD.Domain.MetaData
{
    public abstract class MetaData
    {
        private readonly MetaDataStore metaDataStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaData"/> class.
        /// </summary>
        /// <param name="metaDataStore">The meta data store.</param>
        protected MetaData(MetaDataStore metaDataStore)
        {
            this.metaDataStore = metaDataStore;
        }

        /// <summary>
        /// Gets the meta data store.
        /// </summary>
        /// <value>The meta data store.</value>
        protected MetaDataStore MetaDataStore { get { return this.metaDataStore; } }
    }
}
