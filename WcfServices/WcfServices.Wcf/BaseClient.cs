using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfServices.Wcf
{
	public interface IBaseClient
	{ }

	public abstract class BaseClient<T> : ClientBase<T>, IBaseClient
		where T : class
	{
		public BaseClient(Binding binding, EndpointAddress remoteAddress) :
			base(binding, remoteAddress)
		{ }
	}

	/*
     * Example:
     * 
    
    public class MyClient : BaseClient<IContract>, IContract
    {
        public MyClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        { }

        public bool Test()
        {
            return this.Channel.Test();
        }
    }
    */
}
