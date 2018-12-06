using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub
{
    public class MainViewModel : BindableBase
    {
        private SubscriptionToken _msgOnlyToken;
        private SubscriptionToken _msgWithValue;

        public MainViewModel()
        {


            _msgOnlyToken = EventServiceFactory.EventService.GetEvent<GenericEvent>().Subscribe(topic =>
           {
               if (topic == Topics.MsgOnly)
               {

               }
           });

            _msgWithValue = EventServiceFactory.EventService.GetEvent<GenericEvent<string>>()
                .Subscribe(param =>
                {
                    if (param.Topic == Topics.MsgWithActionAndValue)
                    {

                    }
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent>()
                .Publish("");

            EventServiceFactory.EventService.GetEvent<GenericEvent<string>>()
                .Publish(new EventParameters<string>("Message with action", action: () => { }, value: ""));
        }
    }

    public static class Topics
    {
        public const string MsgOnly = "MsgOnly";
        public const string MsgWithActionAndValue = "MsgWithActionAndValue";
    }
}
