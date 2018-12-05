/**
 * Autogenerated by Thrift Compiler (0.11.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

public partial class ThriftCase {
  public interface ISync {
    int testCase1(int num1, int num2, string num3);
    List<string> testCase2(Dictionary<string, string> num1);
    void testCase3();
    void testCase4(List<Blog> blog);
  }

  public interface Iface : ISync {
    #if SILVERLIGHT
    IAsyncResult Begin_testCase1(AsyncCallback callback, object state, int num1, int num2, string num3);
    int End_testCase1(IAsyncResult asyncResult);
    #endif
    #if SILVERLIGHT
    IAsyncResult Begin_testCase2(AsyncCallback callback, object state, Dictionary<string, string> num1);
    List<string> End_testCase2(IAsyncResult asyncResult);
    #endif
    #if SILVERLIGHT
    IAsyncResult Begin_testCase3(AsyncCallback callback, object state);
    void End_testCase3(IAsyncResult asyncResult);
    #endif
    #if SILVERLIGHT
    IAsyncResult Begin_testCase4(AsyncCallback callback, object state, List<Blog> blog);
    void End_testCase4(IAsyncResult asyncResult);
    #endif
  }

  public class Client : IDisposable, Iface {
    public Client(TProtocol prot) : this(prot, prot)
    {
    }

    public Client(TProtocol iprot, TProtocol oprot)
    {
      iprot_ = iprot;
      oprot_ = oprot;
    }

    protected TProtocol iprot_;
    protected TProtocol oprot_;
    protected int seqid_;

    public TProtocol InputProtocol
    {
      get { return iprot_; }
    }
    public TProtocol OutputProtocol
    {
      get { return oprot_; }
    }


    #region " IDisposable Support "
    private bool _IsDisposed;

    // IDisposable
    public void Dispose()
    {
      Dispose(true);
    }
    

    protected virtual void Dispose(bool disposing)
    {
      if (!_IsDisposed)
      {
        if (disposing)
        {
          if (iprot_ != null)
          {
            ((IDisposable)iprot_).Dispose();
          }
          if (oprot_ != null)
          {
            ((IDisposable)oprot_).Dispose();
          }
        }
      }
      _IsDisposed = true;
    }
    #endregion


    
    #if SILVERLIGHT
    public IAsyncResult Begin_testCase1(AsyncCallback callback, object state, int num1, int num2, string num3)
    {
      return send_testCase1(callback, state, num1, num2, num3);
    }

    public int End_testCase1(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      return recv_testCase1();
    }

    #endif

    public int testCase1(int num1, int num2, string num3)
    {
      #if !SILVERLIGHT
      send_testCase1(num1, num2, num3);
      return recv_testCase1();

      #else
      var asyncResult = Begin_testCase1(null, null, num1, num2, num3);
      return End_testCase1(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_testCase1(AsyncCallback callback, object state, int num1, int num2, string num3)
    #else
    public void send_testCase1(int num1, int num2, string num3)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("testCase1", TMessageType.Call, seqid_));
      testCase1_args args = new testCase1_args();
      args.Num1 = num1;
      args.Num2 = num2;
      args.Num3 = num3;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public int recv_testCase1()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      testCase1_result result = new testCase1_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      if (result.__isset.success) {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "testCase1 failed: unknown result");
    }

    
    #if SILVERLIGHT
    public IAsyncResult Begin_testCase2(AsyncCallback callback, object state, Dictionary<string, string> num1)
    {
      return send_testCase2(callback, state, num1);
    }

    public List<string> End_testCase2(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      return recv_testCase2();
    }

    #endif

    public List<string> testCase2(Dictionary<string, string> num1)
    {
      #if !SILVERLIGHT
      send_testCase2(num1);
      return recv_testCase2();

      #else
      var asyncResult = Begin_testCase2(null, null, num1);
      return End_testCase2(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_testCase2(AsyncCallback callback, object state, Dictionary<string, string> num1)
    #else
    public void send_testCase2(Dictionary<string, string> num1)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("testCase2", TMessageType.Call, seqid_));
      testCase2_args args = new testCase2_args();
      args.Num1 = num1;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public List<string> recv_testCase2()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      testCase2_result result = new testCase2_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      if (result.__isset.success) {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "testCase2 failed: unknown result");
    }

    
    #if SILVERLIGHT
    public IAsyncResult Begin_testCase3(AsyncCallback callback, object state)
    {
      return send_testCase3(callback, state);
    }

    public void End_testCase3(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      recv_testCase3();
    }

    #endif

    public void testCase3()
    {
      #if !SILVERLIGHT
      send_testCase3();
      recv_testCase3();

      #else
      var asyncResult = Begin_testCase3(null, null);
      End_testCase3(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_testCase3(AsyncCallback callback, object state)
    #else
    public void send_testCase3()
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("testCase3", TMessageType.Call, seqid_));
      testCase3_args args = new testCase3_args();
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public void recv_testCase3()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      testCase3_result result = new testCase3_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      return;
    }

    
    #if SILVERLIGHT
    public IAsyncResult Begin_testCase4(AsyncCallback callback, object state, List<Blog> blog)
    {
      return send_testCase4(callback, state, blog);
    }

    public void End_testCase4(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      recv_testCase4();
    }

    #endif

    public void testCase4(List<Blog> blog)
    {
      #if !SILVERLIGHT
      send_testCase4(blog);
      recv_testCase4();

      #else
      var asyncResult = Begin_testCase4(null, null, blog);
      End_testCase4(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_testCase4(AsyncCallback callback, object state, List<Blog> blog)
    #else
    public void send_testCase4(List<Blog> blog)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("testCase4", TMessageType.Call, seqid_));
      testCase4_args args = new testCase4_args();
      args.Blog = blog;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public void recv_testCase4()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      testCase4_result result = new testCase4_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      return;
    }

  }
  public class Processor : TProcessor {
    public Processor(ISync iface)
    {
      iface_ = iface;
      processMap_["testCase1"] = testCase1_Process;
      processMap_["testCase2"] = testCase2_Process;
      processMap_["testCase3"] = testCase3_Process;
      processMap_["testCase4"] = testCase4_Process;
    }

    protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
    private ISync iface_;
    protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

    public bool Process(TProtocol iprot, TProtocol oprot)
    {
      try
      {
        TMessage msg = iprot.ReadMessageBegin();
        ProcessFunction fn;
        processMap_.TryGetValue(msg.Name, out fn);
        if (fn == null) {
          TProtocolUtil.Skip(iprot, TType.Struct);
          iprot.ReadMessageEnd();
          TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
          oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
          x.Write(oprot);
          oprot.WriteMessageEnd();
          oprot.Transport.Flush();
          return true;
        }
        fn(msg.SeqID, iprot, oprot);
      }
      catch (IOException)
      {
        return false;
      }
      return true;
    }

    public void testCase1_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      testCase1_args args = new testCase1_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      testCase1_result result = new testCase1_result();
      try
      {
        result.Success = iface_.testCase1(args.Num1, args.Num2, args.Num3);
        oprot.WriteMessageBegin(new TMessage("testCase1", TMessageType.Reply, seqid)); 
        result.Write(oprot);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        TApplicationException x = new TApplicationException      (TApplicationException.ExceptionType.InternalError," Internal error.");
        oprot.WriteMessageBegin(new TMessage("testCase1", TMessageType.Exception, seqid));
        x.Write(oprot);
      }
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public void testCase2_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      testCase2_args args = new testCase2_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      testCase2_result result = new testCase2_result();
      try
      {
        result.Success = iface_.testCase2(args.Num1);
        oprot.WriteMessageBegin(new TMessage("testCase2", TMessageType.Reply, seqid)); 
        result.Write(oprot);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        TApplicationException x = new TApplicationException      (TApplicationException.ExceptionType.InternalError," Internal error.");
        oprot.WriteMessageBegin(new TMessage("testCase2", TMessageType.Exception, seqid));
        x.Write(oprot);
      }
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public void testCase3_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      testCase3_args args = new testCase3_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      testCase3_result result = new testCase3_result();
      try
      {
        iface_.testCase3();
        oprot.WriteMessageBegin(new TMessage("testCase3", TMessageType.Reply, seqid)); 
        result.Write(oprot);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        TApplicationException x = new TApplicationException      (TApplicationException.ExceptionType.InternalError," Internal error.");
        oprot.WriteMessageBegin(new TMessage("testCase3", TMessageType.Exception, seqid));
        x.Write(oprot);
      }
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public void testCase4_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      testCase4_args args = new testCase4_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      testCase4_result result = new testCase4_result();
      try
      {
        iface_.testCase4(args.Blog);
        oprot.WriteMessageBegin(new TMessage("testCase4", TMessageType.Reply, seqid)); 
        result.Write(oprot);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        TApplicationException x = new TApplicationException      (TApplicationException.ExceptionType.InternalError," Internal error.");
        oprot.WriteMessageBegin(new TMessage("testCase4", TMessageType.Exception, seqid));
        x.Write(oprot);
      }
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase1_args : TBase
  {
    private int _num1;
    private int _num2;
    private string _num3;

    public int Num1
    {
      get
      {
        return _num1;
      }
      set
      {
        __isset.num1 = true;
        this._num1 = value;
      }
    }

    public int Num2
    {
      get
      {
        return _num2;
      }
      set
      {
        __isset.num2 = true;
        this._num2 = value;
      }
    }

    public string Num3
    {
      get
      {
        return _num3;
      }
      set
      {
        __isset.num3 = true;
        this._num3 = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool num1;
      public bool num2;
      public bool num3;
    }

    public testCase1_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32) {
                Num1 = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                Num2 = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.String) {
                Num3 = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase1_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.num1) {
          field.Name = "num1";
          field.Type = TType.I32;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Num1);
          oprot.WriteFieldEnd();
        }
        if (__isset.num2) {
          field.Name = "num2";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Num2);
          oprot.WriteFieldEnd();
        }
        if (Num3 != null && __isset.num3) {
          field.Name = "num3";
          field.Type = TType.String;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Num3);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase1_args(");
      bool __first = true;
      if (__isset.num1) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Num1: ");
        __sb.Append(Num1);
      }
      if (__isset.num2) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Num2: ");
        __sb.Append(Num2);
      }
      if (Num3 != null && __isset.num3) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Num3: ");
        __sb.Append(Num3);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase1_result : TBase
  {
    private int _success;

    public int Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool success;
    }

    public testCase1_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.I32) {
                Success = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase1_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          field.Name = "Success";
          field.Type = TType.I32;
          field.ID = 0;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Success);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase1_result(");
      bool __first = true;
      if (__isset.success) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Success: ");
        __sb.Append(Success);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase2_args : TBase
  {
    private Dictionary<string, string> _num1;

    public Dictionary<string, string> Num1
    {
      get
      {
        return _num1;
      }
      set
      {
        __isset.num1 = true;
        this._num1 = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool num1;
    }

    public testCase2_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Map) {
                {
                  Num1 = new Dictionary<string, string>();
                  TMap _map5 = iprot.ReadMapBegin();
                  for( int _i6 = 0; _i6 < _map5.Count; ++_i6)
                  {
                    string _key7;
                    string _val8;
                    _key7 = iprot.ReadString();
                    _val8 = iprot.ReadString();
                    Num1[_key7] = _val8;
                  }
                  iprot.ReadMapEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase2_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Num1 != null && __isset.num1) {
          field.Name = "num1";
          field.Type = TType.Map;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteMapBegin(new TMap(TType.String, TType.String, Num1.Count));
            foreach (string _iter9 in Num1.Keys)
            {
              oprot.WriteString(_iter9);
              oprot.WriteString(Num1[_iter9]);
            }
            oprot.WriteMapEnd();
          }
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase2_args(");
      bool __first = true;
      if (Num1 != null && __isset.num1) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Num1: ");
        __sb.Append(Num1);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase2_result : TBase
  {
    private List<string> _success;

    public List<string> Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool success;
    }

    public testCase2_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.List) {
                {
                  Success = new List<string>();
                  TList _list10 = iprot.ReadListBegin();
                  for( int _i11 = 0; _i11 < _list10.Count; ++_i11)
                  {
                    string _elem12;
                    _elem12 = iprot.ReadString();
                    Success.Add(_elem12);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase2_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.List;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            {
              oprot.WriteListBegin(new TList(TType.String, Success.Count));
              foreach (string _iter13 in Success)
              {
                oprot.WriteString(_iter13);
              }
              oprot.WriteListEnd();
            }
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase2_result(");
      bool __first = true;
      if (Success != null && __isset.success) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Success: ");
        __sb.Append(Success);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase3_args : TBase
  {

    public testCase3_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase3_args");
        oprot.WriteStructBegin(struc);
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase3_args(");
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase3_result : TBase
  {

    public testCase3_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase3_result");
        oprot.WriteStructBegin(struc);

        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase3_result(");
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase4_args : TBase
  {
    private List<Blog> _blog;

    public List<Blog> Blog
    {
      get
      {
        return _blog;
      }
      set
      {
        __isset.blog = true;
        this._blog = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool blog;
    }

    public testCase4_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.List) {
                {
                  Blog = new List<Blog>();
                  TList _list14 = iprot.ReadListBegin();
                  for( int _i15 = 0; _i15 < _list14.Count; ++_i15)
                  {
                    Blog _elem16;
                    _elem16 = new Blog();
                    _elem16.Read(iprot);
                    Blog.Add(_elem16);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase4_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Blog != null && __isset.blog) {
          field.Name = "blog";
          field.Type = TType.List;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.Struct, Blog.Count));
            foreach (Blog _iter17 in Blog)
            {
              _iter17.Write(oprot);
            }
            oprot.WriteListEnd();
          }
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase4_args(");
      bool __first = true;
      if (Blog != null && __isset.blog) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Blog: ");
        __sb.Append(Blog);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class testCase4_result : TBase
  {

    public testCase4_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("testCase4_result");
        oprot.WriteStructBegin(struc);

        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("testCase4_result(");
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
