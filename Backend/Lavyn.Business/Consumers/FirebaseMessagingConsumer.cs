using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rx.Http;
using Rx.Http.Interceptors;

namespace Lavyn.Business.Consumers
{
    public class FirebaseMessagingConsumer : RxConsumer
    {
        public FirebaseMessagingConsumer(IConsumerConfiguration<RxConsumer> consumerConfiguration, IConfiguration configuration) : base(consumerConfiguration)
        {
            consumerConfiguration.Http.BaseAddress = new Uri(@"https://fcm.googleapis.com/");
            consumerConfiguration.RequestInterceptors.Add(new FirebaseMessagingInterceptor(configuration));
        }

        public IObservable<FirebaseMessagingResponseDto> Send(FirebaseMessagingRequestDto message) =>
            Post<FirebaseMessagingResponseDto>("fcm/send", message);
    }
    
    public class FirebaseNotificationDto
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
    
    public class FirebaseMessagingRequestDto
    {
        public FirebaseMessagingRequestDto(string to, FirebaseNotificationDto notification)
        {
            To = to;
            Notification = notification;
        }
        
        [JsonProperty("to")]
        public string To { get; private set; }
        
        [JsonProperty("notification")]
        public FirebaseNotificationDto Notification { get; private set; }

    }
    
    public class FirebaseMessagingResponseDto
    {
        [JsonProperty("multicast_id")]
        public string MulticastId { get; }
        
        [JsonProperty("success")]
        public long Success { get; }
        
        [JsonProperty("failure")]
        public long Failure { get; }

        [JsonProperty("canonical_ids")]
        public long CanonicalIds { get; }

        [JsonProperty("results")]
        public IDictionary<string, string> Results { get; }

    }
    
    internal class FirebaseMessagingInterceptor : RxRequestInterceptor
    {
        private readonly IConfiguration _configuration;
        public FirebaseMessagingInterceptor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void Intercept(RxHttpRequestOptions request)
        {
            request.AddHeader(new
            {
                Authorization = $"key={_configuration.GetValue<string>("FirebaseServerKey")}"
            });
        }
    }
}