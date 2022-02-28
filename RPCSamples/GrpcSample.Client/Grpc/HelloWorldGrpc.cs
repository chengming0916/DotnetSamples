// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GrpcSample.DTO/protos/HelloWorld.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GRPCDemo {
  public static partial class gRPC
  {
    static readonly string __ServiceName = "gRPCDemo.gRPC";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GRPCDemo.HelloRequest> __Marshaller_gRPCDemo_HelloRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GRPCDemo.HelloRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GRPCDemo.HelloReply> __Marshaller_gRPCDemo_HelloReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GRPCDemo.HelloReply.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> __Method_SayHello = new grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SayHello",
        __Marshaller_gRPCDemo_HelloRequest,
        __Marshaller_gRPCDemo_HelloReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> __Method_ServerStreaming = new grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ServerStreaming",
        __Marshaller_gRPCDemo_HelloRequest,
        __Marshaller_gRPCDemo_HelloReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> __Method_ClientStreaming = new grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "ClientStreaming",
        __Marshaller_gRPCDemo_HelloRequest,
        __Marshaller_gRPCDemo_HelloReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> __Method_BidirectionalStreaming = new grpc::Method<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "BidirectionalStreaming",
        __Marshaller_gRPCDemo_HelloRequest,
        __Marshaller_gRPCDemo_HelloReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GRPCDemo.HelloWorldReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for gRPC</summary>
    public partial class gRPCClient : grpc::ClientBase<gRPCClient>
    {
      /// <summary>Creates a new client for gRPC</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gRPCClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for gRPC that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gRPCClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gRPCClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gRPCClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GRPCDemo.HelloReply SayHello(global::GRPCDemo.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHello(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GRPCDemo.HelloReply SayHello(global::GRPCDemo.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SayHello, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GRPCDemo.HelloReply> SayHelloAsync(global::GRPCDemo.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHelloAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GRPCDemo.HelloReply> SayHelloAsync(global::GRPCDemo.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SayHello, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::GRPCDemo.HelloReply> ServerStreaming(global::GRPCDemo.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ServerStreaming(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::GRPCDemo.HelloReply> ServerStreaming(global::GRPCDemo.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ServerStreaming, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncClientStreamingCall<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> ClientStreaming(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ClientStreaming(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncClientStreamingCall<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> ClientStreaming(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_ClientStreaming, null, options);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> BidirectionalStreaming(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return BidirectionalStreaming(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::GRPCDemo.HelloRequest, global::GRPCDemo.HelloReply> BidirectionalStreaming(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_BidirectionalStreaming, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override gRPCClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new gRPCClient(configuration);
      }
    }

  }
}
#endregion